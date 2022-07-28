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
    public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    float distanceMin;

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

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
        AudioClip[] audios = new AudioClip[2] { sounds[8], sounds[9] };
        StartCoroutine(audioSource.PlayAudio(audios));
        isPressing = false;
        sceneNewCol();
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
    }
    public void sceneNewCol()
    {
        //Para el movimiento del mouse
        //rate = 1;
        // Vector2 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // gameObject.transform.position = pz / rate;

        n = Random.Range(0, 7);
        AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
        StartCoroutine(audioSource.PlayAudio(soundsToPlay));

        //Para mostrar las instrucciones
        TextsCol[n].SetActive(true);//Para que se activen

        //Para que se desactiven los círculos rojos
        IncorrectGreen.SetActive(false);
        IncorrectRed.SetActive(false);
        IncorrectWhite.SetActive(false);
        IncorrectPurple.SetActive(false);
        IncorrectBlack.SetActive(false);
        IncorrectYellow.SetActive(false);
        IncorrectBlue.SetActive(false);
        IncorrectOrange.SetActive(false);

        //Para definir las respuestas correctas
        //para que no se mueva mas
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        for (int i = 0; i < Objects.Length; i++)
        {
            //Para calcular la distancia entre el objeto y el selector
            float distance = Vector3.Distance(Selector.transform.position, Objects[i].transform.position);
            //Debug.Log("Objeto" + Objects[i] + "Distance: " + distance);

            //Para calcular la distancia más pequeña
            if (i == 0)
            {
                distanceMin = distance;
            }
            else
            {
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    //Para saber el nombre del objeto 
                    answerControlCol.AnswerCorrect = Objects[i].name;
                }
            }
        }
        Debug.Log("DistanceMin: " + distanceMin);
        // }
    }
}
