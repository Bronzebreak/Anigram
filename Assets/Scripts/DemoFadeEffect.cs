using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFadeEffect : MonoBehaviour
{

    //Variables 
    public Material mat;
    public GameObject DemoBird;

//---------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        DemoBird.GetComponent<Renderer>().material = mat;
    }

//---------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A down");

            if (gameObject.tag == "DemoBird")
            {
                Color color = mat.color;
                color.a = 1f;
                print("a wild animal appeared");
            }
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            print("S Down");
            if (gameObject.tag == "DemoBird")
            {
                Color color = mat.color;
                color.a = 0f;
                print("it ran away!");
            }
        }
    }
}
