using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VHS;

public enum ShipState { Flying, Landing, Landed, TakingOff}

public class LandingController : MonoBehaviour
{
    //references
    public ShipState shipState;
    Altitude al;
    ShipInput input;
    ShipPhysics physics;
    Quaternion lookRotation;
    Rigidbody rb;
    public Vector3 playerCockpit;
    public FirstPersonController fpc;

    //variables
    public float rotateSpeed;
    public float descendSpeed;
    public float landHeight = 2;
    public float takeoffHeight = 30;


    // Start is called before the first frame update
    void Start()
    {
        shipState = 0;

        al = GetComponent<Altitude>();
        input = GetComponent<ShipInput>();
        physics = GetComponent<ShipPhysics>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        playerCockpit = transform.position + new Vector3(0, 2, 0);
        if (Ship.PlayerShip == null)
        {
            Debug.LogWarning("No ship found");
            return;
        }

        switch (shipState)
        {
            case ShipState.Flying:
                //if flying low altitude and slowly
                if (Ship.PlayerShip.Velocity.magnitude < 30 && al.altitude < 50)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        shipState = ShipState.Landing;
                        DisengageUserControls();
                    }
                }
                break;

            case ShipState.Landed:
                FaceShipForward(new Vector3(0, transform.localRotation.y, 0));
                //if landed be ready for takeoff
                if (Input.GetKeyDown(KeyCode.Space) && CheckIfPlayerIsInCockpit())
                {
                    shipState = ShipState.TakingOff;
                }
                break;

            default:
                Debug.Log("ship in alternate state");
                break;
        }
    }

    private void FixedUpdate()
    {
        //print((playerCockpit - fpc.transform.position).magnitude);
        switch (shipState)
        {
            case ShipState.Landing:
                FaceShipForward(new Vector3(0, transform.localRotation.y, 0)); DeccelerateShip();
                if (rb.velocity.magnitude <= 2)
                {
                    DescendShip();
                }
                break;

            case ShipState.TakingOff:
                AscendShip();
                break;

            default:
                Debug.Log("ship in alternate state");
                break;
        }

    }

    //turns off input and physics
    private void DisengageUserControls()
    {
        input.throttle = 0;
        input.enabled = false;
        physics.enabled = false;
    }

    private void EngageUserControls()
    {
        input.enabled = true;
        physics.enabled = true;
        rb.isKinematic = false;
    }

    private void FaceShipForward(Vector3 lookDir)
    {
        lookRotation = Quaternion.LookRotation(lookDir);
        transform.localRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    private void DeccelerateShip()
    {
        rb.AddRelativeForce(Vector3.zero, ForceMode.Acceleration);
    }

    private void DescendShip()
    {
        rb.isKinematic = true;
        //make the descend speed change over time!
        transform.Translate(Vector3.down * descendSpeed * Time.deltaTime); ;
        if (al.altitude <= landHeight)
        {
            shipState = ShipState.Landed;
        }
    }

    private void AscendShip()
    {
        //make the ascend speed change over time!
        transform.Translate(Vector3.up * descendSpeed * Time.deltaTime);

        if (al.altitude >= takeoffHeight)
        {
            shipState = ShipState.Flying;
            EngageUserControls();
        }
    }

    bool CheckIfPlayerIsInCockpit()
    {
        if ((playerCockpit - fpc.transform.position).magnitude < 5)
        {
            return true;
        }
        return false;
    }

}
