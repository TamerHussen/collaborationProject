using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public List<MouseDraggable> draggableParts;
    public Transform[] targetPositions;

    public GameObject winPanel;
    public GameObject losePanel;

    public Timer timer;

    private bool hasGameEnded = false;

    void Start()
    {
        Time.timeScale = 1; // Ensure game resumes when scene loads
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (hasGameEnded)
            return;

        if (timer.isTimeOut)
        {
            LoseGame();
        }
        else if (CheckWinCondition())
        {
            StartCoroutine(WinGameDelayed());
        }
    }

    bool CheckWinCondition()
    {
        foreach (MouseDraggable part in draggableParts)
        {
            if (!part.isSnapped)
                return false;
        }
        return true;
    }

    IEnumerator WinGameDelayed()
    {
        hasGameEnded = true;
        timer.StopTimer(); // Pause only the timer
        yield return new WaitForSeconds(2f); // Let game keep running briefly
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Fully pause game after showing win panel
    }

    void LoseGame()
    {
        hasGameEnded = true;
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1; // Resume time for next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadNextScene2()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 2);
    }

    public void LoadNextScene3()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 3);
    }
}
