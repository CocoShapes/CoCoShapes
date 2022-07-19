using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerCon1T : MonoBehaviour
{
    private LevelController_Con1T levelController;

    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController_Con1T>();
    }
    
    
    public void evaluateAnswer(Text text)
    {
        levelController.txtR3.text = text.text;
        levelController.answer  = int.Parse(text.text);
        
        if (text.text == levelController.numberToEvaluate.ToString())
        {
            levelController.correctAnswer();
        }
        else
        {
            levelController.wrongAnswer();
        }
    }
}
