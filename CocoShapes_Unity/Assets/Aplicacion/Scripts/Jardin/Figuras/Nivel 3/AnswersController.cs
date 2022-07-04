using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersController : MonoBehaviour
{
    public GameObject[] Shapes;

    //Para lo de los sonidos
    public AudioClip[] sounds = new AudioClip[6];
    public Audio2 audioSource;

    public Transform target;
    public float speed;


    public string AnswerChild;//la que el usuario presionó realmente

    public string AnswerCorrect;//la que el usuario deberia presionar

    private int AnswerCorrects = 0;

    private int AnswerIncorrects = 0;

    public bool isPressing;

    void Start()
    {
        isPressing = false;
    }

    void Update()
    {
        //Para las animaciones de las figuras
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

        if (isPressing)
        {
            foreach (GameObject shapes in Shapes)
            {
                shapes.SetActive(false);
            }
            if (AnswerChild == AnswerCorrect)
            {
                if (AnswerChild == "Circle")
                {
                    Shapes[0].SetActive(true);

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

                AnswerCorrects++;
                Debug.Log("Correct");
                //FALTA el audio NICE JOB
                //StartCoroutine(audioSource.PlayAudio(sounds[5]));

            }
            if (AnswerChild != AnswerCorrect)
            {

                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //FALTA el audio Keep trying
                //StartCoroutine(audioSource.PlayAudio(sounds[6]));
            }
        }
    }
}
