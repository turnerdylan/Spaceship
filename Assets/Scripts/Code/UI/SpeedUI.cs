﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows throttle and speed of the player ship.
/// </summary>
public class SpeedUI : MonoBehaviour
{
    private Text speedText;

    private void Awake()
    {
        speedText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speedText != null && Ship.PlayerShip != null)
        {
            speedText.text = string.Format("THR: {0}\nSPD: {1}", (Ship.PlayerShip.Throttle * 100.0f).ToString("000"), Ship.PlayerShip.Velocity.magnitude.ToString("000"));
        }
    }
}