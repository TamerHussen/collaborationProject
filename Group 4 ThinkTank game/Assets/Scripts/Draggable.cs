using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // first touch point

            if (touch.phase == TouchPhase.Began)
            {
                // When start touching, record the movement
                if (IsTouchOnObject(touch))
                {
                    offset = transform.position - GetTouchWorldPos(touch);
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                transform.position = GetTouchWorldPos(touch) + offset;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // stop dragging when end
                isDragging = false;
            }
        }
    }

    bool IsTouchOnObject(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        return Physics.Raycast(ray, out hit) && hit.transform == transform;
    }

    // Get World pos during touch
    Vector3 GetTouchWorldPos(Touch touch)
    {
        Vector3 touchPos = touch.position;
        touchPos.z = 10f; // a suitable z value make sure that object keeping in front of camera
        return Camera.main.ScreenToWorldPoint(touchPos);
    }
}
