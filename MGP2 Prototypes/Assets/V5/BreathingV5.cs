using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BreathingV5 : MonoBehaviour
{
    [FormerlySerializedAs("lightRate")]
    public float lightGainRate = 10f;
    public float lightLoseRate = 1.5f;
    [FormerlySerializedAs("maxLight")]
    public float maxLightRange = 10f;
    public float minLightRange = 1f;
    
    public float maxZoneRange = 7.139219f;
    public float zoneGainRate = 5f;
    
    private float currentLightRange;
    private Light playerLight;
    private bool isInLight = true;

    private PlayerController pc;

    private void Awake()
    {
        playerLight = GetComponent<Light>();
        pc = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentLightRange = maxLightRange;
        playerLight.range = maxLightRange;
    }

    // Update is called once per frame
    void Update()
    {
        float l = 0;

        if (isInLight)
            l = lightGainRate;
        else
            l = -lightLoseRate;

        currentLightRange += l * Time.deltaTime;

        if (currentLightRange >= maxLightRange)
        {
            currentLightRange = maxLightRange;
        }
        else if (currentLightRange <= minLightRange)
        {
            currentLightRange = maxLightRange;
            pc.Die();
        }

        playerLight.range = currentLightRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightZone") || other.CompareTag("Crystal"))
        {
            isInLight = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            Light l = other.GetComponentInChildren<Light>();

            if (l.range < maxZoneRange)
            {
                l.range += zoneGainRate * Time.fixedDeltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZone") || other.CompareTag("Crystal"))
        {
            isInLight = false;
        }
    }
}
