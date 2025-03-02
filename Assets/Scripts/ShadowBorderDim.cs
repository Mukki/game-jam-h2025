using UnityEngine;

public class ShadowBorderDim : MonoBehaviour
{
    private Material quadMaterial;
    private float targetBorderSize = 0.00f;
    private float targetNoiseStrength = 0.00f;
    public float transitionSpeed = 2.0f;
    
    private float transitionTime;
    private float currentBorderSize;
    private float currentNoiseStrength;

    void Start()
    {
        quadMaterial = GetComponent<Renderer>().material;

        if (quadMaterial.HasProperty("_BorderSize"))
        {
            currentBorderSize = quadMaterial.GetFloat("_BorderSize");
        }
        else
        {
            Debug.LogWarning("The '_BorderSize' property is not found in the shader.");
        }
        
        if (quadMaterial.HasProperty("_NoiseStrength"))
        {
            currentNoiseStrength = quadMaterial.GetFloat("_NoiseStrength");
        }
        else
        {
            Debug.LogWarning("The '_NoiseStrength' property is not found in the shader.");
        }
    }

    void Update()
    {
        if (quadMaterial != null)
        {
            currentBorderSize = Mathf.Lerp(currentBorderSize, targetBorderSize, transitionTime * Time.deltaTime);
            currentNoiseStrength = Mathf.Lerp(currentNoiseStrength, targetNoiseStrength, transitionTime * Time.deltaTime);

            quadMaterial.SetFloat("_BorderSize", currentBorderSize);
            quadMaterial.SetFloat("_NoiseStrength", currentBorderSize);

            if (Mathf.Abs(currentBorderSize - targetBorderSize) < 0.001f)
            {
                currentBorderSize = targetBorderSize;
            }
            
            if (Mathf.Abs(currentNoiseStrength - targetNoiseStrength) < 0.001f)
            {
                currentNoiseStrength = targetNoiseStrength;
            }

            transitionTime += transitionSpeed * Time.deltaTime;
        }
    }

    public void SetBorderValue(float newTargetBorderSize, float newTargetNoiseStrength)
    {
        targetBorderSize = newTargetBorderSize;
        targetNoiseStrength = newTargetNoiseStrength;
        transitionTime = 0f;
    }
}