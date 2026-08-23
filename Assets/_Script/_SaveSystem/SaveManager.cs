using System.IO;
using _Script._Test;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace _Script._SaveSystem
{
    public static class SaveManager
    {
        private const string DirectoryName = "SaveData";

        public static string DirectoryPath;

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init()
        {
            DirectoryPath = PathCombine(Application.dataPath,"..",DirectoryName);
            
            Directory.CreateDirectory(DirectoryPath);
        }
        
        private static string PathCombine(params string[] paths) 
            => Path.GetFullPath(Path.Combine(paths));

        public static void WriteFile(string path, string text)
        {
            try
            {
                string filePath = PathCombine(DirectoryPath, path);
                File.WriteAllText(filePath, text);
            }
            catch
            {
                FDebug.Log("Save Failed");
            }
        }
        
        public static string ReadFile(string path, string baseData = "")
        {
            try
            {
                string filePath = PathCombine(DirectoryPath, path);
                return File.ReadAllText(filePath);
            }
            catch
            {
                return baseData;
            }
        }
        
    }
}