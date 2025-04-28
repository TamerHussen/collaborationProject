using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // for UI
using UnityEngine.SceneManagement; // for scene management

public class WinCondition : MonoBehaviour
{
    public List<MouseDraggable> draggableParts;  // store all draggable parts
    public GameObject winPanel;                  // Panel of win
    public GameObject losePanel;                 // Panel of lose
    public GameObject Model;

    public Timer timer; // Pass isTimeOut variable into here.

    private bool hasGameEnded = false; // Flag to prevent multiple game ends

    public Model_Rotation model_Rotation;

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

    // Check if all parts are snapped and correctly placed by their name
    bool CheckWinCondition()
    {
        foreach (MouseDraggable part in draggableParts)
        {
            if (!part.isSnapped)
            {
                return false; // As long as one part isn't snapped, not a win
            }
        }

        return true; // All parts are snapped
    }


    // Win Condition with Delay
    IEnumerator WinGameDelayed()
    {
        hasGameEnded = true;  // Set game ended flag to prevent re-triggering
        timer.StopTimer();

        model_Rotation.canRotate = true;

        yield return new WaitForSeconds(15f); // Wait 2 seconds before showing win screen
        Destroy(Model);
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
    public void LoadNextScene1()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("First_Game_Scene");
    }

    public void LoadNextScene2()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Second_game_scene");
    }

    public void LoadNextScene3()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Third_game_scene");
    }

    public void LoadNextScene4()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("fourth_game_scene");
    }
}
