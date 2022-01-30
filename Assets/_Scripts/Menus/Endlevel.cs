using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endlevel : MonoBehaviour
{

    public void NextLevel()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
    }
    
    public void QuitGame()
    {
        Debug.Log("Salir del juego! >:( ");
        Application.Quit();
    }
    
    
}
