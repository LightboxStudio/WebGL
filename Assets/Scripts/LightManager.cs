using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
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
    }

    public void ChangeGlobalLightIntensity(float scale)
    {
        foreach (var lightPoint in this.lightPoints)
        {
            lightPoint.light.intensity = lightPoint.initialIntensity * scale;
        }
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
