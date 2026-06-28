using System;
using System.Collections.Generic;

namespace LrwLib.LrwAddClass
{
    public static class AddClass
    {
        public static void Foreach<T>(this IEnumerable<T> arr, Action<T> action)
        {
            foreach (T item in arr) 
            {
                action(item);
            }
        }
        
        
    }
}