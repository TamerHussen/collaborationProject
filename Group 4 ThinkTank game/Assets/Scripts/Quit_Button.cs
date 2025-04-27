using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Quit_Button : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame()
    {
        /*EditorApplication.isPlaying = false;*/ // Editor version // Testing
        Application.Quit(); // Build version
    }

}
