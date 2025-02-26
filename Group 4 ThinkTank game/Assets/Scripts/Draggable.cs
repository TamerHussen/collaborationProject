using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private int activeFingerId = -1;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main; // 获取当前的摄像机
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (IsTouchOnObject(touch))
                    {
                        offset = transform.position - GetTouchWorldPos(touch);
                        isDragging = true;
                        activeFingerId = touch.fingerId;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && isDragging && touch.fingerId == activeFingerId)
                {
                    transform.position = GetTouchWorldPos(touch) + offset;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == activeFingerId)
                {
                    isDragging = false;
                    activeFingerId = -1;
                }
            }
        }
    }

    bool IsTouchOnObject(Touch touch)
    {
        Ray ray = mainCam.ScreenPointToRay(touch.position);
        RaycastHit hit;
        return Physics.Raycast(ray, out hit) && hit.transform == transform;
    }

    Vector3 GetTouchWorldPos(Touch touch)
    {
        Vector3 touchPos = touch.position;
        touchPos.z = mainCam.WorldToScreenPoint(transform.position).z; // 使用物体相对于当前摄像机的 Z 轴位置
        return mainCam.ScreenToWorldPoint(touchPos);
    }
}
