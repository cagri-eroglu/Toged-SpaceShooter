using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
        gameSession.ResetGame();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        gameSession.ResetGame();
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
