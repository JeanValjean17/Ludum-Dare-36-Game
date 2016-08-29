using UnityEngine;
using System.Collections;

public class ActivatePlatform : MonoBehaviour {

    public GameObject platform;

    void OnTriggerEnter2D(Collider2D coll)
    {
        platform.SetActive(true);
    }

}
