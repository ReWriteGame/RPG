using System;
using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        public Action<Vector2> OnVirtualMoveInput;
        public Action<Vector2> OnVirtualLookInput;
        public Action<bool> OnVirtualJumpInput;
        public Action<bool> OnVirtualSprintInput;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            OnVirtualMoveInput?.Invoke(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            OnVirtualLookInput?.Invoke(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            OnVirtualJumpInput?.Invoke(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            OnVirtualSprintInput?.Invoke(virtualSprintState);
        }
    }
}
