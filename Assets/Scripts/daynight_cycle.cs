using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynight_cycle : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;
    float sunInitialIntensity;
    //public Gradient sunColor;

    //public float updateRateInSeconds = 5f;
    /*
    public float maxIntensity = 2f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;
    public Light moon;
    public float maxBounceIntensity = 1.0f;
    public float minBounceIntensity = 0.5f;
    public float maxAmbient = 1f;
    public float minAmbient = 0f;
    public float minAmbientPoint = -0.2f;
    public Gradient nightDayFogColor;
    public float fogScale = 1f;
    public float exposureMultiplier = 1f;
    public float dayAtmosphereThickness = 0.4f;
    public float nightAtmosphereThickness = 0.87f;
    public AnimationCurve fogDensityCurve;
    public Gradient nightDayColor;
    Material skyMat;
    */

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        //InvokeRepeating("UpdateCycle", updateRateInSeconds, updateRateInSeconds);
    }

    void Update()
    {
        UpdateSun();
        //currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        currentTimeOfDay =( 0.45f + (Time.realtimeSinceStartup/secondsInFullDay) ) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay -= 1;
        }

    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }

        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }

        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = 1;//Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

   // void AdjustSunColor()
   // {
     //   sun.color = sunColor.Evaluate(sun.intensity);
    //}

       /*
    void UpdateFX()
    {
        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(sun.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((maxIntensity - minIntensity) * dot) + minIntensity;
        sun.intensity = i;
        if (moon != null)
            moon.intensity = 1 - i;

        i = ((maxBounceIntensity - minBounceIntensity) * dot) + minBounceIntensity;
        sun.bounceIntensity = i;

        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(sun.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((maxAmbient - minAmbient) * dot) + minAmbient;
        RenderSettings.ambientIntensity = i;

        sun.color = nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = sun.color;

        RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
        RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        skyMat.SetFloat("_AtmosphereThickness", i);
        skyMat.SetFloat("_Exposure", i * exposureMultiplier);
    }

    */
}
