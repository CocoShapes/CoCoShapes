using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    public string color;
    public bool playedSound = false;

    private Animator ballAnimator;

    private Animator homeRedAnimator;
    private Animator homeBlueAnimator;
    private Animator homeYellowAnimator;

    void Start()
    {
        ballAnimator = GetComponent<Animator>();

        homeRedAnimator = GameObject.Find("HomeRed").GetComponent<Animator>();
        homeBlueAnimator = GameObject.Find("HomeBlue").GetComponent<Animator>();
        homeYellowAnimator = GameObject.Find("HomeYellow").GetComponent<Animator>();
    }
    
    public IEnumerator launch()
    {
        float recorredTime = 0;

        switch(color){
            case "Red":
                ballAnimator.Play("BallRed");
                homeRedAnimator.Play("HomeRed");
                break;
            case "Blue":
                ballAnimator.Play("BallBlue");
                homeBlueAnimator.Play("HomeBlue");
                break;
            case "Yellow":
                ballAnimator.Play("BallYellow");
                homeYellowAnimator.Play("HomeYellow");
                break;
        }
        
        while (recorredTime < 1.1f)
        {
            recorredTime += Time.deltaTime;
            yield return null;
        }
    }

    public void PlayMetalAudio()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();

        audioSource.Play();
    }
}
