using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsController : MonoBehaviour
{
    //Para llamar al código denominado AnimationCar para obtener AnswerChild.
    public AnimationCar animationCar;
    void Start()
    {

    }
    public void updateIsPressing(string answer)
    {
        //Para lo de los botones, AnswerChild es la respuesta que el usuario presionó
        animationCar.AnswerChild = answer;
        animationCar.isPressing = true;
    }
}
