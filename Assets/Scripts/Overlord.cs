using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Core;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Avatars.Extensions;
using Liminal.SDK.Serialization;
using System.Reflection;

public class Overlord : MonoBehaviour
{
    #region Variables
    //References
    public VRAvatar avatarRef;
    public GazeInput gazeRef;

    //Breathing
    //[HideInInspector]
    public float breathDelay;
    public float inhaleTime;
    public float exhaleTime;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        breathDelay = .5f;
        inhaleTime = 2f;
        exhaleTime = 4f;
        gazeRef = avatarRef.GetComponent<GazeInput>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGaze();
        /*
        Get animController
        animControllerRef.inhaleDelay = inhaleDelay; repeat for time/exhales.
        
        Get sun
        sun delta start-end time == breathing delta inhale start-end time. 
        endbreathDelay = .6f, inhaleTime = 3f, exhaletime = 7f
        */
    }
    
    public void UpdateGazePointerProperties(float delay, float duration)
    {
        gazeRef.HoverDelay = delay;
        gazeRef.HoverDuration = duration;

        var type = gazeRef.GetType();
        var applyPointerPropertiesMethod = type.GetMethod("ApplyTimedPointerProperties", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (applyPointerPropertiesMethod != null)
            applyPointerPropertiesMethod.Invoke(gazeRef, null);
    }

    public void UpdateGaze()
    {
        UpdateGazePointerProperties(breathDelay, inhaleTime);
    }
}
