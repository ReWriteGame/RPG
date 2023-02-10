using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Move move;
    [SerializeField] public float moveSpeed = 1;
    [SerializeField] public float sprintSpeed = 5;
    public float speedChangeRate = 10.0f;
    public float speedOffset = 0.1f;

    public bool sprint = false;
    //public Vector2 moveDirection;
    public bool analogMovement = true;
    private Vector2 inputUserDirection;

    public Camera camera;


    [Range(0.0f, 0.5f)]
    public float RotationSmoothTime = 0.12f;

    private float speed;
    public float targetSpeed;
    private float currentHorizontalSpeed;
    float inputMagnitude;

    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    public Move Move => move;

    public Vector2 InputUserDirection => inputUserDirection;

    public void SetMove(Vector2 direction)
    {
        inputUserDirection = direction;
    }

    private void Update()
    {
        MoveLogic();
        RotateLogic();
        JumpAndGravityLogic();
    }

    private void MoveLogic()
    {
        targetSpeed = sprint ? sprintSpeed : moveSpeed;
        if (inputUserDirection == Vector2.zero) targetSpeed = 0.0f;

        inputMagnitude = analogMovement ? inputUserDirection.magnitude : 1f;
        speed = targetSpeed;


        //currentHorizontalSpeed = (moveDirection * targetSpeed).magnitude;
        currentHorizontalSpeed = new Vector3(move.controller.velocity.x, 0.0f, move.controller.velocity.z).magnitude;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * speedChangeRate);

            speed = RoundValue(speed, 1000f);
        }

        move.HorizontalMove(inputUserDirection, speed);
    }

    private void RotateLogic()
    {
        float cameraViewDirections = camera.transform.eulerAngles.y;
        move.RotateVector(inputUserDirection, RotationSmoothTime, cameraViewDirections);
    }
        

    private void JumpAndGravityLogic()
    {
        move.Grounded = GroundedCheck();

        move.JumpAndGravity();
    }


    // round speed to X decimal places
    private float RoundValue(float value, float viewValue)
    {
        return Mathf.Round(speed * viewValue) / viewValue;
    }

    public bool GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        return Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }
}
