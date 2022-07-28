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
        isPressing = false;
        //Para que se reproduzca el audio del inicio (las isntrucciones)
        AudioClip[] audios = new AudioClip[1] { sounds[5] };
        StartCoroutine(audioSource.PlayAudio(audios));
        sceneNewSha();
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
    public void sceneNewSha()
    {
        n = Random.Range(0, 4);
        AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
        StartCoroutine(audioSource.PlayAudio(soundsToPlay));

        //Para mostrar las instrucciones
        TextsSha[n].SetActive(true);//Para que se activen

        //Para que se desactiven los círculos rojos
        IncorrectCircle.SetActive(false);
        IncorrectTriangle.SetActive(false);
        IncorrectSquare.SetActive(false);
        IncorrectRectangle.SetActive(false);
        IncorrectStar.SetActive(false);

        //Para el movimiento del mouse
        //rate = 1;
        // Vector2 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // gameObject.transform.position = pz / rate;

        //Para definir las respuestas correctas
        //para que no se mueva mas
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        for (int i = 0; i < Shapes.Length; i++)
        {
            //Para calcular la distancia entre el objeto y el selector
            float distance = Vector3.Distance(Selector.transform.position, Shapes[i].transform.position);
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
                    answerControlSha.AnswerCorrect = Shapes[i].name;
                }
            }
        }
        Debug.Log("DistanceMin: " + distanceMin);
        // }
    }
}
