using _Script._2DPlatformAstar.Data.MapData;
using _Script._2DPlatformAstar.MoveSystem;
using _Script._2DPlatformAstar.Surface;
using _Script._Core._ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Script._2DPlatformAstar.Test
{
    public class TestMover : MonoBehaviour
    {
        [SerializeField] private AgentMover agentMover;
        
        private void Update()
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            agentMover.MoveToTarget(worldMousePos);
        }
        
    }
}