using UnityEngine;
using UnityEngine.InputSystem;

namespace PanteonStrategyDemo.Abstracts.InputSystem
{
    public class InputData
    {
        DefaultAction _inputAction;

        public Vector2 MousePosition { get; private set; }
        public bool LeftClickCheck { get; private set; }
        public bool RightClickCheck { get; private set; }

        public InputData()
        {
            _inputAction = new DefaultAction();

            _inputAction.Player.MousePosition.performed += GetMousePositionOnPerformed;
            _inputAction.Player.LeftClick.performed += LeftClickCheckOnPerformed;
            _inputAction.Player.RightClick.performed += RightClickCheckOnPerformed;

            _inputAction.Enable();
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