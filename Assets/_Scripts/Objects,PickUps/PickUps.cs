using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour {

    private Transform pickUpTransform;

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            DestroyObject(this.gameObject);
        }
    }

}
