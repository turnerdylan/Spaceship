using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHS;

public class CameraStateController : MonoBehaviour
{
    public Camera shipCamera;
    public Camera FPSCamera;
    LandingController lc;
    FirstPersonController fpc;
    //Camera hoverbikeCam;

    void Start()
    {
        lc = FindObjectOfType<LandingController>();
        fpc = FindObjectOfType<FirstPersonController>();
        fpc.gameObject.SetActive(false);

    }

    void Update()
    {
        if(lc.shipState == ShipState.Flying)
        {
            fpc.gameObject.transform.position = lc.transform.position;
            fpc.gameObject.transform.parent = lc.transform;
        }
        else if (lc.shipState == ShipState.Landed)
        {
            shipCamera.enabled = false;
            FPSCamera.enabled = true;
            fpc.gameObject.SetActive(true);
            fpc.gameObject.transform.parent = null;
        }
        else if (lc.shipState == ShipState.TakingOff)
        {
            shipCamera.enabled = true;
            FPSCamera.enabled = false;
            fpc.gameObject.SetActive(false);
            fpc.gameObject.transform.position = lc.transform.position;
        }
    }

    private void DisableUI()
    {

    }
}
