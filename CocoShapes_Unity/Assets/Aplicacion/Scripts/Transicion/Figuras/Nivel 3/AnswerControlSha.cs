using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerControlSha : MonoBehaviour
{
    //Para lo de las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar
    private string AnswerChild;//la que el usuario presionó realmente
    private int AnswerCorrects = 0;//para contar las respuestas correctas
    private int AnswerIncorrects = 0;//para contar las respuestas incorrectas

    //Para los audios
    public SoundControl audioSource;

    public AudioClip[] sounds = new AudioClip[4];

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    //Para los círculos rojos cuando es incorrecta la respuesta.
    public GameObject IncorrectCircle;
    public GameObject IncorrectTriangle;
    public GameObject IncorrectSquare;
    public GameObject IncorrectRectangle;
    public GameObject IncorrectStar;
    void Start()
    {
        //Al inicio no se ha presionado ninguna tecla
        isPressing = false;
    }
    void Update()
    {
        //Para saber que tecla se presonó y así conocer si la respuesta es correcta o incorrecta
        //Balón(circle)
        if (Input.GetKeyDown(KeyCode.C))
        {
            AnswerChild = "Circle";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Pizza(Triangle)
        if (Input.GetKeyDown(KeyCode.T))
        {
            AnswerChild = "Triangle";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Windows(Square)
        if (Input.GetKeyDown(KeyCode.S))
        {
            AnswerChild = "Square";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Chocolate(Rectangle)
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnswerChild = "Rectangle";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Star(star)
        ////Para star no pude repetir s por eso se usó la tecla w
        if (Input.GetKeyDown(KeyCode.W))
        {
            AnswerChild = "Star";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Se presionó una tecla y ahora se comparan si la respuesta es correcta
        //(se compara answerCorrect con answerChild)
        if (isPressing)
        {
            //Si las dos son iguales
            if (AnswerChild == AnswerCorrect)
            {
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { sounds[0], sounds[1] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
            }
            //Si las dos NO son iguales
            if (AnswerChild != AnswerCorrect)
            {
                //Para que aparezcan los círculos rojos
                if (AnswerCorrect == "Circle")
                {
                    IncorrectCircle.SetActive(true);
                }
                if (AnswerCorrect == "Triangle")
                {
                    IncorrectTriangle.SetActive(true);
                }
                if (AnswerCorrect == "Square")
                {
                    IncorrectSquare.SetActive(true);
                }
                if (AnswerCorrect == "Rectangle")
                {
                    IncorrectRectangle.SetActive(true);
                }
                if (AnswerCorrect == "Star")
                {
                    IncorrectStar.SetActive(true);
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { sounds[2], sounds[3] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
            }
        }
        //Para que cuando ya se hayan realizado las 8 o se hayan respondido 3 incorrectas
        if (AnswerCorrects >= 8 || AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
        }

    }
}
