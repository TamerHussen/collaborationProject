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
    public Collider targetCollider;  // Target collider
    public bool isSnapped = false;   // Bool to check if snapped

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
        offset.z = 0; // keep z-axis fixed while dragging
        isDragging = true;
        isSnapped = false; // reset snap state
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (targetCollider != null)
        {
            // Check using 2D projection (ignore Z)
            Vector3 pos2D = new Vector3(transform.position.x, transform.position.y, targetCollider.transform.position.z);

            if (targetCollider.bounds.Contains(pos2D))
            {
                isSnapped = true;
                Destroy(gameObject); // Fully delete the object
                Debug.Log(gameObject.name + " is snapped and destroyed!");
            }
        }
    }



    void Update()
    {
        if (isDragging && !isSnapped) // Dragging without snapping
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.z = targetZ; // Keep the z-axis fixed
            transform.position = newPos;

            // Handle mouse scroll wheel to adjust the z-axis depth
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                targetZ += scroll * zStep; // Adjust z based on scroll
            }
        }

        // If snapped, lock the position to the target collider's position
        if (isSnapped)
        {
            transform.position = targetCollider.transform.position; // Snap to target collider
        }
    }

    // Convert mouse position to world position (Z axis depth is calculated)
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCam.transform.position.z - targetZ); // Calculate depth of camera
        return mainCam.ScreenToWorldPoint(mousePos);
    }
}
