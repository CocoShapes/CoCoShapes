using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    //Para llamar al código denominado AnswerController para que pueda usar allá la variable AnswerCorrect
    public AnswersController answerController;

    //Para mostrar las instrucciones de manera aleatoria
    public Transform position;
    public GameObject[] Texts;//Las instrucciones (son imágenes)

    //Las figuras
    public GameObject[] Shapes;//Son imágenes

    //Para los audios
    public AudioClip[] sounds = new AudioClip[9];
    public Audio2 audioSource;

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    void Start()
    {

    }

    void Update()
    {
        //Para que se muestren las instrucciones
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Para que se desactiven
            foreach (GameObject text in Texts)
            {
                text.SetActive(false);
            }
            //Para que las instrucciones se muestren aleatoriamente
            int n = Random.Range(0, Texts.Length);
            Texts[n].SetActive(true);//Para que se activen

            //Para los audios de las instrucciones
            AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));

            //Para definir las respuestas correctas
            if (n == 0)
            {
                answerController.AnswerCorrect = "Circle";
            }
            if (n == 1)
            {
                answerController.AnswerCorrect = "Rectangle";
            }
            if (n == 2)
            {
                answerController.AnswerCorrect = "Square";
            }
            if (n == 3)
            {
                answerController.AnswerCorrect = "Triangle";
            }
            if (n == 4)
            {
                answerController.AnswerCorrect = "Star";
            }

        }

    }
}

