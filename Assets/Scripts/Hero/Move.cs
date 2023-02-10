using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] public CharacterController controller;
    private Vector2 moveDirection;

    private float targetRotation;
    private float rotationVelocity;




    [Space(10)]
    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;
    public float JumpTimeout = 0.50f;
    public float FallTimeout = 0.15f;
   
    public bool jump = false;
    public bool Grounded = true;



    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    public float _jumpTimeoutDelta;
    public float _fallTimeoutDelta;

    public Vector2 MoveDirection { get => moveDirection;}

    public void HorizontalMove(Vector2 direction, float speed)
    {
        float scaledMoveSpeed = speed * Time.deltaTime;
        moveDirection = direction;

        //Fix the problem of diagonal acceleration
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
       
        Vector3 horizontalMove = targetDirection.normalized * scaledMoveSpeed;
        Vector3 verticalMove = new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
        controller.Move(horizontalMove + verticalMove);
    }

    public void RotateVector(Vector2 direction, float rotationSmoothTime, float additionalRotation = 0)
    {
        if (direction == Vector2.zero) return;

        Vector3 inputDirection = new Vector3(direction.x, 0.0f, direction.y).normalized;
        targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + additionalRotation;

        RotateDegree(targetRotation, rotationSmoothTime);
    }

    public void RotateDegree(float degree, float rotationSmoothTime)
    {
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, degree,
            ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    public void JumpAndGravity()
    {
        if (Grounded)
        {
            _fallTimeoutDelta = FallTimeout;


            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
                _verticalVelocity = -2f;


            if (jump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
                _jumpTimeoutDelta -= Time.deltaTime;

        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
                _fallTimeoutDelta -= Time.deltaTime;


            // if we are not grounded, do not jump
            jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
            _verticalVelocity += Gravity * Time.deltaTime;

    }

   
}


