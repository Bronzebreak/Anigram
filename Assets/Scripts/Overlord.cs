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
    public GameObject[] hillSpawns = new GameObject[6];
    public GameObject[] tableSpawns = new GameObject[5];
    public GameObject[] gardenSpawns = new GameObject[5];
    public GameObject[] pondSpawns = new GameObject[6];
    public int spawnMax = 3;
    public int spawnCount = 0;
    public GameObject phone;
    public GameObject centerEye;
    public Camera head;

    //Breathing
    //[HideInInspector]
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
        InvokeRepeating("SpawnCheck", 10f, 5f);
        head = centerEye.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGazePointerProperties(breathDelay, inhaleTime);
        /*
        Get animController
        animControllerRef.inhaleDelay = inhaleDelay; repeat for time/exhales.
        
        Get sun
        sun delta start-end time == breathing delta inhale start-end time. 
        endbreathDelay = .6f, inhaleTime = 3f, exhaletime = 7f
        */
        phone.transform.LookAt(head.transform);
        phone.transform.eulerAngles = new Vector3(phone.transform.rotation.eulerAngles.x + 90, phone.transform.rotation.eulerAngles.y, phone.transform.rotation.eulerAngles.z);
        //phone.transform.LookAt(2*phone.transform.position - head.transform.position);
        //phone.transform.localEulerAngles = new Vector3(phone.transform.localEulerAngles.x + 90, phone.transform.localEulerAngles.y, phone.transform.localEulerAngles.z);
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

    void SpawnCheck()
    {
        if(spawnCount < spawnMax)
        {
            int index = Random.Range(0, animals.Length);
            GameObject toSpawn = animals[index];
            GameObject spawnLocation;
            
            if (index < 2)
            {
                spawnLocation = hillSpawns[Random.Range(0, hillSpawns.Length)];
                print(toSpawn+"Hill");
            }

            else if (index < 4)
            {
                spawnLocation = tableSpawns[Random.Range(0, tableSpawns.Length)];
                print(toSpawn + "Table");
            }

            else if (index < 5)
            {
                spawnLocation = gardenSpawns[Random.Range(0, gardenSpawns.Length)];
                print(toSpawn + "Garden");
            }

            else
            {
                spawnLocation = pondSpawns[Random.Range(0, pondSpawns.Length)];
                print(toSpawn + "Pond");
                
            }

            Instantiate(toSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
            spawnCount++;
        }
    }
}
