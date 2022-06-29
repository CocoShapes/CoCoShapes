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
        string actualShape = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        
        switch(actualShape){
            case "Circle":
                bubbleShape.SetActive(true);
                bubbleShape.GetComponent<SpriteRenderer>().sprite = levelController.spritesBubbles[0];
                break;
            case "Square":
                bubbleShape.SetActive(true);
                bubbleShape.GetComponent<SpriteRenderer>().sprite = levelController.spritesBubbles[1];
                break;
            case "Triangle":
                bubbleShape.SetActive(true);
                bubbleShape.GetComponent<SpriteRenderer>().sprite = levelController.spritesBubbles[2];
                break;
            case "Star":
                bubbleShape.SetActive(true);
                bubbleShape.GetComponent<SpriteRenderer>().sprite = levelController.spritesBubbles[3];
                break;
            case "Rectangle":
                bubbleShape.SetActive(true);
                bubbleShape.GetComponent<SpriteRenderer>().sprite = levelController.spritesBubbles[4];
                break;
        }
    }

    public void deactivateBubble()
    {
        bubbleShape.SetActive(false);
    }
}