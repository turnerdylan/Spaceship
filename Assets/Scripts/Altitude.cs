using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altitude : MonoBehaviour
{
    public float minimumDrawLineDistance = 30;
    public float altitude;
    RaycastHit hit;
    LineRenderer lr;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        SetupLine();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
        //Debug.Log("Altitude is " + hit.distance);
        altitude = hit.distance;

        if(altitude < minimumDrawLineDistance)
        {
            UpdateLine();
        }
    }

    void SetupLine()
    {
        lr.sortingLayerName = "OnTop";
        lr.sortingOrder = 5;
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        lr.startWidth = .2f;
        lr.endWidth = .2f;
        lr.useWorldSpace = true;
        
    }

    void UpdateLine()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hit.point);
    }
}
