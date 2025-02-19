using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject MainCanvas;

    public GameObject MainCamera;

    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCanvas.activeSelf == false)
        {
            LookAtObject(obj);
        }
    }

    void LookAtObject(GameObject target)
    {
        if (target != null)
        {
            MainCamera.transform.LookAt(target.transform.position); // face to object

            Vector3 offset = new Vector3(0, 2, -5);
            MainCamera.transform.position = target.transform.position + offset;
        }
    }
}
