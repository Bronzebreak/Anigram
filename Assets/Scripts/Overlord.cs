using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Core;
using Liminal.SDK.VR.Avatars.Extensions;

public class Overlord : MonoBehaviour
{
    #region Variables

    //References
    public GameObject avatarRef;

    //Breathing
    public float breathDelay;
    public float inhaleTime;
    public float exhaleTime;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        avatarRef = GameObject.FindWithTag("Player");
        breathDelay = .3f;
        inhaleTime = 2f;
        exhaleTime = 4f;
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
