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
    
    public void Launch(string shape)
    {
        Animator animator = animatedShape.GetComponent<Animator>();

        switch(shape){
            case "Circle":
                animator.Play("LaunchCircle", -1, 0f);
                break;
            case "Square":
                animator.Play("LaunchSquare", -1, 0f);
                break;
            case "Triangle":
                animator.Play("LaunchTriangle", -1, 0f);
                break;
            case "Star":
                animator.Play("LaunchStar", -1, 0f);
                break;
            case "Rectangle":
            animator.Play("LaunchRectangle", -1, 0f);
                break;
        }
    }
}