using System;
using LrwLib.UnityServer.Core;
using UnityEngine;

namespace LrwLib.UnityServer._ServerDebug
{
    public class ServerDebugPrinter : MonoBehaviour
    {
        private UnityServerCore _serverCore;
        
        private void Awake()
        {
            _serverCore = new UnityServerCore(9020);
            _serverCore.Start();
            _serverCore.OnReadPacket += HandleServerInput;
        }

        private void OnDestroy()
        {
            _serverCore.OnReadPacket -= HandleServerInput;
        }
        
        private void FixedUpdate()
        {
            _serverCore.SendAll("riwooUnity");
            ServerDebug.Print();
        }

        private void HandleServerInput(int id,string value)
        {
            ServerDebug.AddDebug(value);
        }
        
        
        
    }
}