using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFadeEffect : MonoBehaviour
{

    //Variables 
    public Material mat;
    public GameObject DemoBird;
    float alphaLevel = 1;
    float fadetime;

//---------------------------------------------------------------------------------------------------------------------

    //Upon starting the game, get the gameobject and set it's material as 'mat'
    private void Start()
    {
        DemoBird.GetComponent<Renderer>().material = mat;
    }

//---------------------------------------------------------------------------------------------------------------------


    void Update()
    {
        // Then, everytime the A key is pressed, set color to the mat color and have the alpha turned onto 1.
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A down");
            Color color = mat.color;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1); //f - 0.1f * (Time.deltaTime)
            print("a wild animal appeared");
        }

        // Then, everytime the S key is pressed, set color to the mat color and have the alpha turned onto 0.
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("S Down");
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0); //+ 0.1f / (Time.deltaTime)
            print("it ran away!");
        }
    }
}
