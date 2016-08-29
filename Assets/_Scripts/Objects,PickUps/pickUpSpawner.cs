using UnityEngine;
using System.Collections;

public class pickUpSpawner : MonoBehaviour {

    public GameObject[] obj;
    public float spawnTime = 0;
    public float minRandomY = 0f;
    public float maxRandomY = 0f;
    public float minRandomX = 0f;
    public float maxRandomX = 0f;


    private float randomDeltaTime = 0f;
    private float randomDeltaY = 0f;
    private float randomDeltaX = 0f;

    // Use this for initialization
    void Start()
    {
        Spawn();
    }

    
    void Spawn()
    {        
        randomDeltaTime = Random.Range(-0.3f, 0.8f);
        randomDeltaY = Random.Range(minRandomY, maxRandomY);
        randomDeltaX = Random.Range(minRandomX, maxRandomX);
        

        Instantiate(obj[Random.Range(0, obj.Length)], 
            (transform.position + new Vector3(randomDeltaX, randomDeltaY, 0f)), Quaternion.identity);
        Invoke("Spawn", spawnTime + randomDeltaTime);
    }

}
