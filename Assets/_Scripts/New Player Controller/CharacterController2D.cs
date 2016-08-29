using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

    private const float skimWidth = .02f;
    private const int totalHorizontalRays = 8;
    private const int totalVerticalRays = 4;
    private GuiDebugManager debugGui;
    private static readonly float slopeLimitTangant = Mathf.Tan(75f * Mathf.Deg2Rad);

    public bool movementEnabled = true;
    public bool isDead = false;
    public Slider trailSlider;
    public GameObject managers;
    public LayerMask PlatformMask;
    public ControllerParameters2D DefaultParameters;
    public ControllerState2D State { get; private set; }
    public Vector2 Velocity { get { return _velocity; } }
    public bool HandleCollisions { get; set; }
    public ControllerParameters2D Parameters { get { return _overrideParameters ?? DefaultParameters; } }
    public GameObject StandingOn { get; private set; }
    public bool CanJump
    {
        get
        {
            if (Parameters.JumpRestrictions == ControllerParameters2D.JumpBehavior.CanJumpAnywhere)
                return _jumpIn <= 0;

            if (Parameters.JumpRestrictions == ControllerParameters2D.JumpBehavior.CanJumpGround)
                return State.isGrounded;

            return false;
        }

    }



    private Animator animator;
    private Vector2 _velocity;
    private Transform _transform;
    private Vector3 _localScale;
    private BoxCollider2D _boxCollider;
    private ControllerParameters2D _overrideParameters;
    private Vector3
        _rayCastBottomLeft,
        _rayCastBottomRight,
        _rayCastTopLeft;
    private float _jumpIn;  

    private float
        _verticalDistanceBetweenRays,
        _horizontalDistanceBetweenRays;


    public void Awake()
    {
        HandleCollisions = true;
        State = new ControllerState2D();
        _transform = transform;
        _localScale = transform.localScale;
        _boxCollider = GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
        debugGui = managers.GetComponent<GuiDebugManager>();

        animator.SetBool("isMoving", false);
        var colliderWidth = _boxCollider.size.x * Mathf.Abs(transform.localScale.x) - (2 * skimWidth);
        _horizontalDistanceBetweenRays = colliderWidth / (totalVerticalRays - 1);

        var colliderHeight = _boxCollider.size.y * Mathf.Abs(transform.localScale.y) - (2 * skimWidth);
        _verticalDistanceBetweenRays = colliderHeight / (totalHorizontalRays - 1);
    }

    public void AddForce(Vector2 force)
    {
        _velocity = force;
    }

    public void SetForce(Vector2 force)
    {
        _velocity += force;
    }

    public void SetHorizontalForce(float x)
    {
        _velocity.x = x;        
    }

    public void SetVerticalForce(float y)
    {
        _velocity.y = y;
    }

    public void Jump()
    {
        if (!isDead || movementEnabled)
        {
            AddForce(new Vector2(_velocity.x, Parameters.jumpMagnitude));
            _jumpIn = Parameters.jumpFrequency;
        }
    }

    public void LateUpdate()
    {
        _jumpIn -= Time.deltaTime;
        _velocity.y += Parameters.gravity * Time.deltaTime;
        if (!isDead || movementEnabled)
            Move(Velocity * Time.deltaTime);
        //trailSlider.value = transform.position.x;
        
        //animator.SetBool("isMoving", true);
    }

    private void Move(Vector2 deltaMovement)
    {
        var wasGrounded = State.isCollidingBelow;
        State.Reset();

        if(HandleCollisions)
        {
            HandlePlatforms();
            CalculateRayOrigins();

            if (deltaMovement.y < 0 && wasGrounded)
                HandleVerticalSlope(ref deltaMovement);

            if (Mathf.Abs(deltaMovement.x) > 0.001f)
            {
                MoveHorizontally(ref deltaMovement);
                animator.SetBool("isMoving", true);
            }
            else
                animator.SetBool("isMoving", false);
            MoveVertically(ref deltaMovement);
        }

        _transform.Translate(deltaMovement, Space.World);

        // TODO: platform code.

        if (Time.deltaTime > 0)
            _velocity = deltaMovement / Time.deltaTime;

        _velocity.x = Mathf.Min(_velocity.x, Parameters.MaxVelocity.x);
        _velocity.y = Mathf.Min(_velocity.y, Parameters.MaxVelocity.y);

        if (State.isMovingUpSlope)
            _velocity.y = 0;
    }

    private void HandlePlatforms()
    {

    }

    private void CalculateRayOrigins()
    {
        var size = new Vector2(_boxCollider.size.x * Mathf.Abs(_localScale.x), _boxCollider.size.y * Mathf.Abs(_localScale.y)) / 2;
        var center = new Vector2(_boxCollider.offset.x * _localScale.x, _boxCollider.offset.y * _localScale.y);

        _rayCastTopLeft = _transform.position + new Vector3(center.x - size.x + skimWidth, center.y + size.y - skimWidth);
        _rayCastBottomRight = _transform.position + new Vector3(center.x + size.x - skimWidth, center.y - size.y + skimWidth);
        _rayCastBottomLeft = _transform.position + new Vector3(center.x - size.x + skimWidth, center.y - size.y + skimWidth);

    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        var isGoingRight = deltaMovement.x > 0;
        var rayDistance = Mathf.Abs(deltaMovement.x) + skimWidth;
        var rayDirection = isGoingRight ? Vector2.right : -Vector2.right;
        var rayOrigin = isGoingRight ? _rayCastBottomRight : _rayCastBottomLeft;

        for (var i = 0; i < totalHorizontalRays; i++)
        {
            var rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * _verticalDistanceBetweenRays));
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask);

            if (!rayCastHit)
                continue;

            if (i == 0 && HandleHorizontalSlope(ref deltaMovement, Vector2.Angle(rayCastHit.normal, Vector2.up), isGoingRight))
                break;

            deltaMovement.x = rayCastHit.point.x - rayVector.x;
            rayDistance = Mathf.Abs(deltaMovement.x);

            if(isGoingRight)
            {
                deltaMovement.x -= skimWidth;
                State.isCollidingRight = true;            
            }
            else
            {
                deltaMovement.x += skimWidth;
                State.isCollidingLeft = true;
            }

            if (rayDistance < skimWidth + 0.0001f)
                break;

        }

    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        var isGoingUp = deltaMovement.y > 0;
        var rayDistance = Mathf.Abs(deltaMovement.y) + skimWidth;
        var rayDirection = isGoingUp ? Vector2.up : -Vector2.up;
        var rayOrigin = isGoingUp ? _rayCastTopLeft : _rayCastBottomLeft;

        rayOrigin.x += deltaMovement.x;

        var standingOnDistance = float.MaxValue;
        for (var i = 0; i < totalVerticalRays; i++)
        {
            var rayVector = new Vector2(rayOrigin.x + (i * _horizontalDistanceBetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            var raycastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask);
            if (!raycastHit)
                continue;

            if (!isGoingUp)
            {
                var verticalDistanceToHit = _transform.position.y - raycastHit.point.y;
                if (verticalDistanceToHit < standingOnDistance)
                {
                    standingOnDistance = verticalDistanceToHit;
                    StandingOn = raycastHit.collider.gameObject;
                }
            }        

            deltaMovement.y = raycastHit.point.y - rayVector.y;
            rayDistance = Mathf.Abs(deltaMovement.y);

            if (isGoingUp)
            {
                deltaMovement.y -= skimWidth;
                State.isCollidingAbove = true;
            }
            else
            {
                deltaMovement.y += skimWidth;
                State.isCollidingBelow = true;
            }

            if (!isGoingUp && deltaMovement.y > .0001f)
                State.isMovingUpSlope = true;

            if (rayDistance < skimWidth + .0001f)
                break;
        }

    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        var center = (_rayCastBottomLeft.x + _rayCastBottomRight.x) / 2;
        var direction = -Vector2.up;

        var slopDistance = slopeLimitTangant * (_rayCastBottomRight.x - center);
        var slopeRayVector = new Vector2(center, _rayCastBottomLeft.y);

        Debug.DrawRay(slopeRayVector, direction * slopDistance, Color.yellow);

        var rayCastHit = Physics2D.Raycast(slopeRayVector, direction, slopDistance, PlatformMask);

        if (!rayCastHit)
            return;

        var isMovingDownSlope = Mathf.Sign(rayCastHit.normal.x) == Mathf.Sign(deltaMovement.x);
        if (!isMovingDownSlope)
            return;

        var angle = Vector2.Angle(rayCastHit.normal, Vector2.up);
        if (Mathf.Abs(angle) < 0.0001f)
            return;

        State.isMovingDownSlope = true;
        State.slopeAngle = angle;
        deltaMovement.y = rayCastHit.point.y - slopeRayVector.y;

    }

    private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
        if (Mathf.RoundToInt(angle) == 90)
            return false;

        if (angle > Parameters.slopeLimit)
        {
            deltaMovement.x = 0;
            return true;
        }

        if (deltaMovement.y > .07f)
            return true;

        deltaMovement.x += isGoingRight ? -skimWidth : skimWidth;
        deltaMovement.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * deltaMovement.x);
        State.isMovingUpSlope = true;
        State.isCollidingBelow = true;
        return true;
    }

}
