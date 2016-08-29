using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyInteraction : MonoBehaviour {

    public GameObject enemy;

    private EnemyScript _enemy;
    private HealthEnemy enemyHealth;
    private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {            
            case "Water":
                if (_enemy.forwardMovementSpeed - 0.4f > 0)
                    _enemy.forwardMovementSpeed -= 0.4f;
                else
                    _enemy.forwardMovementSpeed = 0f;
                break;
            case "Enemies":
                //gameManager.GameOver();
                SceneManager.LoadScene("TestLevel");
                return;
            case "Dopm":
                gameManager.enemyDestroyerCharge += 25;
                Debug.Log(gameManager.enemyDestroyerCharge.ToString());
                //enemyHealth.TakeDamage(50);
                Destroy(coll.gameObject);
                break;
        }
    }

    void Awake()
    {
        gameManager = GameManager.Instance;
        _enemy = enemy.GetComponent<EnemyScript>();
        enemyHealth = enemy.GetComponent<HealthEnemy>();
    }

}
