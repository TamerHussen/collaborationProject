using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 用于交互UI元素

public class WinCondition : MonoBehaviour
{
    public List<MouseDraggable> draggableParts;  // store all draggable parts
    public Transform[] targetPositions;          // store pos

    public GameObject winPanel;                  // 胜利时显示的面板
    public GameObject losePanel;                 // 失败时显示的面板

    public Timer timer; // Pass isTimeOut variable into here.
    void Start()
    {
        winPanel.SetActive(false); // 初始化时隐藏胜利面板
        losePanel.SetActive(false); // 初始化时隐藏失败面板
    }

    void Update()
    {
        // check time condition
        if (timer.isTimeOut == true)
        {
            LoseGame();
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

        return true;  // return true
    }

    // Win Condition
    void WinGame()
    {
        winPanel.SetActive(true);  // 显示胜利面板
        Time.timeScale = 0;        // 暂停游戏
    }

    // Lose condition (Time out)
    void LoseGame()
    {
        losePanel.SetActive(true); // 显示失败面板
        Time.timeScale = 0;        // 暂停游戏
    }
}
