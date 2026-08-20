using System;
using LrwLib.ButtonAttribute;
using TMPro;
using UnityEngine;

namespace _Script._SaveSystem
{
    public class SaveDebugger : MonoBehaviour
    {
        private SaveManager _saveManager;
        [SerializeField]
        private TextMeshProUGUI textMeshProUGUI;
        private void Awake()
        {
            _saveManager = new SaveManager();
        }

        private void Start()
        {
            textMeshProUGUI.text = _saveManager.DirectoryPath;
            _saveManager.WriteFile("TestFile","riwoo");
        }

        [UnityButton]
        private void LogDirectoryPath()
        {
            Debug.Log(_saveManager.DirectoryPath);
        }
    }
}