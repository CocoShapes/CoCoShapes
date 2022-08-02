using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    //Para llamar al código denominado AnswerController para poder usar el answerCorrect en ese código.
    public AnswerController answerController;

    //Para el array de los objetos vacíos que ayudan a obtener la distancia de cada color.
    public GameObject[] ObjectColors;

    //Para la rotación
    public float angle;
    private float speed;
    private float rotationTime;
    private float recorredTime;
    public GameObject Wheel2;//La ruleta 2 

    //Variable selector
    public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    float distanceMin;

    //Para las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar

    //Para los audios
    public AudioClip[] sounds = new AudioClip[13];
    public AudioControl1 audioSource;

    // //Para guardar los colores que ya aparecieron
    // public List<string> colors = new List<string>();

    public GameObject RedIncorrect;
    public GameObject GreenIncorrect;
    public GameObject YellowIncorrect;
    public GameObject BlackIncorrect;
    public GameObject OrangeIncorrect;
    public GameObject BlueIncorrect;
    public GameObject PurpleIncorrect;
    public GameObject WhiteIncorrect;

    public GameObject Wheel;//Ruleta 1

    //Para las animaciones
    public Animator animator;

    void Start()
    {
        //Para que se reproduzca el audio del inicio (la instrucción)
        AudioClip[] audios = new AudioClip[1] { sounds[8] };
        StartCoroutine(audioSource.PlayAudio(audios));
    }

    //Método para la rotación de la ruleta.
    public IEnumerator Rotate()
    {
        //Para que aparezca la animación de Coco girando la ruleta
        animator.Play("GiraRuleta");
        //Para que se active Wheel
        Wheel.SetActive(true);
        //Para que se desactive Wheel2
        Wheel2.SetActive(false);
        //Para que se desactiven los círculos rojos
        RedIncorrect.SetActive(false);
        GreenIncorrect.SetActive(false);
        YellowIncorrect.SetActive(false);
        BlackIncorrect.SetActive(false);
        OrangeIncorrect.SetActive(false);
        BlueIncorrect.SetActive(false);
        PurpleIncorrect.SetActive(false);
        WhiteIncorrect.SetActive(false);

        //El while se ejecuta mientras el tiempo que debe girarse la ruleta no halla terminado
        while (recorredTime < rotationTime)
        {
            transform.Rotate(0, 0, -speed * Time.deltaTime);
            recorredTime += Time.deltaTime;
            yield return null;
        }
        //Esto es lo que pasa despues de que la ruleta se detiene
        //Debug.Log("After rotation, Time: " + recorredTime);

        //Para que la Ruleta 2 tome el mismo angulo de la ruleta que gira (ruleta 1)
        Wheel2.SetActive(true);
        Wheel2.transform.rotation = transform.rotation;


        //PARA SABER QUE COLOR ESTÁ MÁS CERCA DEL SELECTOR
        for (int i = 0; i < ObjectColors.Length; i++)
        {
            //Para calcular la distancia entre el color y el selector
            float distance = Vector3.Distance(Selector.transform.position, ObjectColors[i].transform.position);
            //Debug.Log("Color" + ObjectColors[i] + "Distance: " + distance);

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
                    //Para definir las respuestas correctas
                    if (i == 0)
                    {
                        answerController.AnswerCorrect = "Red";
                    }
                    else if (i == 1)
                    {
                        answerController.AnswerCorrect = "Green";
                    }
                    else if (i == 2)
                    {
                        answerController.AnswerCorrect = "Yellow";
                    }
                    else if (i == 3)
                    {
                        answerController.AnswerCorrect = "Black";
                    }
                    else if (i == 4)
                    {
                        answerController.AnswerCorrect = "Orange";
                    }
                    else if (i == 5)
                    {
                        answerController.AnswerCorrect = "Blue";
                    }
                    else if (i == 6)
                    {
                        answerController.AnswerCorrect = "Purple";
                    }
                    else if (i == 7)
                    {
                        answerController.AnswerCorrect = "White";
                    }
                    //Para saber el nombre del color que está más cerca del selector
                    //answerController.AnswerCorrect = ObjectColors[i].name;
                    //Para que se reproduzcan los audios de los colores
                    AudioClip[] soundsToPlay = new AudioClip[1] { sounds[i] };
                    StartCoroutine(audioSource.PlayAudio(soundsToPlay));
                }
            }


        }
        Debug.Log("DistanceMin: " + distanceMin);


        // //Para que no se repitan los colores que ya aparecieron (FALTA)
        // if (colors.Count < ObjectColors.Length)
        // {
        //     //Para que no se repitan los colores que ya aparecieron
        //     colors.Add(answerController.AnswerCorrect);
        // }

        //Para desactivar la ruleta que gira
        this.gameObject.SetActive(false);
        yield return null;
    }

    //Para la rotación de la ruleta.
    void OnEnable()
    {
        speed = Random.Range(250f, 280f);
        angle = Random.Range(0f, 360f);

        rotationTime = Random.Range(6f, 7f);
        recorredTime = 0f;

        transform.Rotate(0, 0, angle);

        StartCoroutine(Rotate());
    }
}


















