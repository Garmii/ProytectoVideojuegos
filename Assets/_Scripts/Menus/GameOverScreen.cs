using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI text;
    int score;
    
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        text.text = score.ToString() + " MONEDAS";
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego! >:( ");
        Application.Quit();
    }
}
