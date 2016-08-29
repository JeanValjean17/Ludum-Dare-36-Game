using UnityEngine;
using System.Collections;

public class tileSpawner : MonoBehaviour {

    public GameObject[] obj;
    public float spawnTime = 0;
    public bool isFloor = false;
    public float minRandomY = 0f;
    public float maxRandomY = 0f;


    private float randomDeltaTime = 0f;
    private float randomDeltaY = 0f;
    private float randomDeltaX = 0f;

    // Use this for initialization
    //void Start()
    //{
    //    Spawn();
    //}


    public void Spawn()
    {

        /*if (!isFloor)
        {
            randomDeltaTime = Random.Range(-2f, 1f);
            randomDeltaY = Random.Range(minRandomY, maxRandomY);
            randomDeltaX = Random.Range(-2f, 2f);
        }
        else
        {
            randomDeltaTime = 0f;
            randomDeltaY = 0f;
        }*/

        Instantiate(obj[Random.Range(0, obj.Length)], 
            (transform.position + new Vector3(0f, 0f, 0f)), Quaternion.identity);
       //Invoke("Spawn", 0.77f);
    }

}
