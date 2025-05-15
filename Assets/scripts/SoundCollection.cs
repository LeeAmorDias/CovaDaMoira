using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundCollection : MonoBehaviour
{


    public void Play(AudioSource audioSource, AudioClip audioClip = null, bool randomPitch = false,float time = 0, float min = .8f, float max = 1f)
    {
        if (randomPitch) audioSource.pitch = UnityEngine.Random.Range(min, max);
        else audioSource.pitch = 1;
        if (audioClip == null)
            audioSource.Play();
        else
        {
            StartCoroutine(PlayAfterDelay(audioSource,audioClip,time));
            
        }
    }
    IEnumerator PlayAfterDelay(AudioSource audioSource, AudioClip audioClip,float time)
    {
        yield return new WaitForSeconds(time); // Wait for 1 second
        audioSource.PlayOneShot(audioClip);
    }
}
