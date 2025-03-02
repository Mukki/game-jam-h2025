using UnityEngine;

public class Lamp : MonoBehaviour
{
    private bool isNight;
    private float currentTime;
    private Light spotLight;

    private float openLightDuration = 5.0f;
    private float closeLightDuration = 2.0f;
    private float lightMaxIntensity = 5.0f;

    private void OnEnable()
    {
        GameEvent.Register(Event.NightStart, OnNightStart);
        GameEvent.Register(Event.NightEnd, OnNightEnd);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.NightStart, OnNightStart);
        GameEvent.Unregister(Event.NightEnd, OnNightEnd);
    }

    private void Awake()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    private void OnNightStart()
    {
        isNight = true;
        currentTime = 0.0f;
    }

    private void OnNightEnd()
    {
        isNight = false;
        currentTime = closeLightDuration;
    }

    private void Update()
    {
        if (isNight && currentTime < openLightDuration)
        {
            currentTime += Time.deltaTime;
            spotLight.intensity = Mathf.Lerp(0.0f, lightMaxIntensity, currentTime / openLightDuration);
        }
        else if (!isNight && currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            spotLight.intensity = Mathf.Lerp(0.0f, lightMaxIntensity, currentTime / closeLightDuration);
        }
    }
}
