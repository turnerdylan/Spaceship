using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHS;

public class PlayerShipInteractionController : MonoBehaviour
{
    FirstPersonController fpc;


    // Start is called before the first frame update
    void Start()
    {
        fpc = FindObjectOfType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
