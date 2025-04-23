using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Rotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public bool canRotate = false;

    public void RotateLeft()
    {
        Debug.Log("RotateLeft clicked. canRotate: " + canRotate);
        if (canRotate)
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }

    public void RotateRight()
    {
        if (canRotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }


}
