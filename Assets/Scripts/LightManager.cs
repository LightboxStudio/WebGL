using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [Header("References")]
    public Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    [Header("Settings")]
    public float minExposure = -4.0f;

    [Header("Data")]
    public List<LightPoint> lightPoints = new();

    void Start()
    {
        LoadLights();
    }

    private void LoadLights()
    {
        lightPoints.Clear();
        foreach (Transform child in this.transform)
        {
            var light = child.GetComponent<Light>();
            if (light != null)
            {
                var lightPoint = new LightPoint(light);
                lightPoints.Add(lightPoint);
            }
        }
        postProcessingVolume.profile.TryGet(out colorAdjustments);
    }

    public void ChangeGlobalLightIntensity(float scale)
    {
        foreach (var lightPoint in this.lightPoints)
        {
            lightPoint.light.intensity = lightPoint.initialIntensity * scale;
        }
        colorAdjustments.postExposure.value = minExposure * (1 - scale);
    }

    [System.Serializable]
    public class LightPoint
    {
        public LightPoint(Light light)
        {
            this.light = light;
            this.initialIntensity = light.intensity;
        }
        public Light light;
        public float initialIntensity = -1.0f;
    }
}
