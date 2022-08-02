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
    public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    float distanceMin;

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[6];
    public SoundControl audioSource;

    //Instrucciones(imágenes)
    public GameObject[] TextsSha;

    //Para desactivar los círculos rojos
    public GameObject IncorrectCircle;
    public GameObject IncorrectTriangle;
    public GameObject IncorrectSquare;
    public GameObject IncorrectRectangle;
    public GameObject IncorrectStar;

    //Para que se muestren aleatoriamente los audios
    public int n;

    void Start()
    {
        //isPressing = false;
        //Para que se reproduzca el audio del inicio (las isntrucciones)
        AudioClip[] audios = new AudioClip[1] { sounds[5] };
        StartCoroutine(audioSource.PlayAudio(audios));
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
    public IEnumerator FindShapes()
    {
        yield return new WaitForSeconds(10);
        //Para que aparezcan los textos y sonidos aleatoriamente
        n = Random.Range(0, 4);
        AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
        StartCoroutine(audioSource.PlayAudio(soundsToPlay));

        //Para mostrar las instrucciones
        TextsSha[n].SetActive(true);//Para que se activen

        //Para definir las respuestas correctas
        if (n == 0)
        {
            answerControlSha.AnswerCorrect = "Circle";
        }
        else if (n == 1)
        {
            answerControlSha.AnswerCorrect = "Triangle";
        }
        else if (n == 2)
        {
            answerControlSha.AnswerCorrect = "Star";
        }
        else if (n == 3)
        {
            answerControlSha.AnswerCorrect = "Square";
        }
        else if (n == 4)
        {
            answerControlSha.AnswerCorrect = "Rectangle";
        }
        yield return null;
    }
    //Para la corrutina
    void OnEnable()
    {
        StartCoroutine(FindShapes());
    }
}
