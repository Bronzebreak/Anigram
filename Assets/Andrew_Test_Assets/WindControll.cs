using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControll : MonoBehaviour
{
    // Start is called before the first frame update
    public FollowPath followPathRef;
    public FollowPath followPathRef2;
    public GameObject spawnPoint;
    float time1 = 2.3f;
    float time2 = 4.3f;
    float timeDelay = 0.3f;
    float timeNow;
    public GameObject particlePrefab1;
    public GameObject particleInstance1;
    public GameObject particlePrefab2;
    public GameObject particleInstance2;


    public void FullCircle(){
        StartCoroutine( Timer());
    }
    void Start()
    {

        FullCircle();
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider[] bc = GetComponentsInChildren<BoxCollider>();
        timeNow = Time.realtimeSinceStartup;
    }

    IEnumerator Timer()
    {
        while (true){
            time1 = 2 + (1 * (timeNow / 300.0f));
            time2 = 4 + (3 * (timeNow / 300.0f));
            timeDelay = 0.3f + (0.3f * (timeNow / 300.0f));
            particleInstance1 = Instantiate(particlePrefab1, spawnPoint.transform.position, Quaternion.identity);
            followPathRef = particleInstance1.GetComponentInChildren<FollowPath>();
            followPathRef.TimeTotal = time1;
            particleInstance1.SetActive(false);
            particleInstance2 = Instantiate(particlePrefab2, spawnPoint.transform.position, Quaternion.identity);
            followPathRef2 = particleInstance2.GetComponentInChildren<FollowPath>();
            followPathRef2.TimeTotal = time2;
            particleInstance2.SetActive(false);
            yield return new WaitForSeconds(timeDelay);
            particleInstance1.SetActive(true);
            yield return new WaitForSeconds(time1 + timeDelay);
            Destroy(particleInstance1);
            particleInstance2.SetActive(true);
            yield return new WaitForSeconds(time2 + timeDelay);
            particleInstance2.SetActive(false);
            Destroy(particleInstance2);
            

        }

        

        
        
        
    }
}
