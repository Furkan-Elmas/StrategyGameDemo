using UnityEngine;
using UnityEngine.InputSystem;

namespace PanteonStrategyDemo.Abstracts.InputSystem
{
    public class InputData
    {
        DefaultAction _inputActions;

        public Vector2 MousePosition { get; private set; }
        public bool LeftClickCheck { get; private set; }
        public bool RightClickCheck { get; private set; }

        
        public InputData()
        {
            _inputActions = new DefaultAction();

            _inputActions.Player.MousePosition.performed += GetMousePositionOnPerformed;
            _inputActions.Player.LeftClick.performed += LeftClickCheckOnPerformed;
            _inputActions.Player.RightClick.performed += RightClickCheckOnPerformed;

            _inputActions.Enable();
        }

        private void RightClickCheckOnPerformed(InputAction.CallbackContext obj)
        {
            RightClickCheck = obj.ReadValueAsButton();
        }

        private void LeftClickCheckOnPerformed(InputAction.CallbackContext obj)
        {
            LeftClickCheck = obj.ReadValueAsButton();
        }

        private void GetMousePositionOnPerformed(InputAction.CallbackContext obj)
        {
            MousePosition = obj.ReadValue<Vector2>();
        }
    }
}