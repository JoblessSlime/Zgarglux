using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float cycleDuration = 10f; // Total time for one day or night
    public Light directionalLight; // Assign your scene's main directional light
    public Material daySkybox, nightSkybox; // Assign in Inspector

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
            directionalLight.intensity = 1f; // Bright light for day
            dayCount++;
        }
        else
        {
            RenderSettings.skybox = nightSkybox;
            directionalLight.intensity = 0.2f; // Dim light for night
        }

        DynamicGI.UpdateEnvironment(); // Update lighting
    }
}