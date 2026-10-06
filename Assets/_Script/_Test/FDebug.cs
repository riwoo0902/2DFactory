using UnityEngine;

namespace _Script._Test
{
    public static class FDebug
    {
        public static void Log(object msg,LogType logType = LogType.Log)
        {
#if UNITY_EDITOR
            Debug.unityLogger.Log(logType, msg);
#endif
        }

        public static void Assert(bool b, string text)
        {
            if(b) return;
            Log(text,LogType.Assert);
        }
    }
}