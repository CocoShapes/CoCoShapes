using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    //Para llamar al código denominado AnswerController para que pueda usar allá la variable AnswerCorrect
    public AnswersController answerController;

    //Para mostrar los textos de manera aleatoria
    public GameObject[] Texts;//Los textos (son imágenes)

    //Las figuras
    public GameObject[] Shapes;//Son imágenes

    //Para los audios
    public AudioClip[] sounds = new AudioClip[11];
    public Audio2 audioSource;

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    public int n;

    //Para las animaciones
    public Animator animator;

    void Start()
    {
        //Para iniciar la corrutina con el audio de la instrucción
        StartCoroutine(WaitInstruction());

    }
    //Corrutina que reproduce el audio de la instrucción
    public IEnumerator WaitInstruction()
    {
        //Para que se reproduzca la animación de saludando
        animator.Play("SaludandoShaJ");

        ///Para que se reproduzca el audio del inicio (la instrucción)
        float recoTime = 0f;

        AudioClip[] audios = new AudioClip[1] { sounds[5] };
        StartCoroutine(audioSource.PlayAudio(audios));

        while (recoTime < sounds[5].length)
        {
            recoTime += Time.deltaTime;
            yield return null;
        }

        //Para que inicie la corrutina que muestra todo (Los textos,etc)
        StartCoroutine(Show());
    }

    //Para que no se repitan las instrucciones (Texts) se eliminan
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    public void RemoveText(int index)
    {
        RemoveAt(ref Texts, index);
    }

    //Corrutina que muestra todo (Los textos,etc)
    public IEnumerator Show()
    {
        yield return new WaitForSeconds(1);
        //Para que los textos se muestren aleatoriamente
        n = UnityEngine.Random.Range(0, Texts.Length);
        Texts[n].SetActive(true);
        //Para los audios de los textos
        AudioClip[] soundsToPlay = new AudioClip[1];

        //Para definir las respuestas correctas según el texto que aparezca
        String GameObjectName = Texts[n].name;//Para obtener el nombre del texto

        if (GameObjectName == "InstructionCircle")
        {
            soundsToPlay[0] = sounds[0];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerController.AnswerCorrect = "Circle";
        }
        if (GameObjectName == "InstructionRectangle")
        {
            soundsToPlay[0] = sounds[1];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerController.AnswerCorrect = "Rectangle";
        }
        if (GameObjectName == "InstructionSquare")
        {
            soundsToPlay[0] = sounds[2];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerController.AnswerCorrect = "Square";
        }
        if (GameObjectName == "InstructionTriangle")
        {
            soundsToPlay[0] = sounds[3];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerController.AnswerCorrect = "Triangle";
        }
        if (GameObjectName == "InstructionStar")
        {
            soundsToPlay[0] = sounds[4];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerController.AnswerCorrect = "Star";
        }
        yield return null;
    }
}



