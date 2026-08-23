using System;
using _Script._Core._EventSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._AstarSystem.BakeSystem
{
    public class AstarManager : MonoBehaviour
    {
        [SerializeField] private AstarMoveDirDataSo moveDirDataSo;
        
        [SerializeField] private Tilemap tilemap;
        
        private AstarMap _astarMap;
        
        private void Awake()
        {
            if (moveDirDataSo == null) throw new Exception("Astar Move Data so is null");
            
            _astarMap = new AstarMap(moveDirDataSo,tilemap);
            
            EventBus<AddAstarNodeEvent>.Event += AddNode;
            EventBus<RemoveAstarNodeEvent>.Event += RemoveNode;
        }
        
        private void OnDestroy()
        {
            EventBus<AddAstarNodeEvent>.Event -= AddNode;
            EventBus<RemoveAstarNodeEvent>.Event -= RemoveNode;
        }
        
        private void AddNode(AddAstarNodeEvent evt)
        {
            _astarMap.AddBlock(evt.Position);
        }
        
        private void RemoveNode(RemoveAstarNodeEvent evt)
        {
            _astarMap.RemoveBlock(evt.Position);
        }


        private void OnDrawGizmosSelected()
        {
            
            if (_astarMap != null)
            {
                foreach (Node node in _astarMap.Nodes)
                {
                    Vector3 pos = (Vector2)node.SelfPos;
                    
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(pos, 0.3f);

                    foreach (var link in node.Links)
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawLine(pos,(Vector2)link);
                    }
                }
            }
            
            
            
        }
        
    }
}