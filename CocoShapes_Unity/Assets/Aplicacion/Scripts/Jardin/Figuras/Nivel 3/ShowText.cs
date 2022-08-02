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

    //Para las animaciones
    public Animator animator;

    void Start()
    {
        ///Para que se reproduzca el audio del inicio (la instrucción)
        AudioClip[] audios = new AudioClip[1] { sounds[5] };
        StartCoroutine(audioSource.PlayAudio(audios));
        //Para que se reproduzca la animación de saludando
        animator.Play("SaludandoShaJ");
    }

    //Método para que aparezcan las instrucciones
    public IEnumerator Show()
    {
        if (sounds[5] == null)
        {
            yield return new WaitForSeconds(0);
        }
        else
        {
            yield return new WaitForSeconds(sounds[5].length);
        }
        //Para que las instrucciones se muestren aleatoriamente
        int n = Random.Range(0, Texts.Length);
        Texts[n].SetActive(true);//Para que se activen

        //Para que NO SE REPITAN LAS INSTRUCCIONES (FALTA)

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
        yield return null;
    }

    //Para la corrutina
    void OnEnable()
    {
        StartCoroutine(Show());
    }
}



