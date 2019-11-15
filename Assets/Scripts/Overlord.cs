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
        InvokeRepeating("Spawn", 30, 5);
        head = centerEye.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGazePointerProperties(breathDelay, inhaleTime);
        phone.transform.LookAt(head.transform);
        phone.transform.eulerAngles = new Vector3(phone.transform.rotation.eulerAngles.x + 90, phone.transform.rotation.eulerAngles.y, phone.transform.rotation.eulerAngles.z);
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

    void Spawn()
    { 
        if (spawnCount < spawnMax)
        {
            int index = Random.Range(0, animals.Length);
            GameObject toSpawn = animals[index];
            GameObject spawnLocation;
            string zone;

            print(index.ToString());
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
            print(zone);

            results = spawnPoints.FindAll(
            delegate(GameObject point)
            {
            return point.name.Contains(zone);
            }
            );

            int spawnIndex = Random.Range(0, results.Count);
            spawnLocation = results[spawnIndex];
            GameObject spawned = Instantiate(toSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
            if (index == 0 || index == 2)
            {
                spawned.transform.position = spawned.transform.position + new Vector3 (0, 2, 0);
            }

            spawned.GetComponent<Destroy>().spawnPoint = results[spawnIndex];
            spawnPoints.Remove(results[spawnIndex]);
            spawnCount++;    
        }
    }
}
