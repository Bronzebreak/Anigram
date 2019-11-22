using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public AudioClip soundToPlay;
    public AudioSource mySound;
    public GameObject spawnPoint;

    //On initialization...
    private void Start()
    {
        //...encapsulate my audio source as a variable...
        mySound = gameObject.GetComponent<AudioSource>();

        //...and set the sound to play of my Audio Source based off of the publicly set variable Audio Clip variable.
        mySound.clip = soundToPlay;
    }

    //On trigger...
    public void PlaySound()
    {
        //...play my publicly set sound.
        mySound.Play();
    }
}
