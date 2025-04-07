using UnityEngine;
using UnityEngine.SceneManagement;

public class Idle : MonoBehaviour
{
    public float TimeLimit = 120f;
    public float Timer = 0.0f;
    public Vector3 LastMousePos;


    void Start()
    {
        LastMousePos = Input.mousePosition;
    }

    void Update()
    {
        if (UserInput())
        {
            Timer = 0.0f;
        }
        else
        {
            Timer += Time.deltaTime;
        }

        if (Timer >= TimeLimit)
        {
            LoadMainMenu();
        }
    }


    bool UserInput()
    {
        if(Input.anyKey)
            return true;

        if (Input.mousePosition != LastMousePos)
        {
            LastMousePos = Input.mousePosition;
            return true;
        }

        return false;
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}