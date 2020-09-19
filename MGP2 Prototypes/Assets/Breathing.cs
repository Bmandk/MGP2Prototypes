using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Breathing : MonoBehaviour
{
    [FormerlySerializedAs("lightRate")]
    public float lightGainRate;
    public float lightLoseRate;
    [FormerlySerializedAs("maxLight")]
    public float maxLightRange;
    
    private float currentLightRange;
    private Light playerLight;
    private bool canBreathe = true;

    private void Awake()
    {
        playerLight = GetComponent<Light>();
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
        else if (currentLightRange <= 0)
        {
            currentLightRange = 0;
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
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fume"))
        {
            canBreathe = true;
        }
    }
}
