using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class daynight_cycle : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioClip breathingSound; 
    public AudioSource breathingSource;
    //Wind Particle Variables
    public float inhaleTime = 2.3f; // initial time of the inhale cycle
    public float exhaleTime = 4.3f; // initial time of exhale cycle
    public float breathDelay = 0.3f; // delay between inhale and exhale
    float timeSinceLaunch; // time since the start of the program

    // day night cycle variables
    public Light sun;
    public float secondsInFullDay = 1200f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;
    float sunInitialIntensity;
    public Light moon;

    public Gradient sunset;

    public void FullCircle()
    {
        StartCoroutine(Timer()); // starts inhale exghale cycle
    }
    void Start()
    {
        breathingSource.clip = breathingSound;
        breathingSource.Play(0); 
        mixer.AudioMixer.SetFloat(pitchBlend, 1.0f,1.5f);
        sunInitialIntensity = sun.intensity;
        FullCircle(); // calls the function at the begining of the run
       
    }

    void Update()
    {

        timeSinceLaunch = Time.realtimeSinceStartup; // sets timne to the runtime of the programm

        UpdateSun();
        //currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        currentTimeOfDay =( 0.45f + (Time.realtimeSinceStartup/secondsInFullDay) ) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay -= 1;
        }
        breathingSource.pitch = (1.1f - (timeSinceLaunch / (secondsInFullDay ))) ;
        sun.color = sunset.Evaluate(currentTimeOfDay);
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

    IEnumerator Timer()
    {
        while (true)
        {
            //After breathing cycle...
            yield return new WaitForSeconds(inhaleTime + breathDelay + exhaleTime + breathDelay);
            inhaleTime = 2 + (timeSinceLaunch / (secondsInFullDay/4)); //Gradually updates the time towards 3, based off of how close time/timeTotal is to 1
            exhaleTime = 4 + (3 * (timeSinceLaunch / (secondsInFullDay / 4))); //Gradually updates the time towards 7, based off of how close time/timeTotal is to 1
            breathDelay = 0.3f + (0.3f * (timeSinceLaunch / (secondsInFullDay / 4))); //Gradually updates the time towards .6, based off of how close time/timeTotal is to 1
        }
    }
}
