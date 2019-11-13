using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Overlord overlordRef;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        overlordRef = FindObjectOfType<Overlord>();
    }

    public void KillSelf()
    {
        overlordRef.spawnPoints.Add(spawnPoint);
        overlordRef.spawnCount -= 1;
        Destroy(gameObject);
    }
}
