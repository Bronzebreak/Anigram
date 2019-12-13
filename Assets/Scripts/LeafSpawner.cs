using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    #region Variables
    public GameObject bottomPoint;
    public GameObject topPoint;
    public daynight_cycle timeOverlord;
    public Vector3 topPos;
    public Vector3 bottomPos;
    public float time;
    public ParticleSystem particlePrefab;
    public ParticleSystem particleSys;
    float randomRange;
    int randomMin = -20;
    int randomMax = 20;
    Vector3[] randomVariance = new Vector3[10];
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[10];
    public Vector3[] particlePositions = new Vector3[10];

    public enum BreathingMode
    {
        inhaling,
        exhaling,
        delay
    }

    public BreathingMode currentMode = BreathingMode.inhaling;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Find the lighting cycle, which knows what time of day it is, and the values of inhale, exhale, and delay.
        timeOverlord = FindObjectOfType<daynight_cycle>();

        //Set values for the position of the top and bottom of the breathing cycle, and spawn a publicly chosen actor at the bottom position.
        bottomPos = bottomPoint.transform.position;
        topPos = topPoint.transform.position;
        particleSys = Instantiate(particlePrefab, bottomPos, bottomPoint.transform.rotation, transform);

        //Begin coroutine Loop.
        StartCoroutine(Loop());
    }

    //When run...
    IEnumerator Loop()
    {
        //...indefinitely loop...
        while (true)
        {
            //...update breathing mode dependent on breathing cycle, as determined by the timeOverlord.
            StorePositions();
            currentMode = BreathingMode.inhaling;
            VariateRandomly();
            yield return new WaitForSecondsRealtime(timeOverlord.inhaleTime);
            StorePositions();
            currentMode = BreathingMode.delay;
            VariateRandomly();
            yield return new WaitForSecondsRealtime(timeOverlord.breathDelay);
            StorePositions();
            currentMode = BreathingMode.exhaling;
            VariateRandomly();
            yield return new WaitForSecondsRealtime(timeOverlord.exhaleTime);
            StorePositions();
            currentMode = BreathingMode.delay;
            VariateRandomly();
            yield return new WaitForSecondsRealtime(timeOverlord.breathDelay);
        }
    }

    void VariateRandomly()
    {
        for(int index = 0; index < particleSys.particleCount; index ++)
        {
            randomVariance[index] = new Vector3(Random.Range(randomMin,randomMax),0,Random.Range(randomMin,randomMax));
        }
    }

    void StorePositions()
    {
        for (int index = 0; index < particleSys.particleCount; index++)
        {
            particlePositions[index] = particles[index].position;
        }
    }

    void Move(Vector3 start, Vector3 end, float targetTime, float size)
    {
        particleSys.GetParticles(particles);
        for (int index = 0; index < particleSys.particleCount; index++)
        {
            particles[index].position = Vector3.Lerp(particlePositions[index], end + (randomVariance[index]/size), Mathf.SmoothStep(0, 1, (time / targetTime)));
        }
        particleSys.SetParticles(particles);
    }

    //Once a frame...
    void Update()
    {
        //...update time,
        time += Time.deltaTime;

        //then, dependent on which mode we're in...
        switch (currentMode) 
        {
            //...move from the bottom to top.
            case BreathingMode.inhaling:
                Move(bottomPos, topPos, timeOverlord.inhaleTime, 4);
                break;
            //...move from the top to bottom.
            case BreathingMode.exhaling:
                Move(topPos, bottomPos, timeOverlord.exhaleTime, 1);
                break;
            //...reset time.
            case BreathingMode.delay:
                time = 0;
                break;
            default:
                break;
        }
    }
}
