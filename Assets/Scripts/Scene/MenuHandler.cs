using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
/// <summary>
/// Unity Menu Handler / Scene Handling.
/// </summary>
public class MenuHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Console.Log("Escape key pressed returning to main menu");
            MainMenu();
        }
	}

    public void StartGame()
    {
       // Console.Log("Loading staging scene.");
        SceneManager.LoadScene("Scenes/Main level - Staging");
    }

    public void MainMenu()
    {
      //  Console.Log("Loading Main Menu scene.");
        SceneManager.LoadScene("Scenes/Main Menu");
    }

    public void ExitGame()
    {
      //  Console.Log("Exiting the game");
        Application.Quit();
    }
}
