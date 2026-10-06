using System;
using System.Collections.Generic;
using _Script._2DPlatformAstar.Data.MapData;
using _Script._2DPlatformAstar.Surface;
using _Script._Core._ServiceLocator;
using _Script._Test;
using UnityEngine;

namespace _Script._2DPlatformAstar.MoveSystem
{
    public class AgentMover : MonoBehaviour
    {
        [SerializeField] public float speed = 5;
        
        private NavMap _navMap;

        private Vector2 _targetPos;
        private List<Vector2> _path;
        private int _currentIndex;
        private bool IsMoving => _path != null && _path.Count > 0;
        
        private void Start()
        {
            INavSurface navSurface = ServiceLocator.Get<INavSurface>();

            _navMap = navSurface.NavMapData;
            FDebug.Assert(_navMap != null,"NavMapData is null");
            
            _navMap.OnMapChanged += PathUpdate;
        }
        
        private void OnDestroy()
        {
            _navMap.OnMapChanged -= PathUpdate;
        }

        private const float MinDistance = 0.1f;
        private void FixedUpdate()
        {
            if(!IsMoving) return;

            Vector2 currentTargetPos = _path[_currentIndex];

            Vector2 currentPos = transform.position;

            Vector2 movePos = Vector2.MoveTowards(currentPos, currentTargetPos,
                speed * Time.fixedDeltaTime);
            transform.position = movePos;

            if (!(Vector2.Distance(transform.position, currentTargetPos) <= MinDistance)) return;
            
            _currentIndex++;

            if (_currentIndex < _path.Count) return;
            
            _currentIndex = 0;
            _path = null;
        }

        public void MoveToTarget(Vector2 targetPos)
        {
            _targetPos = targetPos;
            _path = GetPath(targetPos);
            _currentIndex = 0;
        }

        private List<Vector2> GetPath(Vector2 targetPos)
            => PathFind.GetPath(_navMap,transform.position, targetPos);
        
        private void PathUpdate()
        {
            if(!IsMoving) return;
            MoveToTarget(_targetPos);
        }
        
    }
}