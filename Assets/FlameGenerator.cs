using UnityEngine;
using System.Collections;

public class FlameGenerator : MonoBehaviour {

    public GameObject FlamePrefab;
    public float lowerLimit = 0;
    public float upperLimit = 200;
    public float flameDelay = 5.0f;
    
    private FlameScript flameInstance;

	// Use this for initialization
	void Start () {
        flameInstance = FlamePrefab.GetComponent<FlameScript>();
        StartCoroutine("SpamFlames");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator SpamFlames()
    {
        while (true)
        {
            float randomX = 0;
            randomX = Random.Range(lowerLimit, upperLimit);
            if (flameInstance != null)
            {
                Instantiate(flameInstance, new Vector3(randomX, transform.position.y, 0.0f), Quaternion.identity);
            }
            yield return new WaitForSeconds(flameDelay);
        }
    }
}
