using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerController : MonoBehaviour
{

    //Para lo de las respuestas
    public string AnswerCorrect; //la que el usuario deberia presionar

    private string AnswerChild;//la que el usuario presionó realmente

    private int AnswerCorrects = 0;

    private int AnswerIncorrects = 0;

    //Para lo de los sonidos
    public AudioClip[] sounds = new AudioClip[10];
    public AudioControl1 audioSource;

    public bool isPressing;

    void Start()
    {
        isPressing = false;
    }
    void Update()
    {

        //Para saber si la respuesta es correcta o incorrecta
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnswerChild = "Red";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            AnswerChild = "Green";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AnswerChild = "Yellow";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AnswerChild = "Black";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnswerChild = "Orange";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        //Para BLUE como se repite por black
        if (Input.GetKeyDown(KeyCode.U))
        {
            AnswerChild = "Blue";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnswerChild = "Purple";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AnswerChild = "White";
            Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }

        if (isPressing)
        {

            if (AnswerChild == AnswerCorrect)
            {

                AnswerCorrects++;
                Debug.Log("Correct");
                //FALTA el audio NICE JOB
                StartCoroutine(audioSource.PlayAudio(sounds[8]));

            }
            if (AnswerChild != AnswerCorrect)
            {

                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //FALTA el audio Keep trying
                StartCoroutine(audioSource.PlayAudio(sounds[9]));


            }
            // FALTA Para que se acabe el juego

            //if (AnswerCorrects >= 8 || AnswerIncorrects == 3)
            // {
            //Debug.Log("Game Over");
            // }
        }

    }
}
