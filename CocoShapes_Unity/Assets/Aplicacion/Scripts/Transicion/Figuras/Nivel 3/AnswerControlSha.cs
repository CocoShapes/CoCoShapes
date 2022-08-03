using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerControlSha : MonoBehaviour
{
    public MouseMovementSha mouseMovementSha;
    //Para lo de las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar
    private string AnswerChild;//la que el usuario presionó realmente
    private int AnswerCorrects = 0;//para contar las respuestas correctas
    private int AnswerIncorrects = 0;//para contar las respuestas incorrectas

    //Para los audios
    public SoundControl audioSource;

    public AudioClip[] sounds = new AudioClip[4];

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    //Para los círculos rojos
    public GameObject[] IncorrectsCircles;

    //Para la base de datos
    //Database and Game Finished
    // private DatabaseController database;
    // private string subject = "Shapes";
    // private int level = 3;

    // public GameObject panelGameFinished;
    // private bool gameFinished = false;
    // private float totalGameTime;
    void Start()
    {
        //Para la base de datos:
        //Obtain database gameobject
        //database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }
    //Corrutina para que se terminen de reproducir los sonidos de correcto y nice job antes de girar la ruleta otra vez.
    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 5)
        {
            mouseMovementSha.StopCoroutine(mouseMovementSha.FindShapes());
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            //StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            //panelGameFinished.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(3);
            //Para que se desactiven los textos
            foreach (GameObject textSha in mouseMovementSha.TextsSha)
            {
                textSha.SetActive(false);
            }
            foreach (GameObject incorrects in IncorrectsCircles)
            {
                incorrects.SetActive(false);
            }
            //Para que se elimine el text que ya salió
            mouseMovementSha.RemoveText(mouseMovementSha.n);//Se elimina el texto de la lista de textos
            //Para que empiece otra vez la corrutina que muestra todo
            mouseMovementSha.StartCoroutine(mouseMovementSha.FindShapes());
        }

    }
    void Update()
    {
        //totalGameTime += Time.deltaTime;

        //Para saber que tecla se presonó y así conocer si la respuesta es correcta o incorrecta
        //Balón(circle)
        //Circle (DownArrow) o C
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AnswerChild = "Circle";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Pizza(Triangle)
        //Triangle (LeftArrow) o T
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnswerChild = "Triangle";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Windows(Square)
        //Square (RightArrow) o S
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AnswerChild = "Square";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Chocolate(Rectangle)
        //Rectangle (Backspace) o R
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            AnswerChild = "Rectangle";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Star(star)
        //Star (Tab) o W
        ////Para star no pude repetir s por eso se usó la tecla w
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AnswerChild = "Star";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Se presionó una tecla y ahora se comparan si la respuesta es correcta
        //(se compara answerCorrect con answerChild)
        if (isPressing)
        {
            //Si las dos son iguales
            if (AnswerChild == AnswerCorrect)
            {
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de Fantastic
                AudioClip[] audios = new AudioClip[2] { sounds[0], sounds[1] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
                //Corrutina para que se finalize el audio de Fantastic antes de girar la ruleta otra vez
                StartCoroutine(WaitForAudio());
            }
            //Si las dos NO son iguales
            if (AnswerChild != AnswerCorrect)
            {
                //Para que aparezcan los círculos rojos
                if (AnswerCorrect == "Circle")
                {
                    IncorrectsCircles[0].SetActive(true);
                }
                if (AnswerCorrect == "Triangle")
                {
                    IncorrectsCircles[1].SetActive(true);
                }
                if (AnswerCorrect == "Square")
                {
                    IncorrectsCircles[2].SetActive(true);
                }
                if (AnswerCorrect == "Rectangle")
                {
                    IncorrectsCircles[3].SetActive(true);
                }
                if (AnswerCorrect == "Star")
                {
                    IncorrectsCircles[4].SetActive(true);
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de Upsis
                AudioClip[] audios = new AudioClip[2] { sounds[2], sounds[3] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
            }
        }
        //Para cuando se hayan respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            //StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            //panelGameFinished.SetActive(true);
        }

    }
}
