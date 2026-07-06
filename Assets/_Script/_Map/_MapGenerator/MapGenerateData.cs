using UnityEngine;

namespace _Script._Map._MapGenerator
{
    [CreateAssetMenu(fileName = "Map Generate Data", menuName = "Map/Generate Data", order = 0)]
    public class MapGenerateData : ScriptableObject
    {
        [field:SerializeField] public int Seed { get; private set; } = 10000;
        
    }
}