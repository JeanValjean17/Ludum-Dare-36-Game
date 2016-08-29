using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject targetObject;

    private float distanceToTarget;
    private float distanceToTargetY;
    // Use this for initialization
    void Start ()
    {
        distanceToTarget = transform.position.x - targetObject.transform.position.x;
        distanceToTargetY = transform.position.y - targetObject.transform.position.y;
        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x = 3;
        newCameraPosition.y = 3;
        transform.position = newCameraPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float targetObjectX = targetObject.transform.position.x;
        float targetObjectY = targetObject.transform.position.y;

        if (targetObjectX >= 0)
        {
            Vector3 newCameraPosition = transform.position;
            newCameraPosition.x = targetObjectX + distanceToTarget;
            //if (targetObjectY > -4 && targetObjectY < 15)
            //    newCameraPosition.y = targetObjectY;
            transform.position = newCameraPosition;
        }

    }
}
