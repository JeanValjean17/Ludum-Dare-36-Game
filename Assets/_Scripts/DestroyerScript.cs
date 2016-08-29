using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

    public GameObject spawnerPosition;
    public GameObject[] obj;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":               
                SceneManager.LoadScene("TestLevel");
                return;
            case "floor":
                Destroy(other.gameObject);
                Instantiate(obj[Random.Range(0, obj.Length)], 
                    (transform.position + new Vector3(35f, -transform.position.y, 0f)), Quaternion.identity);
                //spawnerPosition.GetComponent<tileSpawner>().Spawn();
                break;
            default:
                Destroy(other.gameObject);
                break;
        }      
        
    }

}
