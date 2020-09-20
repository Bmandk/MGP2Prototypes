using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BreathingV1 : MonoBehaviour
{
    [FormerlySerializedAs("lightRate")]
    public float lightGainRate = 10f;
    public float lightLoseRate = 1.5f;
    [FormerlySerializedAs("maxLight")]
    public float maxLightRange = 10f;
    public float minLightRange = 1f;
    
    private float currentLightRange;
    private Light playerLight;
    private bool canBreathe = true;
    private bool isInLight = true;

    private PlayerController pc;

    private void Awake()
    {
        playerLight = GetComponent<Light>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float l = 0;

        if (canBreathe && Input.GetKey(KeyCode.Space))
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
            currentLightRange = minLightRange;

            if (!isInLight)
            {
                pc.Die();
            }
        }

        playerLight.range = currentLightRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fume"))
        {
            canBreathe = false;
        }
        else if (other.CompareTag("LightZone"))
        {
            isInLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fume"))
        {
            canBreathe = true;
        }
        else if (other.CompareTag("LightZone"))
        {
            isInLight = false;
        }
    }
}
