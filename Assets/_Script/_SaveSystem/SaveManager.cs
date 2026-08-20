using System.IO;
using UnityEngine;

namespace _Script._SaveSystem
{
    public class SaveManager
    {
        private readonly string _directoryName = "SaveData";

        public readonly string DirectoryPath;
        
        public SaveManager()
        {
            DirectoryPath = PathCombine(Application.dataPath,"..",_directoryName);
            
            Directory.CreateDirectory(DirectoryPath);
        }
        
        private static string PathCombine(params string[] paths) 
            => Path.GetFullPath(Path.Combine(paths));

        public void WriteFile(string path, string text)
        {
            string filePath = PathCombine(DirectoryPath, path);
            File.WriteAllText(filePath, text);
        }
        
    }
}