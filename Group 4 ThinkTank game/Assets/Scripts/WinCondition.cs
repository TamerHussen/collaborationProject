using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // for UI
using UnityEngine.SceneManagement; // for scene management

public class WinCondition : MonoBehaviour
{
    public List<MouseDraggable> draggableParts;  // store all draggable parts
    public Transform[] targetPositions;          // store pos

    public GameObject winPanel;                  // Panel of win
    public GameObject losePanel;                 // Panel of lose

    public Timer timer; // Pass isTimeOut variable into here.

   

    private bool hasGameEnded = false; // Flag to prevent multiple game ends

    void Start()
    {
        winPanel.SetActive(false); // Init Win Panel
        losePanel.SetActive(false); // Init Lose Panel
    }

    void Update()
    {
        // Only check for win/lose if the game hasn't ended yet
        if (hasGameEnded)
            return;

        // Check if time runs out
        if (timer.isTimeOut)
        {
            LoseGame();
        }
        else if (CheckWinCondition())  // Check win condition only when time is not out
        {
            StartCoroutine(WinGameDelayed()); // Call coroutine instead of direct function
        }
    }

    // double check each part got snapped
    bool CheckWinCondition()
    {
        foreach (MouseDraggable part in draggableParts)
        {
            if (!part.isSnapped)  // return false if any didn't snap
            {
                return false;
            }
        }

        return true;  // return true when all parts are snapped
    }

    // Win Condition with Delay
    IEnumerator WinGameDelayed()
    {
        hasGameEnded = true;  // Set game ended flag to prevent re-triggering
        timer.StopTimer();
        yield return new WaitForSeconds(20f); // Wait 2 seconds before showing win screen
        winPanel.SetActive(true);  // Show win panel
        Time.timeScale = 0;        // Stop game
        
    }

    // Lose condition (Time out)
    void LoseGame()
    {
        hasGameEnded = true;  // Set game ended flag to prevent re-triggering
        losePanel.SetActive(true); // Show lose panel
        Time.timeScale = 0;        // Stop game
    }

    // Button function to load next scene
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadNextScene2()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 2);
    }

    public void LoadNextScene3()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 3);
    }
}
