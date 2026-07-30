using System;
using System.Collections;
using System.Collections.Generic;

namespace _Script._Core
{
    public static class AbbClass
    {
        public static void Foreach<T>(this IEnumerable<T> enumerator, Action<T> action)
        {
            foreach (var i in enumerator)
            {
                action(i);
            }
        }
    }
}