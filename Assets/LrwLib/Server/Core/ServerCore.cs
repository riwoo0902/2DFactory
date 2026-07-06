using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LrwLib.Server.Core
{
    public class ServerCore
    {
        private readonly object _streamsLock = new();
        
        private TcpListener _listener;
        private readonly List<StreamWriter> _streams = new();

        private bool _isRunning;
        
        public event ServerPacket OnReadPacket;

        private int ClientCount
        {
            get
            {
                lock (_streamsLock)
                {
                    return _streams.Count;
                }
            }
        }
        
        public ServerCore(int port)
        {
            if(!ServerHelper.CheckPortRange(port)) throw new Exception("port out of range");
            _listener = new TcpListener(IPAddress.Any, port);
            _isRunning = false;
        }
        
        public void Start()
        {
            if (_isRunning)
            {
                Debug.LogWarning("ServerCore is Already running");
                return;
            }
            _isRunning = true;
            _listener.Start();
            Task.Run(AcceptTcpClients);
        }

        public void Close()
        {
            if (!_isRunning)
            {
                Debug.LogWarning("ServerCore is not running");
                return;
            }

            _isRunning = false;
            _listener.Stop();
            OnReadPacket = null;

            lock (_streamsLock)
            {
                foreach (StreamWriter stream in _streams)
                {
                    try
                    {
                        stream.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                _streams.Clear();
            }
        }

        private void AcceptTcpClients()
        {
            int id = 0;
            while (_isRunning)
            {
                TcpClient client;
                try
                {
                    client = _listener.AcceptTcpClient();
                }
                catch
                {
                    if (!_isRunning) break;
                    continue;
                }
                
                if (!_isRunning)
                {
                    client.Dispose();
                    break;
                }

                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream,Encoding.UTF8)
                {
                    AutoFlush = true
                };
                
                lock (_streamsLock)
                {
                    _streams.Add(writer);
                }
                
                Task.Run(() => ClientReader(stream,id++));
            }
        }

        private void ClientReader(NetworkStream stream,int id)
        {
            StreamReader reader = new(stream,Encoding.UTF8);
            
            try
            {
                while (true)
                {
                    string input = reader.ReadLine();
                    if (input == null) break;
                    OnReadPacket?.Invoke(id,input);
                }
            }
            catch
            {
                lock (_streamsLock)
                {
                    _streams[id] = null;
                }
            }
        }
        
        public void Send(int id, string text)
        {
            if(id < 0 || id >= ClientCount) throw new Exception("id out of range");
            try
            {
                lock (_streamsLock)
                {
                    StreamWriter streamWriter = _streams[id];
                    streamWriter.WriteLine(text);
                }
            }
            catch
            {
                
            }
            
        }
        
        public void SendAll(string text)
        {
            int count = ClientCount;

            for (int i = 0; i < count; i++)
            {
                Send(i,text);
            }
        }
        
        
        
        
        
    }
}
