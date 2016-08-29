using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GuiManager : MonoBehaviour {

	public void RestartLevel()
    {
        SceneManager.LoadScene("Level00");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
