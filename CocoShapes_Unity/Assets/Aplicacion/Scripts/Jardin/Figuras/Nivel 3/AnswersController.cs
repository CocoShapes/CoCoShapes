using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersController : MonoBehaviour
{
    //Para llamar al código denominado ShowText para los audios.
    private ShowText showText;
    public GameObject[] Shapes;//Figuras (imágenes)

    //Para los audios
    public Audio2 audioSource;

    //Para la animación de las figuras
    public Transform target;
    public float speed;

    //Para la respuestas
    public string AnswerChild;//la que el usuario presionó realmente
    public string AnswerCorrect;//la que el usuario debería presionar
    private int AnswerCorrects = 0;//para contar las respuestas correctas
    private int AnswerIncorrects = 0;//para contar las respuestas incorrectas

    public bool isPressing;//para presionar las teclas

    //public GameObject[] IncorrectsCircles;//Las instrucciones (son imágenes)

    void Start()
    {
        //En un inicio no se presiona ninguna tecla
        isPressing = false;
        //Para poder usar lo del código ShowText para los audios
        showText = GameObject.Find("GameObject-Script").GetComponent<ShowText>();
    }

    void Update()
    {
        //Para presionar las teclas y obtener la respuesta AnswerChild
        if (Input.GetKey(KeyCode.C))
        {
            AnswerChild = "Circle";
            isPressing = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            AnswerChild = "Rectangle";
            isPressing = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            AnswerChild = "Square";
            isPressing = true;
        }
        if (Input.GetKey(KeyCode.T))
        {
            AnswerChild = "Triangle";
            isPressing = true;
        }
        //Para star no pude repetir s por eso se usó la tecla w
        if (Input.GetKey(KeyCode.W))
        {
            AnswerChild = "Star";
            isPressing = true;
        }

        //Se está presionando una tecla
        if (isPressing)
        {
            //Para que se desactiven las figuras
            foreach (GameObject shapes in Shapes)
            {
                shapes.SetActive(false);
            }
            //Se presionó una tecla y ahora se comparan si la respuesta es correcta
            //(se compara answerCorrect con answerChild)
            if (AnswerChild == AnswerCorrect)
            {
                if (AnswerChild == "Circle")
                {
                    Shapes[0].SetActive(true);//Para que se active la figura
                    //Para la animación de la figura
                    float step = speed * Time.deltaTime;
                    Shapes[0].transform.position = Vector3.MoveTowards(Shapes[0].transform.position, target.position, step);
                }
                if (AnswerChild == "Rectangle")
                {
                    Shapes[1].SetActive(true);
                    float step = speed * Time.deltaTime;
                    Shapes[1].transform.position = Vector3.MoveTowards(Shapes[1].transform.position, target.position, step);
                }
                if (AnswerChild == "Square")
                {
                    Shapes[2].SetActive(true);
                    float step = speed * Time.deltaTime;
                    Shapes[2].transform.position = Vector3.MoveTowards(Shapes[2].transform.position, target.position, step);
                }
                if (AnswerChild == "Triangle")
                {
                    Shapes[3].SetActive(true);
                    float step = speed * Time.deltaTime;
                    Shapes[3].transform.position = Vector3.MoveTowards(Shapes[3].transform.position, target.position, step);
                }
                if (AnswerChild == "Star")
                {
                    Shapes[4].SetActive(true);
                    float step = speed * Time.deltaTime;
                    Shapes[4].transform.position = Vector3.MoveTowards(Shapes[4].transform.position, target.position, step);
                }
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { showText.sounds[5], showText.sounds[6] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;

            }
            //Si las dos NO son iguales
            if (AnswerChild != AnswerCorrect)
            {
                //OJO: FALTA Para que se desactiven los círculos rojos 
                // foreach (GameObject incorrects in IncorrectsCircles)
                // {
                //     incorrects.SetActive(false);
                // }
                // if (AnswerCorrect == "Circle")
                // {
                //     IncorrectsCircles[0].SetActive(true);
                // }
                // if (AnswerCorrect == "Rectangle")
                // {
                //     IncorrectsCircles[1].SetActive(true);
                // }
                // if (AnswerCorrect == "Square")
                // {
                //     IncorrectsCircles[2].SetActive(true);
                // }
                // if (AnswerCorrect == "Triangle")
                // {
                //     IncorrectsCircles[3].SetActive(true);
                // }
                // if (AnswerCorrect == "Star")
                // {
                //     IncorrectsCircles[4].SetActive(true);
                // }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { showText.sounds[7], showText.sounds[8] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
            }
        }
        //Para que cuando ya se hayan realizado las 5 o se hayan respondido 3 incorrectas
        if (AnswerCorrects >= 5 || AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
        }
    }
}
