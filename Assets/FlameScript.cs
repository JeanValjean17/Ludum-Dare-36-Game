using UnityEngine;
using System.Collections;

public class FlameScript : MonoBehaviour {

    public float speed = 4.0f;
    private float smooth = 0;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "FallDetect")
            Destroy(this);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        smooth = speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -1.0f * speed, 0.0f);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -200.0f, transform.position.z), smooth);

    }
}
