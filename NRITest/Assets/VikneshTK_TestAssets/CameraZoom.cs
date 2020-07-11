using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public Slider slider;
    public float zoomMultiplier;    //Rate at which the camera zooms in. The more the value the higher it get close to the object

    public float higherFov;         //These two variables can be used when we use Mathf.lerp function to adjust the FOV for zooming.    
    public float lowerFov;

    private Vector3 camPosition;
    private float currentZoom;      //current camera zoom value. This is a default zoom, which is literally zero.
    // Start is called before the first frame update
    void Start()
    {
       /* camPosition = transform.position;       //Disbaled this only because we are using Mathf.Lerp function to access the fov to adjust the zoom.
        currentZoom = transform.position.z;*/
    }

    // Update is called once per frame
    void Update()
    {
      /*  float zoom = currentZoom + (slider.value * zoomMultiplier); //Calculating the zoom value from the slider multiplied with zoom multiplier.
        camPosition.z = zoom;                                       //Assigning the calculated zoom to the camera z position
        transform.position = camPosition;*/

        Camera.main.fieldOfView = Mathf.Lerp(higherFov, lowerFov, slider.value); //One line replacement to the above three lines by accessing the field of view of the camera component with same zoom
    }
}
