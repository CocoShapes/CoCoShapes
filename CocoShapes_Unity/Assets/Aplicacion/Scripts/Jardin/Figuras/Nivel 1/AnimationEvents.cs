using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public LevelController levelController;
    public GameObject bubbleShape;
    
    public void AnimationLaunchShapeFinish()
    {
        levelController.isPlaying = false;
    }

    public void ActivateBubble()
    {
        bubbleShape.SetActive(true);
    }

    public void deactivateBubble()
    {
        bubbleShape.SetActive(false);
    }

    public void PlayCanyonSound()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.Play();
    }
}