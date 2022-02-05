using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        SceneManager.LoadScene("Tutorial");
    }

    public void Settings()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Debug.Log("Ajustes");
        //SceneManager.LoadScene("Ajustes");
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Debug.Log("Salir del juego! >:( ");
        Application.Quit();
    }

}
