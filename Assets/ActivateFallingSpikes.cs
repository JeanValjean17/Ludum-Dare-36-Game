using UnityEngine;
using System.Collections;

public class ActivateFallingSpikes : MonoBehaviour {

    public GameObject spikes;
    public bool ActivateTrap;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            spikes.GetComponent<FallingSpikes>().isTrapActivated = ActivateTrap;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
