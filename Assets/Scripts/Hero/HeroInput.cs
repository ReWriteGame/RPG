using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class HeroInput : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private UICanvasControllerInput ñanvasControllerInput;

    private UserInput input;


    private void Awake()
    {
        input = new UserInput();
        input.Player.Jump.performed += Jump;
        input.Player.Sprint.performed += Sprint;
        input.Player.Move.performed += MoveInputDevices;
        ñanvasControllerInput.OnVirtualMoveInput += hero.SetMove;
    }

    private void OnDestroy()
    {
        input.Player.Jump.performed -= Jump;
        input.Player.Sprint.performed -= Sprint;
        input.Player.Move.performed -= MoveInputDevices;
        ñanvasControllerInput.OnVirtualMoveInput -= hero.SetMove;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void MoveInputDevices(InputAction.CallbackContext callback)
    {
        Vector2 direction = callback.ReadValue<Vector2>();
        hero.SetMove(direction);
    }


    private void Jump(InputAction.CallbackContext callback)
    {
        hero.Move.jump = true;
    }

    private void Sprint(InputAction.CallbackContext callback)
    {
        hero.sprint = Convert.ToBoolean(callback.ReadValue<float>());
    }
}
