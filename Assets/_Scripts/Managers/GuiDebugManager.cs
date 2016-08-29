using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuiDebugManager : MonoBehaviour {


    public Text[] text;
   // public GameObject debugGroup;

    private bool isDebug = false;
    private bool previousIsDebug;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = "";
        }
    }

    void Update()
    {

       
     
    }
	
	public void PrintDebug(string debugText, int pos)
    {
        text[pos].text = debugText;
    }

}
