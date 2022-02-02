using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Settings()
    {
        Debug.Log("Ajustes");
        //SceneManager.LoadScene("Ajustes");
    }

    public void Quit()
    {
        Debug.Log("Salir del juego! >:( ");
        Application.Quit();
    }

}
