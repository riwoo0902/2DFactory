using System;
using LrwLib.UnityServer.Core;
using UnityEngine;

namespace _Script._Test
{
    public class TestServerCore : MonoBehaviour
    {
        private const int TestPort = 32109;
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

        private int value = 0;
        private void Update()
        {
            _core.SendAll("Unity!" + value++);
        }

        private void OnOnReadPacket(int id, string data)
        {
            Debug.Log($"id : {id}, data : {data}");
        }
        
        
    }
}