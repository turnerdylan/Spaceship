using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverbikeController : MonoBehaviour
{
    private Rigidbody hoverbike;
    private bool isBikeActive = false;

    [SerializeField] private float verticalSpeedMultiplier = 1f;
    [SerializeField] private float hoverHeight = 10f;
    [SerializeField] private float hoverTolerance = 0.5f;
    [SerializeField] private float maximumVerticalVelocity = 10f;

    private void Awake()
    {
        hoverbike = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { ToggleBike(); }

        if (isBikeActive)
        {
            Physics.Raycast(hoverbike.transform.position, Vector3.down, out RaycastHit hit);

            Vector3 modifiedVelocity = hoverbike.velocity;

            if ((hit.distance > hoverHeight - hoverTolerance) && (hit.distance < hoverHeight + hoverTolerance))
            {
                modifiedVelocity.y = 0f;
            }
            else
            {
                modifiedVelocity.y = -(hit.distance - hoverHeight) * verticalSpeedMultiplier;
                modifiedVelocity.y = Mathf.Clamp(modifiedVelocity.y, -maximumVerticalVelocity, maximumVerticalVelocity);
            }

            Debug.Log($"Distance from ground: {hit.distance}, Bike Velocity.y: {modifiedVelocity}");
            hoverbike.velocity = modifiedVelocity;
            hoverbike.AddForce(Vector3.forward);
        }
    }

    private void ToggleBike()
    {
        isBikeActive = !isBikeActive;
        hoverbike.useGravity = !isBikeActive;
    }
}
