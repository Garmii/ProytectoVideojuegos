using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public float restartDelay = 3f;

    public GameOverScreen gameOverScreen;
    void Update()
    {

    }

    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
           gameOverScreen.Setup(FindObjectOfType<ScoreManager>().score);
        }
    }
}
