using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreManager : MonoBehaviour {

    [HideInInspector]
    public static int score;



    public Text text;
	// Use this for initialization
	void Awake ()
    {
       // score = 0;     
	}
	///Comentario De Prueba
	// Update is called once per frame
	void Update ()
    {
        //NADA QUE DECIR ACÁ
        //text.text = "Money: " + score.ToString();
    }
   
}
