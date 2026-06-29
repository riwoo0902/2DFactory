using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script._Debug
{
    public class DebugManager : MonoBehaviour
    {
        private Dictionary<Type, Queue<string>> _debugs = new();

        private void Awake()
        {
            
        }


        public void AddDebug<T>(string text)
        {
            
        }

        public void Debug<T>()
        {
            
        }
    }
}