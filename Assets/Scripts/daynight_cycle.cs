using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class daynight_cycle : MonoBehaviour
{
    public AudioSource InhaleSource;
    public AudioSource ExhaleSource;
    public float inhaleTime = 2f; // initial time of the inhale cycle
    public float exhaleTime = 4f; // initial time of exhale cycle
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
    public Gradient skybox;
    public Gradient fog;
    bool ending;

    public void FullCircle()
    {
        StartCoroutine(Breathing()); // starts inhale exghale cycle
    }

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        FullCircle(); // calls the function at the begining of the run
    }

    void Update()
    {
        timeSinceLaunch = Time.realtimeSinceStartup; // sets timne to the runtime of the programm
        UpdateSun();
        currentTimeOfDay = (0.45f + (Time.realtimeSinceStartup / secondsInFullDay)) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay -= 1;
        }

        sun.color = sunset.Evaluate(currentTimeOfDay);

        if (RenderSettings.skybox.HasProperty("_Tint"))
        {
            RenderSettings.skybox.SetColor("_Tint", skybox.Evaluate(currentTimeOfDay));
            RenderSettings.skybox.SetFloat("_Exposure", .4f);
            RenderSettings.skybox.SetFloat("_Rotation", -300*currentTimeOfDay);
            RenderSettings.fogColor = fog.Evaluate(currentTimeOfDay);
        }

        if (currentTimeOfDay >= .73f && !ending)
        {
            ending = true;
            EndItAll();
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

    IEnumerator Breathing()
    {
        while (true)
        {
            StartCoroutine(VolumeFader(InhaleSource, inhaleTime + 0.1f, 0.0f));// starts the couroutine
            yield return new WaitForSeconds(inhaleTime + breathDelay); // Wait For the end of the coroutinea by delay
            StartCoroutine(VolumeFader(ExhaleSource, exhaleTime + 0.1f, 0.0f));// starts the coroutine withe different audiosource and time.
            yield return new WaitForSeconds(exhaleTime + breathDelay);// delay 
            //After breathing cycle...
            inhaleTime = 2 + (timeSinceLaunch / (300)); //Gradually updates the time towards 3, based off of how close time/timeTotal is to 1
            exhaleTime = 4 + (3 * (timeSinceLaunch / (300))); //Gradually updates the time towards 7, based off of how close time/timeTotal is to 1
            breathDelay = 0.3f + (0.3f * (timeSinceLaunch / 300)); //Gradually updates the time towards .6, based off of how close time/timeTotal is to 1
        }
    }

    public IEnumerator VolumeFader(AudioSource audioSource, float duration, float targetVolume)
    {
        audioSource.volume = 1f;// sets audio source volume value
        audioSource.Play();// starts the audio source
        float currentTime = 0;
        float start = audioSource.volume; // sets the initial value for lerp
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(start, targetVolume, currentTime / duration);// lerps the volume between intial value and zero
            yield return null;
        }
        yield break;
    }

    IEnumerator EndItAll()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(Color.black, 2.0f);
        yield return new WaitForSeconds(2.0f);
        ExperienceApp.End();
        yield return null;
    }
}
  