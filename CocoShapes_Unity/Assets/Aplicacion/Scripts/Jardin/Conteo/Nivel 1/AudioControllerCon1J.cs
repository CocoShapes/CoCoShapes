using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerCon1J : MonoBehaviour
{
    public IEnumerator PlayAudio(AudioClip[] audioClips)
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();

        for (int i = 0; i < audioClips.Length; i++)
        {
            float playedTime = 0;

            audioSource.clip = audioClips[i];
            audioSource.Play();

            while(playedTime < audioClips[i].length)
            {
                playedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
