using System;
using LrwLib.ButtonAttribute;
using TMPro;
using UnityEngine;

namespace _Script._SaveSystem
{
    public class SaveDebugger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        
        private void Awake()
        {
            Log();
        }

        [UnityButton]
        private void Log()
        {
            textMeshProUGUI.text = SaveManager.DirectoryPath;
            SaveManager.WriteFile("TestFile.txt","riwoo");
        }
    }
}