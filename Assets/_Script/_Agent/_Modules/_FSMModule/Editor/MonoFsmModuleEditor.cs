#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Script._Agent._Modules._FSMModule.Editor
{
    [CustomEditor(typeof(MonoFsmModule))]
    public class MonoFsmModuleEditor : UnityEditor.Editor
    {
        private const string NullOption = "NULL";
        private const string StartStateNameProperty = "startStateName";
    
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
        
            SerializedProperty startStateName = serializedObject.FindProperty(StartStateNameProperty);
            List<Type> stateTypes = GetStateTypesInHierarchyOrder((MonoFsmModule)target);
        
            DrawStartStateDropdown(startStateName, stateTypes);
        
            serializedObject.ApplyModifiedProperties();
        }
    
        private static void DrawStartStateDropdown(SerializedProperty startStateName, List<Type> stateTypes)
        {
            string[] options = BuildOptions(stateTypes);
            int currentIndex = GetCurrentIndex(startStateName.stringValue, stateTypes);
        
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            int selectedIndex = EditorGUILayout.Popup("Start State", currentIndex, options);
            EditorGUI.EndDisabledGroup();
        
            if (selectedIndex == currentIndex)
            {
                return;
            }
        
            startStateName.stringValue = selectedIndex == 0
                ? string.Empty
                : stateTypes[selectedIndex - 1].AssemblyQualifiedName;
        }
    
        private static string[] BuildOptions(List<Type> stateTypes)
        {
            string[] options = new string[stateTypes.Count + 1];
            options[0] = NullOption;
        
            for (int i = 0; i < stateTypes.Count; i++)
            {
                options[i + 1] = stateTypes[i].Name;
            }
        
            return options;
        }
    
        private static int GetCurrentIndex(string currentValue, List<Type> stateTypes)
        {
            if (string.IsNullOrEmpty(currentValue))
            {
                return 0;
            }
        
            for (int i = 0; i < stateTypes.Count; i++)
            {
                Type stateType = stateTypes[i];
                if (stateType.AssemblyQualifiedName == currentValue ||
                    stateType.FullName == currentValue ||
                    stateType.Name == currentValue)
                {
                    return i + 1;
                }
            }
        
            return 0;
        }
    
        private static List<Type> GetStateTypesInHierarchyOrder(MonoFsmModule fsmModule)
        {
            IMonoState[] monoStates = fsmModule.GetComponentsInChildren<IMonoState>(true);
            List<Type> stateTypes = new();
            HashSet<Type> addedTypes = new();
        
            foreach (IMonoState monoState in monoStates)
            {
                Type stateType = monoState.GetType();
                if (addedTypes.Add(stateType))
                {
                    stateTypes.Add(stateType);
                }
            }
        
            return stateTypes;
        }
    }
}
#endif