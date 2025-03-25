using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // for UI

public class WinCondition : MonoBehaviour
{
    public List<MouseDraggable> draggableParts;  // store all draggable parts
    public Transform[] targetPositions;          // store pos

    public GameObject winPanel;                  // Panel of win
    public GameObject losePanel;                 // Panel of lose

    public Timer timer; // Pass isTimeOut variable into here.
    void Start()
    {
        winPanel.SetActive(false); // Init Win Panel
        losePanel.SetActive(false); // Init Lose Panel
    }

    void Update()
    {
        // check time condition
        if (timer.isTimeOut == true)
        {
            LoseGame();
        }

        // check win condition
        CheckWinCondition();
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

        return true;  // return true
    }

    // Win Condition
    void WinGame()
    {
        winPanel.SetActive(true);  // show win panel
        Time.timeScale = 0;        // stop game
    }

    // Lose condition (Time out)
    void LoseGame()
    {
        losePanel.SetActive(true); // show lose panel
        Time.timeScale = 0;        // stop game
    }
}
