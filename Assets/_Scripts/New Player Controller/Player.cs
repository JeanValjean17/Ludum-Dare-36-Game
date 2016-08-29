using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private bool _isFacingRight;
    [HideInInspector]
    public CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;
    private bool doubleJump = false;
    private bool touchJump = false;
    private GameManager gm;
    private GuiDebugManager debugGui;
    private LevelsContainer LevelContainer;
    private Animator animator;
    private BoxCollider2D _collider;

    public bool isEndLevelZero = false;
    public float PlayerHealth;
    public Slider healthSlider;
    public float maxSpeed;
    public float speedAccelerationOnGround = 10f;
    public float speedAccelerationInAir = 5f;
    public GameObject managers, gameOverManager;

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "PickUpObject":
                scoreManager.score += 10;
                break;
            case "obstacle":
                //SceneManager.LoadScene("TestLevel");                
                PlayerHealth -= 10.0f;
                var movementFactor = _controller.State.isGrounded ? speedAccelerationOnGround : speedAccelerationInAir;

                float facingSide = 0.0f;
                if (_isFacingRight)
                    facingSide = -1.0f;
                else
                    facingSide = 1.0f;
                _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, 200 * facingSide, Time.deltaTime * movementFactor));
                StartCoroutine("HandleDeath");
                //gm.GameOver();
                break;
            case "FallDetect":
                PlayerHealth = 0.0f;
                StartCoroutine("HandleDeath");
                break;
            case "Rock":
                PlayerHealth -= 10.0f;
                StartCoroutine("HandleDeath");
                break;
            case "Spikes":
                if (!_controller.isDead)
                {
                    PlayerHealth -= 10000.0f; //It's super effective.
                    if (LevelsContainer.sceneManager != null)
                        LevelsContainer.sceneManager.isLevelZero = false;
                    StartCoroutine("HandleDeath");
                }
                break;
            case "Water":
                if (PlayerHealth <= 60)
                    PlayerHealth += 40;
                else if (PlayerHealth > 60)
                    PlayerHealth += (100 - PlayerHealth);
                break;
            case "Flame":
                PlayerHealth -= 5.0f;
                if (!_controller.isDead)
                {
                    Destroy(coll.gameObject);
                    StartCoroutine("HandleDeath");
                }
                break;
            case "StrangeDiamond":
                Destroy(coll.gameObject);
                break;
        }
    }

    

    public void Start()
    {

        PlayerHealth = LevelsContainer.HealthPlayer;           
        _controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        LevelContainer = new LevelsContainer();
        //debugGui = managers.GetComponent<GuiDebugManager>();
        gm = gameOverManager.GetComponent<GameManager>();
        _isFacingRight = transform.localScale.x > 0;
        healthSlider.value = PlayerHealth;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            LevelsContainer.sceneManager.isLevelZero = true;
        else
            LevelsContainer.sceneManager.isLevelZero = false;
    }

    public void Update()
    {
        HandleInput();
        var movementFactor = _controller.State.isGrounded ? speedAccelerationOnGround : speedAccelerationInAir;
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * maxSpeed, Time.deltaTime * movementFactor));
        healthSlider.value = PlayerHealth;        
    }

    public void FixedUpdate()
    {
        if (_controller.isDead)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void HandleInput()
    {
        #region Para manejo con teclado
        if(Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if(!_isFacingRight)
            {
                Flip();
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();
            //else
            //    _normalizedHorizontalSpeed = 0;
        }
        else
            _normalizedHorizontalSpeed = 0;        
        #endregion

        //_normalizedHorizontalSpeed = 1;
        //debugGui.PrintDebug("Velocity Y : " + _controller.Velocity.y.ToString(), 0);        
        //debugGui.PrintDebug("Can Jump : " + _controller.CanJump.ToString(), 1);
        //debugGui.PrintDebug("Grounded: " + _controller.State.isGrounded.ToString(), 3);

        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
                touchJump = true;
            else
                touchJump = false;
        }

        if (_controller.CanJump && (Input.GetKeyDown(KeyCode.Space) || touchJump) && doubleJump == false)
        {
            _controller.Jump();
            doubleJump = true;
        }
        else if (!_controller.State.isGrounded && (Input.GetKeyDown(KeyCode.Space) || touchJump) && doubleJump == true)
        {
            _controller.Jump();
            doubleJump = false;
        }

        else if (_controller.State.isGrounded)//|| _controller.Velocity.y < 0
            doubleJump = false;

    }

    private void Flip()
    {
        if (!_controller.isDead || _controller.movementEnabled)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isFacingRight = transform.localScale.x > 0;
        }
    }

    IEnumerator HandleDeath()
    {
        if (!_controller.isDead)
        {
            if ((SceneManager.GetActiveScene().buildIndex != 0 && LevelsContainer.sceneManager.LevelList.Count != 1) || isEndLevelZero)
            {
                int level;
                if (PlayerHealth <= 0.0f)
                {
                    _controller.movementEnabled = false;
                    _controller.isDead = true;
                    animator.SetTrigger("isDead");
                    LevelsContainer.HealthPlayer = 100.0f;
                    while (true)
                    {
                        level = Random.Range(1, LevelsContainer.sceneManager.LevelList.Count + 1);
                        if (SceneManager.GetActiveScene().buildIndex != LevelsContainer.sceneManager.LevelList[level - 1])
                            break;
                    }
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                        yield return new WaitForSeconds(5);

                    SceneManager.LoadScene(LevelsContainer.sceneManager.LevelList[level - 1]);       
                    
                }
            }
            else
            {
                if (PlayerHealth <= 0.0f)
                {
                    LevelsContainer.HealthPlayer = 100.0f;
                    _controller.movementEnabled = false;
                    animator.SetTrigger("isDead");
                    _controller.isDead = true;                                     
                    yield return new WaitForSeconds(1);
                    gm.GameOver();
                }
                
            }
        }
    }
}

