using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl1 : MonoBehaviour
{
    private AudioSource audioSource;

    public IEnumerator PlayAudio(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(1);
    }
}