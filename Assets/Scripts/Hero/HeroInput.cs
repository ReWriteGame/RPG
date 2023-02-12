using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class HeroInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Hero hero;
    [SerializeField] private UICanvasControllerInput ñanvasControllerInput;

    private UserInput input;


    private void Awake()
    {
        input = new UserInput();
        playerInput.actions[input.Player.Jump.name].performed += Jump;
        playerInput.actions[input.Player.Sprint.name].performed += Sprint;
        playerInput.actions[input.Player.Move.name].performed += MoveInputDevices;
        ñanvasControllerInput.OnVirtualMoveInput += hero.SetMove;
    }

    private void OnDestroy()
    {
        playerInput.actions[input.Player.Jump.name].performed -= Jump;
        playerInput.actions[input.Player.Sprint.name].performed -= Sprint;
        playerInput.actions[input.Player.Move.name].performed -= MoveInputDevices;
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
