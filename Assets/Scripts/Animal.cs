using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public AudioClip soundToPlay;
    public AudioSource mySound;
    public GameObject spawnPoint;

    private void Start()
    {
        mySound = gameObject.GetComponent<AudioSource>();
        mySound.clip = soundToPlay;
    }

    public void PlaySound()
    {
        mySound.Play();
    }
}
