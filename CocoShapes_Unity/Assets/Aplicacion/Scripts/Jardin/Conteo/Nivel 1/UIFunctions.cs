using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    public LevelControllerCont1J levelController;

    public void checkAnswer(Text txt){
        string answer = txt.text;

        if(answer == levelController.correctAnswer.ToString()){
            levelController.goodAnswer();
        }
        else{
            levelController.badAnswer();
        }
    }
}
