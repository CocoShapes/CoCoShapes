using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerControlCol : MonoBehaviour
{
    public MouseMovement mouseMovement;
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
        //isPressing = false;
    }
    //Corrutina para que se terminen de reproducir los sonidos de correcto y nice job antes de girar la ruleta otra vez.
    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 8)
        {
            mouseMovement.StopCoroutine(mouseMovement.FindColors());
            Debug.Log("Game Over");
        }
        else
        {
            yield return new WaitForSeconds(3);
            //Para que se desactiven los textos
            foreach (GameObject textCol in mouseMovement.TextsCol)
            {
                textCol.SetActive(false);
            }
            //Para que se desactiven los círculos rojos
            IncorrectGreen.SetActive(false);
            IncorrectRed.SetActive(false);
            IncorrectWhite.SetActive(false);
            IncorrectPurple.SetActive(false);
            IncorrectBlack.SetActive(false);
            IncorrectYellow.SetActive(false);
            IncorrectBlue.SetActive(false);
            IncorrectOrange.SetActive(false);
            mouseMovement.StartCoroutine(mouseMovement.FindColors());
        }

    }
    void Update()
    {
        //Para saber que tecla se presonó y así conocer si la respuesta es correcta o incorrecta
        //Butterfly (yellow)
        //Yellow (F1)
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AnswerChild = "Yellow";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Flower(purple)
        //Purple (F6)
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnswerChild = "Purple";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Star(white)
        //White (F8)
        if (Input.GetKeyDown(KeyCode.W))
        {
            AnswerChild = "White";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Bird(red)
        //Red (F3)
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnswerChild = "Red";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Cat(black)
        //Black (F7)
        if (Input.GetKeyDown(KeyCode.B))
        {
            AnswerChild = "Black";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Green (F4)
        //Fish(green)
        if (Input.GetKeyDown(KeyCode.G))
        {
            AnswerChild = "Green";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Heart(blue)
        //Blue (F2)
        //Para BLUE como se repite por black
        if (Input.GetKeyDown(KeyCode.U))
        {
            AnswerChild = "Blue";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Bear(orange)
        //Orange (F5)
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnswerChild = "White";
            //Debug.Log("AnswerChild: " + AnswerChild);
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
                AudioClip[] audios = new AudioClip[2] { sounds[10], sounds[11] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
                //Corrutina para que se finalize el audio de NiceJob antes de girar la ruleta otra vez
                StartCoroutine(WaitForAudio());

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
                AudioClip[] audios = new AudioClip[2] { sounds[12], sounds[13] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
            }
        }
        //Para que cuando ya se hayan realizado las 8 o se hayan respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
        }
    }
}
