using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    public float speed = 4.0f;
    private float smooth = 0;
    public Vector3 Destination, Origin;
    bool movingRight = true;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        smooth = speed * Time.deltaTime;
        
        if (movingRight)
            transform.position = Vector3.MoveTowards(transform.position, Destination, smooth);
        else
            transform.position = Vector3.MoveTowards(transform.position, Origin, smooth);

        if (transform.position.x == Destination.x)
            movingRight = false;
        else if (transform.position.x == Origin.x)
            movingRight = true;

        
    }
}
