using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementSha : MonoBehaviour
{
    public AnswerControlSha answerControlSha;

    //Para el movimiento del mouse
    public float rate;//para que el movimiento se mueva más o menos

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    //Para el array de los objetos vacíos que ayudan a obtener la distancia de cada objeto.
    public GameObject[] Shapes;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[6];
    public SoundControl audioSource;

    //Instrucciones(imágenes)
    public GameObject[] TextsSha;

    //Para que se muestren aleatoriamente los audios
    public int n;

    void Start()
    {
        //Para iniciar la corrutina con el audio de la instrucción
        StartCoroutine(WaitInstructionSha());
    }
    //Corrutina que reproduce el audio de la instrucción
    public IEnumerator WaitInstructionSha()
    {
        ///Para que se reproduzca el audio del inicio (la instrucción)
        float recoTime = 0f;

        // AudioClip[] audios = new AudioClip[2] { sounds[8], sounds[9] };
        AudioClip[] audios = new AudioClip[1] { sounds[5] };
        StartCoroutine(audioSource.PlayAudio(audios));

        while (recoTime < sounds[5].length)
        {
            recoTime += Time.deltaTime;
            yield return null;
        }

        //Para que inicie la corrutina que muestra todo (Los textos,etc)
        StartCoroutine(FindShapes());
    }

    //Para que no se repitan las instrucciones (Texts) se eliminan
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    public void RemoveText(int index)
    {
        RemoveAt(ref TextsSha, index);
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
    public IEnumerator FindShapes()
    {
        yield return new WaitForSeconds(1);
        //Para que aparezcan los textos y sonidos aleatoriamente
        n = UnityEngine.Random.Range(0, TextsSha.Length);
        //Para mostrar las instrucciones
        TextsSha[n].SetActive(true);//Para que se activen

        AudioClip[] soundsToPlay = new AudioClip[1];

        //Para definir las respuestas correctas según la instrucción que aparezca
        String GameObjectName = TextsSha[n].name;//Para obtener el nombre del texto
        if (GameObjectName == "InstructionCircle")
        {
            soundsToPlay[0] = sounds[0];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlSha.AnswerCorrect = "Circle";
        }
        else if (GameObjectName == "InstructionTriangle")
        {
            soundsToPlay[0] = sounds[1];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlSha.AnswerCorrect = "Triangle";
        }
        else if (GameObjectName == "InstructionStar")
        {
            soundsToPlay[0] = sounds[2];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlSha.AnswerCorrect = "Star";
        }
        else if (GameObjectName == "InstructionSquare")
        {
            soundsToPlay[0] = sounds[3];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlSha.AnswerCorrect = "Square";
        }
        else if (GameObjectName == "InstructionRectangle")
        {
            soundsToPlay[0] = sounds[4];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            answerControlSha.AnswerCorrect = "Rectangle";
        }
        yield return null;
    }
}
