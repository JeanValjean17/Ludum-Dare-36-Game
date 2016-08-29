using UnityEngine;
using System.Collections;

public class SpikeActivator : MonoBehaviour {

    public GameObject spikes;
    public GameObject walls;
    private SpriteRenderer sprite;
    public bool ActivateTrap;

    private bool lerpColours = false;
    private float lerpControl = 0;
    private float blinkDelay = 0.51f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        walls.SetActive(true);
        spikes.GetComponent<SpikeScript>().isTrapActivated = ActivateTrap;
        coll.gameObject.GetComponent<Player>().isEndLevelZero = true;
        coll.gameObject.GetComponent<CharacterController2D>().movementEnabled = false;
        lerpColours = true;

    }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

     	
	// Update is called once per frame
	void Update () {
        if (lerpColours)
        {
            if (lerpControl < 1)
            {
                sprite.color = Color.Lerp(Color.white, Color.red, lerpControl);

                lerpControl += Time.deltaTime / blinkDelay;
            }
            if (lerpControl > 1)
            {
                lerpControl = 0;
            }
        }
    }
}
