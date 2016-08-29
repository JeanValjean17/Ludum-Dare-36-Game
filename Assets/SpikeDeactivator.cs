using UnityEngine;
using System.Collections;

public class SpikeDeactivator : MonoBehaviour {

    public GameObject spikes;
    public bool ActivateTrap;

    void OnTriggerEnter2D(Collider2D coll)
    {
        spikes.GetComponent<DeactivateSpike>().isTrapActivated = ActivateTrap;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
