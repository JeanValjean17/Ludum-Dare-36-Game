using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TrapDetector : MonoBehaviour
{
    public List<GameObject> rocksTrap = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            foreach (GameObject rock in rocksTrap)
            {
                if (rock != null)
                {
                    Rigidbody2D rockRigidBody = rock.GetComponent<Rigidbody2D>();
                    rockRigidBody.isKinematic = false;
                }
            }       
        }
    }
}
