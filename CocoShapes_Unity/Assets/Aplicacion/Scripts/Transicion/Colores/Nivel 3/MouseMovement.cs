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
    //public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    //float distanceMin;

    //Para que se sepa si se presionó una tecla
    //public bool isPressing;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[10];
    public SoundController audioSource;

    //Para que se muestren aleatoriamente los audios
    public int n;

    //Instrucciones(imágenes)
    public GameObject[] TextsCol;

    //Para desactivar los círculos rojos
    public GameObject IncorrectGreen;
    public GameObject IncorrectRed;
    public GameObject IncorrectWhite;
    public GameObject IncorrectPurple;
    public GameObject IncorrectBlack;
    public GameObject IncorrectYellow;
    public GameObject IncorrectBlue;
    public GameObject IncorrectOrange;

    void Start()
    {
        //Para que se reproduzca el audio del inicio (las isntrucciones)
        // AudioClip[] audios = new AudioClip[2] { sounds[8], sounds[9] };
        AudioClip[] audios = new AudioClip[1] { sounds[8] };
        StartCoroutine(audioSource.PlayAudio(audios));
        //isPressing = false;
    }

    void Update()
    {
        //Para el movimiento de la pantalla de la tablet (solo funciona en el Update)
        // rate = 1;
        // if (Input.touchCount > 0)
        // {
        //     Vector2 pz2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //     gameObject.transform.position = pz2 / rate;
        // }

        //Para el movimiento del mouse
        rate = 1;
        Vector2 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = pz / rate;
    }
    public IEnumerator FindColors()
    {
        yield return new WaitForSeconds(8);
        //Para que aparezcan los textos y sonidos aleatoriamente
        n = Random.Range(0, 7);
        AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
        StartCoroutine(audioSource.PlayAudio(soundsToPlay));

        //Para mostrar las instrucciones
        TextsCol[n].SetActive(true);//Para que se activen

        //Para definir las respuestas correctas
        if (n == 0)
        {
            answerControlCol.AnswerCorrect = "Green";
        }
        else if (n == 1)
        {
            answerControlCol.AnswerCorrect = "Red";
        }
        else if (n == 2)
        {
            answerControlCol.AnswerCorrect = "White";
        }
        else if (n == 3)
        {
            answerControlCol.AnswerCorrect = "Purple";
        }
        else if (n == 4)
        {
            answerControlCol.AnswerCorrect = "Black";
        }
        else if (n == 5)
        {
            answerControlCol.AnswerCorrect = "Yellow";
        }
        else if (n == 6)
        {
            answerControlCol.AnswerCorrect = "Blue";
        }
        else if (n == 7)
        {
            answerControlCol.AnswerCorrect = "Orange";
        }
        yield return null;
    }
    //Para la corrutina
    void OnEnable()
    {
        StartCoroutine(FindColors());
    }
}
