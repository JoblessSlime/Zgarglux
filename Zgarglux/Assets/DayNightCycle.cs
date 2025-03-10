using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float cycleDuration = 10f;
    public float transitionDuration = 2f;
    public Light directionalLight;
    public Material daySkybox, nightSkybox;
    public float dayLightIntensity = 1f;
    public float nightLightIntensity = 0.2f;


 
    private bool isDay = true;
    private int dayCount = 1;

    private void Start()
    {
        StartCycle();
    }

    private void StartCycle()
    {
        InvokeRepeating(nameof(SwitchTime), cycleDuration, cycleDuration);
    }

    private void SwitchTime()
    {
        isDay = !isDay;

        if (isDay)
        {
            RenderSettings.skybox = daySkybox;
            directionalLight.intensity = 1f; 
            dayCount++;
        }
        else
        {
            RenderSettings.skybox = nightSkybox;
            directionalLight.intensity = 0.2f; 
        }

        DynamicGI.UpdateEnvironment(); 
    }
}
