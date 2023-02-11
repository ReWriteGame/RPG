using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    public Animator _animator;
    public Hero hero;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    private float _animationBlend;
    private bool _hasAnimator;


    private void Start()
    {
        _hasAnimator = _animator != null;

        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("MoveSpeed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void Update()
    {
        //_hasAnimator = TryGetComponent(out _animator);

        if (!_hasAnimator) return;
        AnimationMove();
        AnimationGround();
        AnimationFall();
    }


  
    private void AnimationMove()
    {
        _animationBlend = Mathf.Lerp(_animationBlend, hero.targetSpeed, Time.deltaTime * hero.speedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat(_animIDSpeed, _animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, NormalizedToOne(hero.InputUserDirection).magnitude);
    }

    private void AnimationGround()
    {
        _animator.SetBool(_animIDGrounded, hero.Move.Grounded);


        if (hero.Move.Grounded)
        {
            bool jump = hero.Move.jump && hero.Move._jumpTimeoutDelta <= 0.0f;
            _animator.SetBool(_animIDJump, jump);

            _animator.SetBool(_animIDFreeFall, false);
        }
    }

    private void AnimationFall()
    {
        if (hero.Move.Grounded) return;

        if (hero.Move._fallTimeoutDelta < 0.0f)
            _animator.SetBool(_animIDFreeFall, true);
    }

    private Vector2 NormalizedToOne(Vector2 vector)
    {
        return vector.magnitude > 1 ? vector.normalized : vector;
    }
}
