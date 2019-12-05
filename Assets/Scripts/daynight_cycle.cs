using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynight_cycle : MonoBehaviour
{
    //Wind Particle Variables
    public FollowPath followPathRef; //reference to script in first particle
    //public FollowPath followPathRef2; //reference to script in second particle
    //public FollowPath followPathRef3; //reference to script in second particle
    public GameObject inhaleSpawnPoint;
    public GameObject exhaleSpawnPoint;//Spawn Point of the wind
    float time1 = 2.3f; // initial time of the inhale cycle
    float time2 = 4.3f; // initial time of exhale cycle
    float timeDelay = 0.3f; // delay between inhale and exhale
    float timeNow; // time since the start of the program
    public GameObject particlePrefab1; // reference to prefabe
    public GameObject particleInstance1; // instantiated object
   /* public GameObject particlePrefab2;
    public GameObject particleInstance2;
    
    public GameObject particlePrefab3;
    public GameObject particleInstance3;*/

    // day night cycle variables
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;
    float sunInitialIntensity;
    public Light moon;

    /*
    public Color startColor;
    public Color lightorangeColor;
    public Color orangeColor;
    public Color redColor;
    public Color pinkColor;
    public Color endColor;
    */
    public Gradient sunset;

    //public float updateRateInSeconds = 5f;
    /*
    public float maxIntensity = 2f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;
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
    public void FullCircle()
    {
        StartCoroutine(Timer()); // starts inhale exghale cycle
    }
    void Start()
    {
        sunInitialIntensity = sun.intensity;
        //InvokeRepeating("UpdateCycle", updateRateInSeconds, updateRateInSeconds);
        particleInstance1 = Instantiate(particlePrefab1, inhaleSpawnPoint.transform.position, Quaternion.identity);
        followPathRef = particleInstance1.GetComponentInChildren<FollowPath>();
       /* particleInstance2 = Instantiate(particlePrefab2, inhaleSpawnPoint.transform.position, Quaternion.identity);
        followPathRef2 = particleInstance2.GetComponentInChildren<FollowPath>();
        particleInstance3 = Instantiate(particlePrefab3, inhaleSpawnPoint.transform.position, Quaternion.identity);
        followPathRef3 = particleInstance3.GetComponentInChildren<FollowPath>();*/
        FullCircle(); // calls the function at the begining of the run
    }

    void Update()
    {

        BoxCollider[] bc = GetComponentsInChildren<BoxCollider>();
        timeNow = Time.realtimeSinceStartup; // sets timne to the runtime of the programm


        UpdateSun();
        //currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        currentTimeOfDay =( 0.45f + (Time.realtimeSinceStartup/secondsInFullDay) ) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay -= 1;
        }

        sun.color = sunset.Evaluate(currentTimeOfDay);
/*
        if (currentTimeOfDay >= 0.65)
            {
                sun.color = Color.Lerp(startColor, endColor, currentTimeOfDay);
            }

        else if (currentTimeOfDay >= 0.62)
        {
            sun.color = Color.Lerp(redColor, pinkColor, currentTimeOfDay);
        }
        else if (currentTimeOfDay >= 0.6)
        {
            sun.color = Color.Lerp(orangeColor, redColor, currentTimeOfDay);
        }
        else if (currentTimeOfDay >= 0.57)
        {
            sun.color = Color.Lerp(lightorangeColor, orangeColor, currentTimeOfDay);
        }

        else if (currentTimeOfDay >= 0.55)
        {
            sun.color = Color.Lerp(startColor, lightorangeColor, currentTimeOfDay);
        }
        */

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

    IEnumerator Timer()
    {
        while (true)
        {
            time1 = 2 + (1 * (timeNow / (secondsInFullDay/4)));// updates the time for the graduale increase of the inhale cycle
            time2 = 4 + (3 * (timeNow / (secondsInFullDay / 4)));// updates the time for the graduale increase of the exhale cycle
            timeDelay = 0.3f + (0.3f * (timeNow / (secondsInFullDay / 4)));// updates delay between cycles


            followPathRef.TimeTotal1 = time1;
            followPathRef.TimeTotal2 = time2;

            //followPathRef2.TimeTotal1 = time1;
            //followPathRef2.TimeTotal2 = time2;

            //followPathRef3.TimeTotal1 = time1;
            //followPathRef3.TimeTotal2 = time2;

            yield return new WaitForSeconds(time1 +time2+ timeDelay);
        }
    }
}
