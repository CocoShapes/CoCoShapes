using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersController : MonoBehaviour
{
    //Para llamar al código denominado ShowText para los audios.
    private ShowText showText;

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

    //Para los círculos rojos
    public GameObject[] IncorrectsCircles;

    //Para los audios de las instrucciones cuando es incorrecto
    int c;

    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Shapes";
    private int level = 3;

    public GameObject panelGameFinished;
    private float totalGameTime;

    void Start()
    {
        //En un inicio no se presiona ninguna tecla
        isPressing = false;
        //Para poder usar lo del código ShowText para los audios
        showText = GameObject.Find("GameObject-Script").GetComponent<ShowText>();

        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }

    //Corrutina para que se terminen de reproducir los sonidos de correcto y nice job
    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 5)
        {
            //Para que pare la animación "Celebrando"
            showText.animator.Play("QuietoShaJ");
            showText.StopCoroutine(showText.Show());
            Debug.Log("Game Over");

            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(4);
            showText.animator.Play("QuietoShaJ");
            //Para que se desactiven los textos
            foreach (GameObject text in showText.Texts)
            {
                text.SetActive(false);
            }
            //Para que se desactiven los círculos rojos
            foreach (GameObject incorrects in IncorrectsCircles)
            {
                incorrects.SetActive(false);
            }
            foreach (GameObject shape in showText.Shapes)
            {
                shape.SetActive(false);
            }
            //Para que se elimine el text que ya salió
            showText.RemoveText(showText.n);//Se elimina el texto de la lista de textos
            //Para que empiece otra vez la corrutina que muestra todo
            showText.StartCoroutine(showText.Show());
        }
    }

    void Update()
    {
        totalGameTime += Time.deltaTime;

        //Para presionar las teclas y obtener la respuesta AnswerChild
        //Circle (DownArrow) o C
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AnswerChild = "Circle";
            isPressing = true;
        }
        //Rectangle (Backspace) o R
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            AnswerChild = "Rectangle";
            isPressing = true;
        }
        //Square (RightArrow) o S
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AnswerChild = "Square";
            isPressing = true;
        }
        //Triangle (LeftArrow) o T
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnswerChild = "Triangle";
            isPressing = true;
        }
        //Para star no pude repetir s por eso se usó la tecla w
        //Star (Tab) o W
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AnswerChild = "Star";
            isPressing = true;
        }

        //Se está presionando una tecla
        if (isPressing)
        {
            //Se presionó una tecla y ahora se comparan si la respuesta es correcta
            //(se compara answerCorrect con answerChild)
            if (AnswerChild == AnswerCorrect)
            {
                //Se ejecuta la animación
                showText.animator.Play("SacandoFigura");
                if (AnswerChild == "Circle")
                {
                    showText.Shapes[0].SetActive(true);//Para que se active la figura
                    //Para la animación de la figura
                    float step = speed;
                    showText.Shapes[0].transform.position = Vector3.MoveTowards(showText.Shapes[0].transform.position, target.position, step);
                }
                if (AnswerChild == "Rectangle")
                {
                    showText.Shapes[1].SetActive(true);
                    float step = speed;
                    showText.Shapes[1].transform.position = Vector3.MoveTowards(showText.Shapes[1].transform.position, target.position, step);
                }
                if (AnswerChild == "Square")
                {
                    showText.Shapes[2].SetActive(true);
                    float step = speed;
                    showText.Shapes[2].transform.position = Vector3.MoveTowards(showText.Shapes[2].transform.position, target.position, step);
                }
                if (AnswerChild == "Triangle")
                {
                    showText.Shapes[3].SetActive(true);
                    float step = speed;
                    showText.Shapes[3].transform.position = Vector3.MoveTowards(showText.Shapes[3].transform.position, target.position, step);
                }
                if (AnswerChild == "Star")
                {
                    showText.Shapes[4].SetActive(true);
                    float step = speed;
                    showText.Shapes[4].transform.position = Vector3.MoveTowards(showText.Shapes[4].transform.position, target.position, step);
                }
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                isPressing = false;
                //Se reproduce el sonido de sacarFigura, correcto y el audio de Fantastic
                AudioClip[] audios = new AudioClip[3] { showText.sounds[10], showText.sounds[6], showText.sounds[7] };
                StartCoroutine(audioSource.PlayAudio(audios));

                //Corrutina para que se finalice el audio de Fantastic antes mostrar otra instrucción
                StartCoroutine(WaitForAudio());
            }
            //Si las dos NO son iguales
            else
            {
                if (AnswerCorrect == "Circle")
                {
                    IncorrectsCircles[0].SetActive(true);
                    showText.sounds[c] = showText.sounds[0];
                }
                if (AnswerCorrect == "Rectangle")
                {
                    IncorrectsCircles[1].SetActive(true);
                    showText.sounds[c] = showText.sounds[1];
                }
                if (AnswerCorrect == "Square")
                {
                    IncorrectsCircles[2].SetActive(true);
                    showText.sounds[c] = showText.sounds[2];
                }
                if (AnswerCorrect == "Triangle")
                {
                    IncorrectsCircles[3].SetActive(true);
                    showText.sounds[c] = showText.sounds[3];
                }
                if (AnswerCorrect == "Star")
                {
                    IncorrectsCircles[4].SetActive(true);
                    showText.sounds[c] = showText.sounds[4];
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                isPressing = false;
                //Se reproduce el sonido de incorrecto y el audio de Upsis
                if (AnswerIncorrects < 3)
                {
                    AudioClip[] audios = new AudioClip[3] { showText.sounds[8], showText.sounds[9], showText.sounds[c] };
                    StartCoroutine(audioSource.PlayAudio(audios));
                }
                if (AnswerIncorrects == 3)
                {
                    AudioClip[] audios = new AudioClip[2] { showText.sounds[8], showText.sounds[9] };
                    StartCoroutine(audioSource.PlayAudio(audios));
                }

                //Se reproduce la animación de triste
                showText.animator.Play("TristeShaJ");
            }

        }
        //Para cuando responde 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            showText.animator.Play("QuietoShaJ");
            showText.StopCoroutine(showText.Show());
            Debug.Log("Game Over");

            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);

        }
    }
}
