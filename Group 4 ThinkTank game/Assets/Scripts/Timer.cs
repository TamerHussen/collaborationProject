using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft = 120f;
    public bool isRunning = true;
    public TMP_Text timeText;

    public bool isTimeOut = false;

    void Start()
    {
        isTimeOut = false;
        isRunning = true;
        DisplayTime(timeLeft);
    }

    void Update()
    {
        if (isRunning && !isTimeOut)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {
                timeLeft = 0;
                isTimeOut = true;
                isRunning = false;
            }
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        if (!isTimeOut)
            isRunning = true;
    }

    public void ResetTimer(float newTime)
    {
        timeLeft = newTime;
        isTimeOut = false;
        isRunning = true;
        DisplayTime(timeLeft);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
