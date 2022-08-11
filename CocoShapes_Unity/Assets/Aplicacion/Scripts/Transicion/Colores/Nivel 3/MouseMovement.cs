using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public AnswerControlCol answerControlCol;

    //Para el movimiento del mouse
    public float rate;//para que el movimiento se mueva más o menos

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    //Para el array de los objetos vacíos que ayudan a obtener la distancia de cada objeto.
    public GameObject[] Objects;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[22];
    public SoundController audioSource;

    //Para que se muestren aleatoriamente los audios
    public int n;

    //Instrucciones(imágenes)
    public GameObject[] TextsCol;

    public AudioClip[] soundsToPlay = new AudioClip[1];

    void Start()
    {
        //Para iniciar la corrutina con el audio de la instrucción
        StartCoroutine(WaitInstructionCol());
    }

    //Corrutina que reproduce el audio de la instrucción
    public IEnumerator WaitInstructionCol()
    {
        ///Para que se reproduzca el audio del inicio (la instrucción)
        float recoTime = 0f;

        // AudioClip[] audios = new AudioClip[2] { sounds[8], sounds[9] };
        AudioClip[] audios = new AudioClip[1] { sounds[8] };
        StartCoroutine(audioSource.PlayAudio(audios));

        while (recoTime < sounds[8].length)
        {
            recoTime += Time.deltaTime;
            yield return null;
        }

        //Para que inicie la corrutina que muestra todo (Los textos,etc)
        StartCoroutine(FindColors());
    }

    void Update()
    {
        //Para el movimiento de la pantalla de la tablet (solo funciona en el Update)
        rate = 1;
        if (Input.touchCount > 0)
        {
            Vector2 pz2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            gameObject.transform.position = pz2 / rate;
        }

        //Para el movimiento del mouse
        // rate = 1;
        // Vector2 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // gameObject.transform.position = pz / rate;
    }

    //Para que no se repitan las instrucciones (Texts) se eliminan
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    public void RemoveText(int index)
    {
        RemoveAt(ref TextsCol, index);
    }

    public IEnumerator FindColors()
    {
        yield return new WaitForSeconds(1);
        //Para que aparezcan los textos y sonidos aleatoriamente
        n = UnityEngine.Random.Range(0, TextsCol.Length);
        //Para mostrar las instrucciones
        TextsCol[n].SetActive(true);//Para que se activen



        //Para definir las respuestas correctas según la instrucción que aparezca
        String GameObjectName = TextsCol[n].name;//Para obtener el nombre del texto
        if (GameObjectName == "InstructionGreen")
        {
            soundsToPlay[0] = sounds[0];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Green";
        }
        else if (GameObjectName == "InstructionRed")
        {
            soundsToPlay[0] = sounds[1];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Red";
        }
        else if (GameObjectName == "InstructionWhite")
        {
            soundsToPlay[0] = sounds[2];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "White";
        }
        else if (GameObjectName == "InstructionPurple")
        {
            soundsToPlay[0] = sounds[3];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Purple";
        }
        else if (GameObjectName == "InstructionBlack")
        {
            soundsToPlay[0] = sounds[4];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Black";
        }
        else if (GameObjectName == "InstructionYellow")
        {
            soundsToPlay[0] = sounds[5];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Yellow";
        }
        else if (GameObjectName == "InstructionBlue")
        {
            soundsToPlay[0] = sounds[6];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Blue";
        }
        else if (GameObjectName == "InstructionOrange")
        {
            soundsToPlay[0] = sounds[7];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlCol.AnswerCorrect = "Orange";
        }
        yield return null;
    }
}
