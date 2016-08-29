using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {

    public bool isTrapActivated = false;

    private float speed = 10.0f;
    private float smooth = 0;
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("ActivateTrap");
	}

    IEnumerator ActivateTrap()
    {
        smooth = speed * Time.deltaTime;
        if (isTrapActivated)
        {
            yield return new WaitForSeconds(5);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.0f, transform.position.z), smooth);
        }
    }
}
