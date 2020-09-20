using UnityEngine;
using UnityEngine.Serialization;

public class BreathingV5 : MonoBehaviour
{
    [FormerlySerializedAs("lightRate")]
    public float collectibleGain = 10f;
    public float lightLoseRate = 1.5f;
    [FormerlySerializedAs("maxLight")]
    public float maxLightRange = 10f;
    public float minLightRange = 1f;
    
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
            l = 0;
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
        if (other.CompareTag("LightZone"))
        {
            isInLight = true;
        }
        else if (other.CompareTag("Collectible"))
        {
            currentLightRange += collectibleGain;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZone"))
        {
            isInLight = false;
        }
    }
}
