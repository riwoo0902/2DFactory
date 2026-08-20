using System.Collections.Generic;
using UnityEngine;

namespace _Script._AstarSystem.BakeSystem
{
    [CreateAssetMenu(fileName = "Astar Move Data", menuName = "Astar Move Data", order = 0)]
    public class AstarMoveDirDataSo : ScriptableObject
    {
        [SerializeField] private List<Vector2> moveDirs = new();
        
    }
}