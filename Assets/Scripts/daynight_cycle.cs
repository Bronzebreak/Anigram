using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class daynight_cycle : MonoBehaviour
{
    #region Time
    float timeSinceLaunch;
    public float secondsInFullDay = 1200f;
    [HideInInspector]
    public float timeMultiplier = 1f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    #endregion

    #region Breathing
    public AudioSource InhaleSource;
    public AudioSource ExhaleSource;
    public float inhaleTime = 2f;
    public float exhaleTime = 4f;
    public float breathDelay = 0.3f;
    #endregion

    #region Lighting
    public Light sun;
    float sunInitialIntensity;
    public Gradient sunset;
    public Gradient skybox;
    public Gradient fog;
    #endregion

    bool ending;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        StartCoroutine(Breathing());
    }

    void Update()
    {
        //Keep track of time since the start of program.
        timeSinceLaunch = Time.time;

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

        currentTimeOfDay = (0.45f + (Time.time / secondsInFullDay)) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay -= 1;
        }

        sun.color = sunset.Evaluate(currentTimeOfDay);
        RenderSettings.skybox.SetColor("_Tint", skybox.Evaluate(currentTimeOfDay));
        RenderSettings.skybox.SetFloat("_Exposure", .4f);
        RenderSettings.skybox.SetFloat("_Rotation", -300*currentTimeOfDay);
        RenderSettings.fogColor = fog.Evaluate(currentTimeOfDay);
        RenderSettings.ambientLight = skybox.Evaluate(currentTimeOfDay);

        if (currentTimeOfDay >= .73f && !ending)
        {
            ending = true;
            EndExperience();
        }
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

    IEnumerator EndExperience()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(Color.black, 2.0f);
        yield return new WaitForSeconds(2.0f);
        ExperienceApp.End();
        yield return null;
    }
}
  