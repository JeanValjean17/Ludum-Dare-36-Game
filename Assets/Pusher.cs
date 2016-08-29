using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {

    public float speed = 4.0f;
    private float smooth = 0;
    private Player playerInstance;
    public Vector3 Destination, Origin;
    bool movingRight = true;
   

    void OnTriggerEnter2d(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            int movementDirection = 0;
            if (movingRight)
                movementDirection = 1;
            else
                movementDirection = -1;

            playerInstance = coll.gameObject.GetComponent<Player>();
            playerInstance._controller.SetHorizontalForce(Mathf.Lerp(playerInstance._controller.Velocity.x, movementDirection * speed, 
                Time.deltaTime * smooth));
        }
    }


    // Update is called once per frame
    void Update()
    {
        //smooth = speed * Time.deltaTime;

        //if (movingRight)
        //    transform.position = Vector3.MoveTowards(transform.position, Destination, smooth);
        //else
        //    transform.position = Vector3.MoveTowards(transform.position, Origin, smooth);

        //if (transform.position.x == Destination.x)
        //    movingRight = false;
        //else if (transform.position.x == Origin.x)
        //    movingRight = true;


    }
}
