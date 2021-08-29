using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void MenuButton()
    {
        Debug.Log("Still need to add in a Menu");
    }
    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; 
#else
		    Application.Quit();
#endif
    }

}
