using UnityEngine;
using System.Collections;

public class FallingSpikes : MonoBehaviour {

    public float speed = 10.0f;
    private float smooth = 0;
    public bool isTrapActivated = false;
    // Update is called once per frame
    void Update()
    {
        smooth = speed * Time.deltaTime;
        if (isTrapActivated)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -100.0f, transform.position.z), smooth);
        }

    }
}
