using System;

namespace _Script._Core._Data
{
    [Serializable]
    public struct Pair<T1,T2>
    {
        public T1 first;
        public T2 second;
    }
}