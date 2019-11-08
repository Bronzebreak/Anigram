using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControll : MonoBehaviour
{
    // Start is called before the first frame update
    public FollowPath followPathRef;
    public FollowPath followPathRef2;
    public GameObject spawnPoint;
    public float time1;
    public float time2;
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
        
    }

    IEnumerator Timer()
    {
        while (true){
            time1 = 2.0f;
            yield return new WaitForSeconds(0.3f);
            particleInstance1 = Instantiate(particlePrefab1, spawnPoint.transform.position, Quaternion.identity);
            followPathRef = particleInstance1.GetComponentInChildren<FollowPath>();
            followPathRef.TimeTotal = time1;  
            yield return new WaitForSeconds(time1 + 0.6f);
            Destroy(particleInstance1);
            particleInstance2 = Instantiate(particlePrefab2, spawnPoint.transform.position, Quaternion.identity);
            followPathRef2 = particleInstance2.GetComponentInChildren<FollowPath>();
            time2 = 4.0f;
            followPathRef2.TimeTotal = time2;
            yield return new WaitForSeconds(time2 + 0.5f);
            Destroy(particleInstance2);
            print("3");
        }

        

        
        
        
    }
}
