using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainCanvas;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (MainCanvas != null)
        {
            MainCanvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCanvas != null || MainCanvas == true)
        {
           if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainCanvas.SetActive(true);
            }
        }
    }

    public void startGame()
    {
        if (MainCanvas != null || mainCamera != null)
        {
            MainCanvas.SetActive(false);

            mainCamera.orthographic = false;
            mainCamera.fieldOfView = 60f;
        }
    }
}
