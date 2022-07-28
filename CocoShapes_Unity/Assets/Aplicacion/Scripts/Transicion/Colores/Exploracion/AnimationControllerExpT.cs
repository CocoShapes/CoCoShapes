using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerExpT : MonoBehaviour
{
    private LevelController_ExpT levelController;

    void Start(){
        levelController = GameObject.Find("LevelController").GetComponent<LevelController_ExpT>();
    }
    
    public IEnumerator PlayAnimationSugar(GameObject sugar, string animationName){
        Animator animator = sugar.GetComponent<Animator>();
        animator.Play(animationName);

        float recorredTime = 0;
        while (recorredTime < 4.5f){
            recorredTime += Time.deltaTime;
            yield return null;
        }
        
        levelController.requestColor();
    }
}
