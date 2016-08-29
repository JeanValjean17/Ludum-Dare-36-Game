using UnityEngine;
using System.Collections;
using System;

public class RotatoryMovement : MonoBehaviour {

    public float speed = 4.0f;
    private float smooth = 0;
    public Vector3 Destination;
    bool rotatingLeft = true;
    // Use this for initialization
    void Start () {
        StartCoroutine(Rotator());
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    IEnumerator Rotator()
    {
        smooth = speed * Time.deltaTime;

        if (rotatingLeft)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(180, new Vector3(0.0f, 0.0f, 1.0f)), smooth);
            
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(0, new Vector3(0.0f, 0.0f, 1.0f)), smooth);
        }
        
        if (transform.rotation.z == 1.0f)
        {
            rotatingLeft = false;
            yield return new WaitForSeconds(3);
        }
        else if (transform.rotation.z == 0.0f)
        {
            rotatingLeft = true;
            yield return new WaitForSeconds(3);
        }
    }

}
