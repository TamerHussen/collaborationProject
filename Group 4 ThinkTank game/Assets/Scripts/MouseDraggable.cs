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
    public Collider targetCollider;  // 目标触发器
    public bool isSnapped = false;   // bool check if snap
    public string partName;          // 部件名称，用于检查是否为正确的部件

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

        // Check if part name matches and it is within the trigger
        if (targetCollider != null && targetCollider.bounds.Contains(transform.position) && gameObject.name == partName)
        {
            isSnapped = true;
            gameObject.SetActive(false); // Hide the part when it is correctly placed
            Debug.Log(gameObject.name + " is snapped and hidden!");
        }
    }

    void Update()
    {
        if (isDragging && !isSnapped) // moving and not snapped yet
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.z = targetZ; // force z axis
            transform.position = newPos;

            // Handle mouse scroll wheel to adjust the z-axis depth
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                targetZ += scroll * zStep; // add or decrease z
            }
        }

        // Snapped -> unmovable
        if (isSnapped)
        {
            transform.position = targetCollider.transform.position; // Ensure the part is "snapped" to the trigger area
        }
    }

    // Convert mouse position to world position
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCam.transform.position.z - targetZ); // Calculate depth of camera
        return mainCam.ScreenToWorldPoint(mousePos);
    }
}
