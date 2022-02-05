using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endlevel : MonoBehaviour
{

    public void NextLevel()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Time.timeScale = 1f;
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
