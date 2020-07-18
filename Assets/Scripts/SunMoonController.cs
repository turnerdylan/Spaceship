using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonController : MonoBehaviour
{
    public float SunRotationRate = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, SunRotationRate * Time.deltaTime);
        transform.LookAt(Vector3.zero);

    }
}
