using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControll : MonoBehaviour
{
    // Start is called before the first frame update
    public FollowPath followPathRef;
    public FollowPath followPathRef2;
    float time ;
    public GameObject Path_1;
    public GameObject Path_2;



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
        
    }

    IEnumerator Timer()
    {
        while (true){
            time =2.0f;
            followPathRef.TimeTotal = time;
            followPathRef.MyPath.i = 1;
            yield return new WaitForSeconds(1.3f);
            Path_1.SetActive(true);
            Path_2.SetActive(false);
            time =4.0f;
            followPathRef2.MyPath.i = 1;
            followPathRef2.TimeTotal = time;
            yield return new WaitForSeconds(time+0.6f);
            Path_1.SetActive(false);
            Path_2.SetActive(true);
            yield return new WaitForSeconds(time+0.5f);
        }

        

        
        
        
    }
}
