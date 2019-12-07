﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMover : MonoBehaviour
{
    #region Variables
    public GameObject prefab;
    public GameObject particleActor;
    public GameObject bottomPoint;
    public GameObject topPoint;
    public daynight_cycle timeOverlord;
    public Vector3 topPos;
    public Vector3 bottomPos;
    public float time;

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
        particleActor = Instantiate(prefab, bottomPos, bottomPoint.transform.rotation);
        particleActor.transform.localScale = new Vector3(10,10,10);

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
            currentMode = BreathingMode.inhaling;
            yield return new WaitForSecondsRealtime(timeOverlord.inhaleTime);
            currentMode = BreathingMode.delay;
            yield return new WaitForSecondsRealtime(timeOverlord.breathDelay);
            currentMode = BreathingMode.exhaling;
            yield return new WaitForSecondsRealtime(timeOverlord.exhaleTime);
            currentMode = BreathingMode.delay;
            yield return new WaitForSecondsRealtime(timeOverlord.breathDelay);
        }
    }

    //Using a start location, end location, and time...
    void Move(Vector3 start, Vector3 end, float targetTime)
    {
        //...tween the particle from the start to end, based off of what the current time is compared to the full time for the process to end.
        particleActor.transform.position = Vector3.Lerp(start, end, (time / targetTime));
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
                Move(bottomPos, topPos, timeOverlord.inhaleTime);
                break;
            //...move from the top to bottom.
            case BreathingMode.exhaling:
                Move(topPos, bottomPos, timeOverlord.exhaleTime);
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
