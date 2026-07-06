using System.Collections.Generic;
using UnityEngine;

namespace LrwLib.UnityServer._ServerDebug
{
    public static class ServerDebug
    {
        private static Queue<string> _queue = new();
        private static readonly object Lock = new();
        public static void AddDebug(string message)
        {
            lock (Lock)
            {
                _queue.Enqueue(message);
            }
        }

        public static void Print()
        {
            lock (Lock)
            {
                while (_queue.Count > 0)
                {
                    Debug.Log(_queue.Dequeue());
                }
            }
        }
        
        
    }
}