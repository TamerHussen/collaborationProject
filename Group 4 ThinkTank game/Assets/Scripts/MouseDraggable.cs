using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDraggable : MonoBehaviour
{
    private Vector3 offset;  // object movement and movement of mouse
    private bool isDragging = false;
    public float fixedZ = 10.0f;

    void OnMouseDown()
    {
        // record data when mouse down
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseUp()
    {
        // stop dragging
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // update object position，following mouse
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.z = fixedZ;
            transform.position = newPos;
        }
    }

    // get world position of mouse
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = fixedZ;  // make sure in front cam
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
