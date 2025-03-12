using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Rotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, -rotationSpeed *  Time.deltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }


}
