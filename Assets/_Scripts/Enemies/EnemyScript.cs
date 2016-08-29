using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyScript : MonoBehaviour {


    public float forwardMovementSpeed = 4.0f;
    public GameObject managers;
    public Slider trailSlider;
    public GameObject player, topCheck;


    private static bool cycle = false;
    private Rigidbody2D rigidBody;
    private Vector3 enemyPosition;
    private GuiDebugManager debugGui;
    private Player _player;
    private float x_distanceToPlayer, y_distanceToPlayer;
    private float y_distanToTopCheck;
    private float yDirection = -1;


    // Use this for initialization
    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        debugGui = managers.GetComponent<GuiDebugManager>();
        _player = player.GetComponent<Player>();
    }

    void Start()
    {
        InvokeRepeating("IncreaseSpeed", 5f, 7f);
    }


    // Update is called once per frame
    void Update()
    {
        x_distanceToPlayer = transform.position.x - player.transform.position.x;
        y_distanceToPlayer = transform.position.y - player.transform.position.y;

        y_distanToTopCheck = transform.position.y - topCheck.transform.position.y;    

        // if x_distanceToPlayer == postive no follow, else do follow him.
        enemyPosition = transform.position;
    
        #region Enemy Behavior

        if (y_distanceToPlayer > 0.5f)
            yDirection = -1;
        else
            yDirection = 1;

        if (x_distanceToPlayer < 0 && x_distanceToPlayer > -6)
        {
            enemyPosition.y += yDirection * 1.5f * Time.deltaTime;
        }       
        
        if (x_distanceToPlayer > 5)
        {

            if (transform.position.y <= 4.1f && transform.position.y > -4.1f && cycle == false)
            {
                enemyPosition.y += 1.5f * Time.deltaTime;
            }
            else
            {
                enemyPosition.y -= 1.5f * Time.deltaTime;
            }

            if (y_distanToTopCheck >= 0.1f)
                cycle = true;
            else if (y_distanToTopCheck <= -7.1f)
                cycle = false;            
        }

        debugGui.PrintDebug("TopCheck " + y_distanToTopCheck.ToString(), 2);
        debugGui.PrintDebug("cycle " + cycle.ToString(), 5);

        transform.position = enemyPosition;

        #endregion
    }

    void FixedUpdate()
    {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = forwardMovementSpeed;
        rigidBody.velocity = newVelocity;
        trailSlider.value = transform.position.x;      
    }

    void IncreaseSpeed()
    {
        forwardMovementSpeed += 0.3f;
    }
}
