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
    public GameObject[] animals = new GameObject[7];
    public int spawnMax = 3;
    public int spawnCount = 0;
    public GameObject phone;
    public GameObject centerEye;
    private Camera head;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> results = new List<GameObject>();
    public int startDelay = 30;
    public int repeatDelay = 5; 
    public float breathDelay = .3f;
    public float inhaleTime = 2f;
    public float exhaleTime = 4f;
    public AudioClip[] sounds = new AudioClip[7];
    public ParticleSystem spawnParticle;

    #endregion

    //On itialization...
    void Start()
    {
        //...encapsulate the GazeInput to allow for modification to the hoverDelay and hoverDuration components.
        gazeRef = avatarRef.GetComponent<GazeInput>();

        //...encapsulate the Camera used (in PIE sessions, at least) to allow for the LookAt() function to be ran.
        head = centerEye.GetComponent<Camera>();

        //...run Spawn after startDelay seconds, repeating every repeatDelay seconds.
        InvokeRepeating("Spawn", startDelay, repeatDelay);
    }

    //Once a frame...
    void Update()
    {
        //...call function to update the values of the GazeInput.
        UpdateGazePointerProperties(breathDelay, inhaleTime);

        //...reorient the phone to face the encapsulated camera,
        phone.transform.LookAt(head.transform);
        //then orient the camera in world space to always appear upright.
        phone.transform.eulerAngles = new Vector3(phone.transform.rotation.eulerAngles.x + 90, phone.transform.rotation.eulerAngles.y, phone.transform.rotation.eulerAngles.z);

        #region Pseudo-Code
        /*
        Get animController
        animControllerRef.inhaleDelay = inhaleDelay; repeat for time/exhales.
        
        Get sun
        sun delta start-end time == breathing delta inhale start-end time. 
        endbreathDelay = .6f, inhaleTime = 3f, exhaletime = 7f
        */
        #endregion
    }

    //When called...
    public void UpdateGazePointerProperties(float delay, float duration)
    {
        //...set encapsulated GazeInput component based off of passed in variables.
        gazeRef.HoverDelay = delay;
        gazeRef.HoverDuration = duration;

        //Code provided by Tin of liminalVR in order to allow for GazeInput properties to be accessed.
        var type = gazeRef.GetType();
        var applyPointerPropertiesMethod = type.GetMethod("ApplyTimedPointerProperties", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (applyPointerPropertiesMethod != null)
            applyPointerPropertiesMethod.Invoke(gazeRef, null);
    }


    //When called...
    void Spawn()
    {
        //...if the maximum amount of spawns is not reached...
        if (spawnCount < spawnMax)
        {
            //...return a random object from the animals list,
            int index = Random.Range(0, animals.Length);
            GameObject toSpawn = animals[index];
            //and create local variables for the spawn actor and string used in the FindAll call.
            GameObject spawnLocation;
            string zone;
            //Then, determine which biome is used based off of which animal is selected,
            #region Determine Biome
            if (index < 2)
            {
                zone = "Hill";
            }

            else if (index < 4)
            {
                zone = "Table";
            }

            else if (index < 5)
            {
                zone = "Garden";
            }

            else
            {
                zone = "Pond";
            }
            #endregion
            //find all potential spawn points in the biome,
            results = spawnPoints.FindAll(
            delegate(GameObject point)
            {
            return point.name.Contains(zone);
            }
            );
            //instantiate the animal randomly at one of the points,
            int spawnIndex = Random.Range(0, results.Count);
            spawnLocation = results[spawnIndex];
            GameObject spawned = Instantiate(toSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
            //if the animal is flying, modify its spawn to above ground,
            if (index == 0 || index == 2)
            {
                spawned.transform.position = spawned.transform.position + new Vector3 (0, 2, 0);
            }
            //instantiate the publicly set paricle at the location of the spawned creature,
            ParticleSystem spawnedParticle = Instantiate(spawnParticle, spawnLocation.transform.position, spawnLocation.transform.rotation);
            //then publicly set the spawnPoint and sound in the instantiated animal's code,
            spawned.GetComponent<Animal>().spawnPoint = results[spawnIndex];
            spawned.GetComponent<Animal>().soundToPlay = sounds[index];
            //before finally removing the used location as a potential spawn point and increasing the total count of spawned creatures.
            spawnPoints.Remove(results[spawnIndex]);
            spawnCount++;    
        }
    }
}
