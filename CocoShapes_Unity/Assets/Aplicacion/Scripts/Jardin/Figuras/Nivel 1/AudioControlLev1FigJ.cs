using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlLev1FigJ : MonoBehaviour
{
    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playAudio(AudioClip audioClip){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
