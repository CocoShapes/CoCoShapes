using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{

    public AnswersController answerController;

    //Para mostrar las instrucciones de manera aleatoria
    public Transform position;
    public GameObject[] Texts;

    public GameObject[] Shapes;

    //Para lo de los sonidos
    public AudioClip[] sounds = new AudioClip[6];
    public Audio2 audioSource;

    //Para la respuestas

    public string AnswerCorrect;//la que el usuario deberia presionar


    void Start()
    {

    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject text in Texts)
            {
                text.SetActive(false);
            }

            //Para que las instrucciones se muestren aleatoriamente
            int n = Random.Range(0, Texts.Length);

            Texts[n].SetActive(true);

            //Para los audios de las instrucciones
            StartCoroutine(audioSource.PlayAudio(sounds[n]));

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

