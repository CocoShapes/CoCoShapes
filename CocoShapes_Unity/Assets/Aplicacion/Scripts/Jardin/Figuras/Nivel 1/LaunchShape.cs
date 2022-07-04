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
        spriteRenderer.sprite = spriteShape;

        switch(spriteRenderer.sprite.name){
            case "Circle":
                spriteRenderer.color = Color.red;
                break;
            case "Square":
                spriteRenderer.color = Color.green;
                break;
            case "Triangle":
                spriteRenderer.color = Color.blue;
                break;
            case "Star":
                spriteRenderer.color = Color.yellow;
                break;
            case "Rectangle":
                spriteRenderer.color = Color.magenta;
                break;
        }

        animator.Play("LaunchShape", -1, 0f);
    }
}