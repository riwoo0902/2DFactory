using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Script._Core._Input
{
    [CreateAssetMenu(fileName = "InputSo", menuName = "InputSo", order = 0)]
    public class InputSo : ScriptableObject,InputController.IPlayerActions
    {
        private InputController _inputController;
        
        private void OnEnable()
        {
            if (_inputController == null)
            {
                _inputController = new InputController();
                _inputController.Player.SetCallbacks(this);
            }
            _inputController.Player.Enable();
        }

        private void OnDisable()
        {
            _inputController.Player.Disable();
        }

        public Vector2 MoveDir { get; private set; }
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<Vector2>();
        }

        public event Action MouseDown;
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.started)
                MouseDown?.Invoke();
        }

        public Vector2 MovePos { get; private set; }
        public void OnMouse(InputAction.CallbackContext context)
        {
            MovePos = context.ReadValue<Vector2>();
        }
    }
}