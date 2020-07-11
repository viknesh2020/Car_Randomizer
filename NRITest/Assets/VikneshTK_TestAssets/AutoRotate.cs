using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;    //Rotation speed of the Car object around y axis

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);  //Auto rotating around y axis with a speed per delta time.
    }
}
