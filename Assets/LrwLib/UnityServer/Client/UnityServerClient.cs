using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LrwLib.UnityServer.Client
{
    public class UnityServerClient
    {
        private readonly int _port;
        
        public event Action<string> OnReadPacket;
        
        private StreamWriter _streamWriter;
        
        public UnityServerClient(int port)
        {
            if(!UnityServerHelper.CheckPortRange(port)) throw new Exception("port out of range");
            
            _port = port;
        }

        public void Start()
        {
            if (!TryConnect(_port,out TcpClient client)) return;
            
            NetworkStream netWorkStream = client.GetStream();
            StreamReader reader = new StreamReader(netWorkStream,Encoding.UTF8);
            _streamWriter = new StreamWriter(netWorkStream, Encoding.UTF8)
            {
                AutoFlush = true
            };
            
            Task.Run(() => Reader(reader));
        }

        private static bool TryConnect(int port,out TcpClient client)
        {
            client = new TcpClient();
            
            try
            {
                client.Connect(UnityServerHelper.GetCurrentIP(),port);
            }
            catch
            {
                client.Dispose();
                client = null;
                return false;
            }
            return true;
        }

        private void Reader(StreamReader streamReader)
        {
            try
            {
                while (true)
                {
                    string input = streamReader.ReadLine();
                    if (input == null) break;
                    OnReadPacket?.Invoke(input);
                }
            }
            catch
            {
                
            }
        }

        public void Send(string message)
        {
            try
            {
                _streamWriter.WriteLine(message);
            }
            catch
            {
                
            }
            
        }
        
        
        
    }
}