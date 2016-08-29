using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class floorController : MonoBehaviour {

    private Transform floorTransform;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && this.gameObject.tag == "obstacle")
        {
            SceneManager.LoadScene("TestLevel");
        }            
    }



    // Use this for initialization
    void Start()
    {
        floorTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Crear una variable de velocidad que se comparta entre todos los objetos móviles, para de esta forma
        //ir aumentando la velocidad del juego progresivamente.
        Vector3 movement = new Vector3(0f, 0.0f, 0.0f);        
        floorTransform.position += movement * Time.deltaTime;

    }
        
}
