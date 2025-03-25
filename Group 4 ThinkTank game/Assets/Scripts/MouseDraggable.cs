using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDraggable : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 offset;
    private bool isDragging = false;
    public float targetZ = 0f; // fix z-axis
    public float zStep = 1.0f; // step distance 

    // Snap function
    public Transform targetPosition;  // target pos
    public float snapDistance = 0.5f; // snap distance
    public bool isSnapped = false;   // bool check if snap

    void Start()
    {
        mainCam = Camera.main;
        targetZ = transform.position.z; // Origin z-axis
    }

    void UpdateCameraReference()
    {
        mainCam = Camera.main; // update cam pos
    }

    void OnMouseDown()
    {
        Vector3 mouseWorldPos = GetMouseWorldPos();
        offset = transform.position - mouseWorldPos;
        offset.z = 0; // keep z-axis
        isDragging = true;
        isSnapped = false; // reset bool when start
    }

    void OnMouseUp()
    {
        isDragging = false;

        // check if close enough to start auto snap
        Vector3 currentPos = transform.position;
        Vector3 targetPos = targetPosition.position;

        float distance = Vector2.Distance(new Vector2(currentPos.x, currentPos.y), new Vector2(targetPos.x, targetPos.y));

        if (distance < snapDistance)
        {
            transform.position = new Vector3(targetPos.x, targetPos.y, currentPos.z);
            isSnapped = true;
        }
    }

    void Update()
    {
        if (isDragging && !isSnapped) // moving and not snapping yet
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.z = targetZ; // furce z axis
            transform.position = newPos;

            // 监听滚轮输入
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                targetZ += scroll * zStep; // add or decrease z
            }
        }

        // snapped -> unmoveable
        if (isSnapped)
        {
            transform.position = targetPosition.position; // make sure obj stay in target pos
            Destroy(gameObject);
            Debug.Log("Part is snapped!");
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCam.transform.position.z - targetZ); // cal deepth of cam
        return mainCam.ScreenToWorldPoint(mousePos);
    }
}
