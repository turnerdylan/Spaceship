using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float fov = 50;
    Camera shipCamera;
    //Ship ship;

    private void Start()
    {
        //ship = GetComponentInParent<Ship>();
        shipCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ship.PlayerShip != null)
        {
            fov = Ship.PlayerShip.Velocity.magnitude;
            shipCamera.fieldOfView = Mathf.Clamp(fov, 60, 100);
        }

        /*Vector3 mousePos = Input.mousePosition;
        Vector3 cameraRotation = new Vector3((mousePos.x / 38), mousePos.y / 22, 0f) - new Vector3(10, 10, 0);
        Quaternion rot = Quaternion.Euler(-cameraRotation);
        shipCamera.transform.localRotation = rot;
        Debug.Log("new values are: " + cameraRotation);*/

    }
}
