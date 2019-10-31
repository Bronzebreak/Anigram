using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFreeze : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 myPos;
    void Start()
    {
        myPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = myPos;
    }
}
