using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchShape : MonoBehaviour
{
    public GameObject animatedShape;

    void Start()
    {
        animatedShape = GameObject.Find("AnimatedShape");
    }
    
    public void Launch(Sprite spriteShape)
    {
        Animator animator = animatedShape.GetComponent<Animator>();
    
        SpriteRenderer spriteRenderer = animatedShape.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = spriteShape;

        switch(spriteRenderer.sprite.name){
            case "Circle":
                animator.Play("LaunchCircle");
                break;
            case "Square":
                animator.Play("LaunchSquare");
                break;
            case "Triangle":
                animator.Play("LaunchTriangle");
                break;
            case "Star":
                animator.Play("LaunchStar");
                break;
            case "Rectangle":
            animator.Play("LaunchRectangle");
                break;
        }
    }
}