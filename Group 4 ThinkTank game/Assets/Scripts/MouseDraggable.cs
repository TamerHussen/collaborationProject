using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDraggable : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 offset;
    private bool isDragging = false;
    public float targetZ = 0f; // 固定 Z 轴
    public float zStep = 0.5f; // Z 轴变化的步长

    void Start()
    {
        mainCam = Camera.main;
        targetZ = transform.position.z; // 物体的初始 Z 轴
    }

    void OnEnable()
    {
        CameraManager.OnCameraMoved += UpdateCameraReference;
    }

    void OnDisable()
    {
        CameraManager.OnCameraMoved -= UpdateCameraReference;
    }

    void UpdateCameraReference()
    {
        mainCam = Camera.main; // 摄像机移动后更新引用
    }

    void OnMouseDown()
    {
        Vector3 mouseWorldPos = GetMouseWorldPos();
        offset = transform.position - mouseWorldPos;
        offset.z = 0; // 避免 Z 轴偏移
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.z = targetZ; // 强制固定 Z 轴
            transform.position = newPos;

            // 监听滚轮输入
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                targetZ += scroll * zStep; // 滚轮向上增加 Z，向下减少 Z
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCam.transform.position.z - targetZ); // 计算到摄像机的深度
        return mainCam.ScreenToWorldPoint(mousePos);
    }
}

