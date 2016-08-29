using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public GameObject player, finalLevelLandmark;
    //Canvas to display gameover screen.
    public GameObject Canvas;
    //The X distance that triggers the end of the level and activates the destroyer. 
    public int goal;
    [HideInInspector]
    //Charge recollected to destroy the enemy. DOPMS.
    public int enemyDestroyerCharge = 0;

    
    // Static singleton property
    private Vector3 playerPosition, enemyPosition;
    private bool isInstantiated = false;
    private HealthEnemy enemyHealth;
    private Vector3 crystalPosition;
    private bool isDamageTaken = false;
    private GameObject DestroyerInstance;
    private Animator anim;
    private GUIText gameOverText;
    static GameManager instance;


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        /* if (Instance != null && Instance != this)
         {
             Destroy(gameObject);
         }

         Instance = this;*/

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        //DontDestroyOnLoad(gameObject);
        anim = Canvas.GetComponent<Animator>();        
        //enemyHealth = enemy.GetComponent<HealthEnemy>();

        
    }

	// Update is called once per frame
	void Update ()
    {
        //playerPosition = player.transform.position;

        //if (playerPosition.x >= goal)
        //{
            //if (!isInstantiated)
            //{
            //    crystalPosition = new Vector3(player.transform.position.x + 50, 0, 0);
            //    DestroyerInstance = (GameObject) Instantiate(finalLevelLandmark, crystalPosition, Quaternion.identity);
            //    isInstantiated = true;
            //}

            //if (playerPosition.x > enemyPosition.x)
            //{
            //    if (playerPosition.x >= crystalPosition.x && isInstantiated && !isDamageTaken)
            //    {
            //        enemyHealth.TakeDamage(enemyDestroyerCharge);
            //        Debug.Log("Final Health " + enemyHealth.currentHealth);
            //        isDamageTaken = true;

            //        if (enemyHealth.currentHealth <= 0)
            //        {
            //            //Level finished successfully.
                        
            //            anim.SetTrigger("GameSuccess");
            //            //Load another level.

            //        }                        
            //        else if (enemyPosition.x >= crystalPosition.x)
            //        {                        
            //            Destroy(DestroyerInstance);
            //            anim.SetTrigger("GameOver");
            //        }

            //    }
            //}
            //else if (enemy != null && enemyPosition.x >= playerPosition.x)
            //{
            //    if (enemyPosition.x >= crystalPosition.x && isInstantiated)
            //    {                   
            //        Destroy(DestroyerInstance);
            //        anim.SetTrigger("GameOver");
            //    }
            //}                            
        //}
	}

    public void GameOver()
    {
        anim.SetTrigger("GameOver");        
    }

    public void LevelCompleted()
    {
        anim.SetTrigger("GameSuccess");
    }
}
