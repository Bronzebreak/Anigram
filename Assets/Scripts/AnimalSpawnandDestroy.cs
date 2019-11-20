using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnandDestroy : MonoBehaviour
{
    public Overlord overlordRef;
    public GameObject spawnPoint;
    public Material myMaterial;
    public bool active;
    public ParticleSystem activateParticle;
    public ParticleSystem deactivateParticle;
    public Color myColour;

    // Start is called before the first frame update
    void Start()
    {
        myColour = myMaterial.color;
        overlordRef = FindObjectOfType<Overlord>();
        gameObject.GetComponent<MeshRenderer>().material = myMaterial;
    }

    public void ToggleActivation()
    {
        if (active == false)
        {
            myColour.a = 1;
            deactivateParticle.Stop(true);
            activateParticle.Play(true);
        }

        else
        {
            myColour.a = 0;
            activateParticle.Stop(true);
            deactivateParticle.Play(true);
        }
    }

    public void KillSelf()
    {
        overlordRef.spawnPoints.Add(spawnPoint);
        overlordRef.spawnCount -= 1;
        Destroy(gameObject);
    }
}
