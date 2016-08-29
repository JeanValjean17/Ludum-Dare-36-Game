using UnityEngine;
using System.Collections;

public class DeactivateSpike : MonoBehaviour {

    public bool isTrapActivated = true;

    private float speed = 10.0f;
    private float smooth = 0;

    // Update is called once per frame
    void Update()
    {
        smooth = speed * Time.deltaTime;
        if (!isTrapActivated)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -1.0f, transform.position.z), smooth);
        }
    }
}

