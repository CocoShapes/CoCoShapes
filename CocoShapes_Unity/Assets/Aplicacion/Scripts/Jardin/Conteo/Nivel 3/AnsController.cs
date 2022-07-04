using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsController : MonoBehaviour
{
    public string AnswerChild;//la que el usuario presionó realmente

    public string AnswerCorrect;//la que el usuario deberia presionar

    public Button ButtonIceCream;

    public Button ButtonPopcorn;

    private int AnswerCorrects = 0;

    private int AnswerIncorrects = 0;

    public bool isPressing;

    // public Transform stopRail1;
    // public Transform stopRail2;
    // public Transform stopRail3;
    // public Transform stopRail4;
    // public Transform stopRail5;
    // public Transform stopRail6;
    // public Transform stopRail7;
    // public Transform stopRail8;
    // public Transform stopRail9;
    // public Transform stopRail10;

    public float speed;

    private Vector3 InitPos = new Vector3(-6.29f, -0.00999999f, -9.39f);

    private Vector3 IntPos = new Vector3(-2.18f, -0.00999999f, -9.39f);
    private Vector3 FinalPos = new Vector3(-0.08f, 0.42f, -9.39f);

    public GameObject Car;

    int n;

    //Para lo de los sonidos
    public AudioClip[] sounds = new AudioClip[12];
    public AudioControl audioSource;

    void Start()
    {
        isPressing = false;
    }


    void Update()
    {
        //Para los botones de las respuestas
        if (ButtonIceCream)
        {
            ButtonIceCream.GetComponent<Button>().onClick.AddListener(() =>
            {
                AnswerChild = "IceCream";
                isPressing = true;
            });
        }
        if (ButtonPopcorn)
        {
            ButtonPopcorn.GetComponent<Button>().onClick.AddListener(() =>
            {
                AnswerChild = "Popcorn";
                isPressing = true;
            });
        }

        //Para comparar las respuestas
        if (isPressing)
        {
            if (AnswerChild == AnswerCorrect)
            {
                if (n == 0)
                {
                    float step = speed * Time.deltaTime;
                    Car.transform.position = IntPos;
                    Car.transform.rotation = Quaternion.Euler(0f, 0f, 13.315f);
                    Car.transform.position = FinalPos;

                }
                if (n == 1)
                {



                }
                if (n == 2)
                {


                }
                if (n == 3)
                {

                }
                if (n == 4)
                {


                }
                if (n == 5)
                {


                }
                if (n == 6)
                {


                }
                if (n == 7)
                {


                }
                if (n == 8)
                {


                }
                if (n == 9)
                {


                }

                AnswerCorrects++;
                Debug.Log("Correct");


            }
            if (AnswerChild != AnswerCorrect)
            {

                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //FALTA el audio Keep trying
                StartCoroutine(audioSource.PlayAudio(sounds[11]));
            }

        }
    }


}
