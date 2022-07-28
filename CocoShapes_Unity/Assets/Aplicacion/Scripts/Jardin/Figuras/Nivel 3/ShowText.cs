using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    //Para llamar al código denominado AnswerController para que pueda usar allá la variable AnswerCorrect
    public AnswersController answerController;

    //Para mostrar las instrucciones de manera aleatoria
    //public Transform position;
    public GameObject[] Texts;//Las instrucciones (son imágenes)

    //Las figuras
    public GameObject[] Shapes;//Son imágenes

    //Para los audios
    public AudioClip[] sounds = new AudioClip[10];
    public Audio2 audioSource;

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    //Para desactivar los círculos rojos
    public GameObject[] IncorrectsCircles;

    int n;

    //Para que no se repitan las instrucciones FALTA
    //List<int> numerosGuardados = new List<int>();
    void Start()
    {
        //Para que se reproduzca el audio del inicio (la instrucción)
        AudioClip[] audios = new AudioClip[1] { sounds[5] };
        StartCoroutine(audioSource.PlayAudio(audios));
    }

    void Update()
    {
        //Para que se muestren las instrucciones
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Para que se desactiven los textos
            foreach (GameObject text in Texts)
            {
                text.SetActive(false);
            }
            //Para que se desactiven los círculos rojos
            foreach (GameObject incorrects in IncorrectsCircles)
            {
                incorrects.SetActive(false);
            }
            //Para que se desactiven las figuras
            foreach (GameObject shapes in Shapes)
            {
                shapes.SetActive(false);
            }
            //Para que las instrucciones se muestren aleatoriamente
            int n = Random.Range(0, Texts.Length);
            Texts[n].SetActive(true);//Para que se activen

            //Para que NO SE REPITAN LAS INSTRUCCIONES


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

