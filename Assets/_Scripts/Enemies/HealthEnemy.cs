using UnityEngine;
using System.Collections;

public class HealthEnemy : MonoBehaviour {

    
    private GuiDebugManager debugGui;
    private GameManager gm;
    private int startingHealth = 0;            // The amount of health the enemy starts the game with. Randomize!!!


    [HideInInspector]
    public int currentHealth;                   // The current health the enemy has.
    public GameObject managers, gameManager;
    public int minStartingHealth = 500;
    public int maxStartingHealth = 5000;
    
    [HideInInspector]
    public bool isDead;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "floor")
        {
            Destroy(coll.gameObject);
            TakeDamage(50);
        }
    }

    void Awake ()
    {
        debugGui = managers.GetComponent<GuiDebugManager>();
        gm = gameManager.GetComponent<GameManager>();
        startingHealth = Random.Range(minStartingHealth, (maxStartingHealth + 1));
        currentHealth = startingHealth;
        Debug.Log("Starting Health " + currentHealth);
        isDead = false;
    }

    void Update()
    {
        //debugGui.PrintDebug("Enemy's Health: " + currentHealth.ToString(), 4);

        if (currentHealth < 0)
        {
            //Destroy(this.gameObject);
            //gm.LevelCompleted();
            isDead = true;
        }    
    }
	
    public void TakeDamage(int amount)
    {
        
        if ((currentHealth > 0) && (isDead == false))
            currentHealth -= amount;
        
    }
}
