using UnityEngine;
using UnityEngine.Serialization;

public class BreathingV4 : MonoBehaviour
{
    [FormerlySerializedAs("lightRate")]
    public float collectibleGain = 10f;
    public float lightLoseRate = 1.5f;
    [FormerlySerializedAs("maxLight")]
    public float maxLightRange = 10f;
    
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
        else if (currentLightRange <= 0)
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
            Destroy(other.gameObject);
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
