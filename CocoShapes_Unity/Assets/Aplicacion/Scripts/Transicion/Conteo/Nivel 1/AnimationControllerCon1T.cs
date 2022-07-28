using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerCon1T : MonoBehaviour
{
    private LevelController_Con1T levelController;
    
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController_Con1T>();
    }

    public IEnumerator PlayGoodAnimation(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();
        animator.Play("CarritoGood");

        float time = 0;

        while(time < 2.1f){
            time += Time.deltaTime;
            yield return null;
        }

        levelController.correctAnswers++;
        levelController.selectNumber();
        
        levelController.txtR3.text = "";
        levelController.showAnswerButtons();
    }

    public IEnumerator PlayBadAnimation(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();
        animator.Play("CarritoBad");

        float time = 0;

        while (time < 2.1f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        levelController.wrongAnswers++;

        levelController.txtR3.text = "";
        levelController.showAnswerButtons();
    }
}
