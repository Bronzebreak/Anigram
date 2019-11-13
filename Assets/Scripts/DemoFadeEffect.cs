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

    public ParticleSystem DustParticle;
    public ParticleSystem BlownAwayParticle;

//-------------------------------------------------------------------------------------------------------------------------
    //Upon starting the game, get the gameobject and set it's material as 'mat', and hid the main animal.
    private void Start()
    {
        DemoBird.GetComponent<MeshRenderer>().material = mat;
        animalHidden = true;
    }

//-------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // Then, everytime the A key is pressed, set color to the mat color and have the alpha turned onto 1.
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A down");
            Color color = mat.color;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1);
            animalHidden = false;
            print("a wild animal appeared");
            print("made it!");
        }

//-------------------------------------------------------------------------------------------------------------------------
        // Then, everytime the S key is pressed, set color to the mat color and have the alpha turned onto 0. When object is hidden play Blown away particle
        if (Input.GetKeyDown(KeyCode.S))
        {
            //fadeIn();
            print("S Down");
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
            print("it ran away!");
            animalHidden = true;
            BlownAwayParticle.Play();
            print("made it2!");
        }
//-------------------------------------------------------------------------------------------------------------------------

        // if the animal is hidden, have the particles stop.
        if (animalHidden == true)
        {
            DustParticle.Stop();
        }
        //Otherwise, in the animal is not hidden, play particle effect.
        else if (animalHidden == false)
        {
            DustParticle.Play();
        }
    }
//-------------------------------------------------------------------------------------------------------------------------


    /*
        void fadeIn()
        {
            while (mat.color.a > 0)
            {
                Color newColor = mat.color;
                newColor.a -= Time.deltaTime;
                mat.color = newColor;
            }
        }
    */



}
