using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllerCouT : MonoBehaviour
{
    //Para poder utilizar el AnswerChild en el script AnswerCouT
    public AnswerCouT answerCouT;

    void Start()
    {

    }

    public void updateIsPressing(string answer)
    {
        //Para lo de los botones, AnswerChild es la respuesta que el usuario presionó
        answerCouT.AnswerChild = answer;
        answerCouT.isPressing = true;
    }
}
