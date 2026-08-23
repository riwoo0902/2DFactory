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
        
    }
}