using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeleteLevel : MonoBehaviour {

    int level;
    private SpriteRenderer sprite;
    private bool lerpAlphaSprite = false;
    private float lerpControl = 0;
    private float blinkDelay = 200f;
    private float playerHealth;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            
            LevelsContainer.sceneManager.LevelList.Remove(SceneManager.GetActiveScene().buildIndex);
            level = Random.Range(1, LevelsContainer.sceneManager.LevelList.Count + 1);
            if (SceneManager.GetActiveScene().buildIndex != 0)
                LevelsContainer.HealthPlayer = coll.gameObject.GetComponent<Player>().PlayerHealth;
            lerpAlphaSprite = true;
            StartCoroutine("LoadLevel");
        }
    }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (lerpAlphaSprite)
        {

            if (lerpControl < 1)
            {
                sprite.color = Color.Lerp(sprite.color, Color.clear, lerpControl);

                lerpControl += Time.deltaTime / blinkDelay;
            }            
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(7);
        if (LevelsContainer.sceneManager.LevelList.Count != 0)
        {
            LevelsContainer.HealthPlayer = 100.0f;
            SceneManager.LoadScene(LevelsContainer.sceneManager.LevelList[level - 1]);
        }
        else
            GameManager.Instance.LevelCompleted();
    }
}
