using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
                
        }
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    { 
        FindObjectOfType<AudioManager>().PlaySound("Button");
       pauseMenuUI.SetActive(true);
       Time.timeScale = 0f;
       gameIsPaused = true;
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Debug.Log("Salir del juego! >:( ");
        Application.Quit();
    }
}
