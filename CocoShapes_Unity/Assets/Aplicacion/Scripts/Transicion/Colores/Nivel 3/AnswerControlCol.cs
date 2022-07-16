using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerControlCol : MonoBehaviour
{
    //Para lo de las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar
    private string AnswerChild;//la que el usuario presionó realmente
    private int AnswerCorrects = 0;//para contar las respuestas correctas
    private int AnswerIncorrects = 0;//para contar las respuestas incorrectas

    //Para los audios
    public SoundController audioSource;

    public AudioClip[] sounds = new AudioClip[4];

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    //Para los círculos rojos cuando es incorrecta la respuesta.
    public GameObject IncorrectGreen;
    public GameObject IncorrectRed;
    public GameObject IncorrectWhite;
    public GameObject IncorrectPurple;
    public GameObject IncorrectBlack;
    public GameObject IncorrectYellow;
    public GameObject IncorrectBlue;
    public GameObject IncorrectOrange;

    void Start()
    {
        //Al inicio no se ha presionado ninguna tecla
        isPressing = false;
    }
    void Update()
    {
        //Para saber que tecla se presonó y así conocer si la respuesta es correcta o incorrecta
        //Butterfly (yellow)
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AnswerChild = "Yellow";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Flower(purple)
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnswerChild = "Purple";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Star(white)
        if (Input.GetKeyDown(KeyCode.W))
        {
            AnswerChild = "White";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Bird(red)
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnswerChild = "Red";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Cat(black)
        if (Input.GetKeyDown(KeyCode.B))
        {
            AnswerChild = "Black";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Fish(green)
        if (Input.GetKeyDown(KeyCode.G))
        {
            AnswerChild = "Green";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Heart(blue)
        //Para BLUE como se repite por black
        if (Input.GetKeyDown(KeyCode.U))
        {
            AnswerChild = "Blue";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Bear(orange)
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnswerChild = "White";
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
                if (AnswerCorrect == "Green")
                {
                    IncorrectGreen.SetActive(true);
                }
                if (AnswerCorrect == "Red")
                {
                    IncorrectRed.SetActive(true);
                }
                if (AnswerCorrect == "White")
                {
                    IncorrectWhite.SetActive(true);
                }
                if (AnswerCorrect == "Purple")
                {
                    IncorrectPurple.SetActive(true);
                }
                if (AnswerCorrect == "Black")
                {
                    IncorrectBlack.SetActive(true);
                }
                if (AnswerCorrect == "Yellow")
                {
                    IncorrectYellow.SetActive(true);
                }
                if (AnswerCorrect == "Blue")
                {
                    IncorrectBlue.SetActive(true);
                }
                if (AnswerCorrect == "Orange")
                {
                    IncorrectOrange.SetActive(true);
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
