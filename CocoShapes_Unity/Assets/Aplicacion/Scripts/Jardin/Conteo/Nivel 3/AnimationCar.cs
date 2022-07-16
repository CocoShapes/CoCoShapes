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

    //Para mostrar las instrucciones(son imágenes)
    public GameObject[] Instructions;
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
                    animator.Play("Rail4");
                }
                if (n == 1)
                {
                    animator.Play("Rail7");
                }
                if (n == 2)
                {
                    animator.Play("Rail10");
                }
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { rollerCoster.sounds[3], rollerCoster.sounds[4] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
            }
            if (AnswerChild != AnswerCorrect)
            {
                //Para poner de color rojo la instrucción
                if (n == 0)
                {
                    Instructions[0].GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (n == 1)
                {
                    Instructions[1].GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (n == 2)
                {
                    Instructions[2].GetComponent<SpriteRenderer>().color = Color.red;
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { sounds[5], sounds[6] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
            }
        }
        //Para que cuando ya se hayan realizado las 3 o se hayan respondido 3 incorrectas
        if (AnswerCorrects >= 3 || AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
        }
    }
}
