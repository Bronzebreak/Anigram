using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class daynight_cycle : MonoBehaviour
{
    public AudioClip inhaleSound;
    public AudioClip exhaleSound;
    bool keepFadingIn;
    bool keepFadingOut;
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

    public void FullCircle()
    {
        StartCoroutine(Breathing()); // starts inhale exghale cycle
    }

    void Start()
    {
        InhaleSource.clip = inhaleSound;
        sunInitialIntensity = sun.intensity;
        FullCircle(); // calls the function at the begining of the run
    }

    void Update()
    {
        //print(InhaleSource.volume);
        if (!InhaleSource.isPlaying)
        {

            print("End");
            //InhaleSource.volume = 0.9f;
        }
        if (!ExhaleSource.isPlaying)
        {

            print("End");
            //ExhaleSource.volume = 0.9f;
        }
        timeSinceLaunch = Time.realtimeSinceStartup; // sets timne to the runtime of the programm
        //print(breathingSource.volume);
        UpdateSun();
        //currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        currentTimeOfDay = (0.45f + (Time.realtimeSinceStartup / secondsInFullDay)) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay -= 1;
        }
        //breathingSource.pitch = (1.1f - (timeSinceLaunch / (secondsInFullDay ))) ;
        sun.color = sunset.Evaluate(currentTimeOfDay);
        if (RenderSettings.skybox.HasProperty("_Tint"))
        {
            RenderSettings.skybox.SetColor("_Tint", skybox.Evaluate(currentTimeOfDay));
            RenderSettings.skybox.SetFloat("_Exposure", .4f);
            RenderSettings.skybox.SetFloat("_Rotation", -300*currentTimeOfDay);
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
            StartCoroutine(StartFadeInhale(InhaleSource, inhaleTime + 0.1f, 0.0f));

            yield return new WaitForSeconds(inhaleTime + breathDelay);
            StartCoroutine(StartFadeInhale(ExhaleSource, exhaleTime + 0.1f, 0.0f));
            yield return new WaitForSeconds(exhaleTime + breathDelay);
            //After breathing cycle...
            inhaleTime = 2 + (timeSinceLaunch / (300)); //Gradually updates the time towards 3, based off of how close time/timeTotal is to 1
            exhaleTime = 4 + (3 * (timeSinceLaunch / (300))); //Gradually updates the time towards 7, based off of how close time/timeTotal is to 1
            breathDelay = 0.3f + (0.3f * (timeSinceLaunch / 300)); //Gradually updates the time towards .6, based off of how close time/timeTotal is to 1
        }
    }
    /*

        IEnumerator FadeIN(AudioSource breath, float speed, float maxVolume)
        {
            keepFadingIn = true;
            keepFadingOut = false;
            breath.volume = 0;
            float AudioVolume = breath.volume;
            while (breath.volume< maxVolume && keepFadingIn)
            {
                AudioVolume += speed;
                breath.volume = AudioVolume;
                yield return new WaitForSeconds(0.1f);
            }
        }
        IEnumerator FadeOUT(AudioSource breath, float speed)
        {
            keepFadingIn = false;
            keepFadingOut = true;

            float AudioVolume = breath.volume;
            while (breath.volume >= speed && keepFadingOut)
            {
                AudioVolume -= speed;
                breath.volume = AudioVolume;
                yield return new WaitForSeconds(0.1f);
            }
        }
        */
    public IEnumerator StartFadeInhale(AudioSource audioSource, float duration, float targetVolume)
    {
        audioSource.volume = 0.8f;
        //yield return new WaitForSeconds(0.1f);
        audioSource.Play();

        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;

        }
        yield break;
    }
}
  