using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFadeEffect : MonoBehaviour
{

    //Variables 
    public Material mat;
    public GameObject DemoBird;

    float alphaLevel = 1;
    bool animalHidden;

    public GameObject spawnPartical1;
    public GameObject spawnPartical2;

    public GameObject playerLocation;

//-------------------------------------------------------------------------------------------------------------------------
    //Upon starting the game, get the gameobject and set it's material as 'mat', and hid the main animal.
    private void Start()
    {
        DemoBird.GetComponent<MeshRenderer>().material = mat;       
    }

//-------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // Then, everytime the A key is pressed, set color to the mat color and have the alpha turned onto 1.
        if (Input.GetKeyDown(KeyCode.A))
        {
            Color color = mat.color;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1);
            //Then get the players location, and spawn particals at that point.
            playerLocation = Instantiate(spawnPartical1, DemoBird.transform.position, Quaternion.identity);

        }

//-------------------------------------------------------------------------------------------------------------------------
        // Then, everytime the S key is pressed, set color to the mat color and have the alpha turned onto 0. When object is hidden play Blown away particle
        if (Input.GetKeyDown(KeyCode.S))
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
            //Destroy the player location, and then player the second partical until it is finaihed.
            Destroy(playerLocation);
            playerLocation = Instantiate(spawnPartical2, DemoBird.transform.position, Quaternion.identity);
        }
    }

//-------------------------------------------------------------------------------------------------------------------------
}
