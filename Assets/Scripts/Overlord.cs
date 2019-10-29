using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Core;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Avatars.Extensions;

public class Overlord : MonoBehaviour
{
    #region Variables
    //References
    public VRAvatar avatarRef;
    public GazeInput gazeRef;

    //Breathing
    public float breathDelay;
    public float inhaleTime;
    public float exhaleTime;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        breathDelay = .3f;
        inhaleTime = 2f;
        exhaleTime = 4f;
        gazeRef = avatarRef.GetComponent<GazeInput>();
        gazeRef.HoverDelay = breathDelay;
        gazeRef.HoverDuration = inhaleTime;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Get animController
        animControllerRef.inhaleDelay = inhaleDelay; repeat for time/exhales.
        
        Get sun
        sun delta start-end time == breathing delta inhale start-end time. 
        endbreathDelay = .6f, inhaleTime = 3f, exhaletime = 7f
        */
    }
}
