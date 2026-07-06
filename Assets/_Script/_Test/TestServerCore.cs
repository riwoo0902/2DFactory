using System;
using LrwLib.UnityServer.Core;
using UnityEngine;

namespace _Script._Test
{
    public class TestServerCore : MonoBehaviour
    {
        private const int TestPort = 322109;
        private UnityServerCore _core;
        
        private void Awake()
        {
            _core = new UnityServerCore(TestPort);
            _core.Start();
            _core.OnReadPacket += OnOnReadPacket;
        }
        
        private void OnDestroy()
        {
            _core.Close();
        }
        
        private void OnOnReadPacket(int id, string data)
        {
            Debug.Log($"id : {id}, data : {data}");
        }
        
        
    }
}