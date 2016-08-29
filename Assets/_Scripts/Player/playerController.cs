using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerController : MonoBehaviour {


    public float upwardsForce = 0f;
    public float dblJmpForceConstraint = 0f;
    public float forwardMovementSpeed = 3.0f;
    public Slider trailSlider;
    public GameObject managers;
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.


    private Animator animator;
    private Rigidbody2D rigidBody;
    private Transform groundCheck, playerPosition, obstacleCheckStart, obstacleCheckEnd;
    private bool grounded = false;			// Whether or not the player is grounded.
    private bool obstacleDetected = false;
    private bool doubleJump = false;
    private int doubleJumpConstraint = 0;
    private Vector3 movement;
    private GuiDebugManager debugGui;
    private Vector3 movForce;

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "PickUpObject":
                scoreManager.score += 10;
                break;
        }
    }

    // Use this for initialization
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody2D>();
        debugGui = managers.GetComponent<GuiDebugManager>();       
    }

    void Start()
    {
        animator.SetBool("isMoving", false);
        groundCheck = transform.Find("groundCheck");
        obstacleCheckStart = transform.Find("obstacleCheckStart");
        obstacleCheckEnd = transform.Find("obstacleCheckEnd");
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //obstacleDetected = Physics2D.Linecast(obstacleCheckStart.position, obstacleCheckEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        //Debug.DrawLine(obstacleCheckStart.position, obstacleCheckEnd.position, Color.red);
        //debugGui.PrintDebug("grounded : " + grounded.ToString(), 1);
        if (Input.GetButtonDown("Jump") && doubleJump == false) //GetButtonDown; "space", GetKeyDown; "Jump"
            jump = true;            

        if (grounded)
            doubleJump = false;

    }

    void FixedUpdate()
    {
        ChangeSpeed(Input.GetAxisRaw("Horizontal"));
        //debugGui.PrintDebug("Horizontal Input : " + h.ToString(), 1);

        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = forwardMovementSpeed;
        rigidBody.velocity = newVelocity;

        

        if ((grounded || !doubleJump) && jump)
        {
            if (doubleJump)
            {
                doubleJumpConstraint = 1;
                rigidBody.velocity = new Vector3(0, 0, 0);
            }
            else
                doubleJumpConstraint = 0;

            rigidBody.AddForce(new Vector2(0f, upwardsForce - (doubleJumpConstraint * dblJmpForceConstraint)));                        
            jump = false;

            if (!grounded)
                doubleJump = true;
        }
       
        if (forwardMovementSpeed == 0f)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        trailSlider.value = transform.position.x;
    }

    void ChangeSpeed(float h)
    {        

        if ((forwardMovementSpeed + h * 0.2f) >= 1 && (forwardMovementSpeed + h * 0.2f) <= 4)
            forwardMovementSpeed += h * 0.2f;
    }

}
