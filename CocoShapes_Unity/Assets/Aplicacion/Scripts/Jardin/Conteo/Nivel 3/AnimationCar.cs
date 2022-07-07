using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCar : MonoBehaviour
{
    //Para llamar al código denominado RollerCoster para los audios.
    private RollerCoster rollerCoster;

    //Para las animaciones
    public Animator animator;

    //Para las respuestas
    public string AnswerChild;//la que el usuario presionó realmente
    public string AnswerCorrect;//la que el usuario debería presionar
    private int AnswerCorrects = 0;//Para contar las respuestas correctas
    private int AnswerIncorrects = 0;//Para contar las respuestas incorrectas

    //Para presionar el botón
    public bool isPressing;

    //Para obtener que riel se está mostrando
    public int n;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[14];
    public AudioControl audioSource;

    void Start()
    {
        //En un inicio no se está presionando ningún botón
        isPressing = false;
        //Para las animaciones
        animator = GetComponent<Animator>();
        //Para obtener variables del código denominado RollerCoster para los audios.
        rollerCoster = GameObject.Find("Background").GetComponent<RollerCoster>();

    }

    void Update()
    {
        //Se presionó un botón y ahora se compara si la respuesta es correcta
        //(se compara answerCorrect con answerChild)
        if (isPressing)
        {
            //Si las dos son iguales
            if (AnswerChild == AnswerCorrect)
            {
                if (n == 0)
                {
                    //Se ejecuta la animación
                    animator.Play("Rail1");
                }
                if (n == 1)
                {
                    animator.Play("Rail2");
                }
                if (n == 2)
                {
                    animator.Play("Rail3");
                }
                if (n == 3)
                {
                    animator.Play("Rail4");
                }
                if (n == 4)
                {
                    animator.Play("Rail5");
                }
                if (n == 5)
                {
                    animator.Play("Rail6");
                }
                if (n == 6)
                {
                    animator.Play("Rail7");
                }
                if (n == 7)
                {
                    animator.Play("Rail8");
                }
                if (n == 8)
                {
                    animator.Play("Rail9");
                }
                if (n == 9)
                {
                    animator.Play("Rail10");
                }
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { rollerCoster.sounds[12], rollerCoster.sounds[10] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
            }
            if (AnswerChild != AnswerCorrect)
            {
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { sounds[13], sounds[11] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
            }
        }
        //Para que cuando ya se hayan realizado las 10 o se hayan respondido 3 incorrectas
        if (AnswerCorrects >= 10 || AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
        }
    }
}
