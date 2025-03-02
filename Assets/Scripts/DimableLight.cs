using UnityEngine;

public class DimableLight : MonoBehaviour
{
    private Light directionalLight;
    public float targetIntensity = 1.0f;
    public float transitionDuration = 2.0f;

    private float currentIntensity;
    private float timeElapsed;

    void Start()
    {
        directionalLight = RenderSettings.sun;

        if (directionalLight != null)
        {
            currentIntensity = directionalLight.intensity;
        }
        else
        {
            Debug.LogWarning("No main directional light found.");
        }
    }

    void Update()
    {
        if (directionalLight != null)
        {
            timeElapsed += Time.deltaTime;

            directionalLight.intensity = Mathf.Lerp(currentIntensity, targetIntensity, timeElapsed / transitionDuration);

            if (timeElapsed >= transitionDuration)
            {
                directionalLight.intensity = targetIntensity;
            }
        }
    }

    public void SetTargetIntensity(float newTargetIntensity)
    {
        targetIntensity = newTargetIntensity;
        currentIntensity = directionalLight.intensity;
        timeElapsed = 0f;
    }
}