using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.Linq.Expressions;

public class Timer : MonoBehaviour
{

    public float timeLeft = 120f;
    public bool isRunning = true;
    public TMP_Text timeText;

    public bool isTimeOut = false;




    // Start is called before the first frame update
    void Start()
    {
        isTimeOut = false;
        DisplayTime(timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
                isTimeOut = false;
            }
            else
            {
                timeLeft = 0;
                isRunning = false;
                isTimeOut = true;
            }
        }

    }

    public void StopTimer()
    {
        isRunning = false;
        isTimeOut = true;
    }

    void DisplayTime (float timeToDisplay)
    {
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes , seconds);
    }
}
