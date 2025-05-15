using System;
using UnityEngine;
using System.Collections.Generic;

public class SoundCollection : MonoBehaviour
{


    public void Play( AudioSource audioSource, AudioClip audioClip = null , bool randomPitch = false,  float min = .8f, float max = 1f)
    {
        if (randomPitch) audioSource.pitch = UnityEngine.Random.Range(min, max);
        else audioSource.pitch = 1;
        if(audioClip == null)
            audioSource.Play();
        else{
            audioSource.PlayOneShot(audioClip);
        }
    }
}
