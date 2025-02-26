using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static event Action OnCameraMoved; // 事件通知

    public GameObject MainCanvas;
    public GameObject MainCamera;
    public GameObject obj;

    private bool hasMoved = false;

    void Update()
    {
        if (!MainCanvas.activeSelf && !hasMoved)
        {
            MoveCameraToObject(obj);
            hasMoved = true;

            // 触发事件，通知所有监听的脚本摄像机已更新
            OnCameraMoved?.Invoke();
        }

        // DEBUG
        //Debug.Log("Camera Position: " + MainCamera.transform.position); // 实时打印摄像机位置

        if (!MainCanvas.activeSelf && !hasMoved)
        {
            MoveCameraToObject(obj);
            hasMoved = true;
            OnCameraMoved?.Invoke();
        }
    }

    void MoveCameraToObject(GameObject target)
    {
        if (target != null)
        {
            Vector3 offset = new Vector3(0, 2, -5);
            MainCamera.transform.position = target.transform.position + offset;
            MainCamera.transform.LookAt(target.transform.position);
        }
    }
}
