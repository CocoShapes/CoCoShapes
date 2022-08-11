using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerControlCol : MonoBehaviour
{
    public MouseMovement mouseMovement;
    //Para lo de las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar
    private string AnswerChild;//la que el usuario presionó realmente
    private int AnswerCorrects = 0;//para contar las respuestas correctas
    private int AnswerIncorrects = 0;//para contar las respuestas incorrectas

    //Para los audios
    public SoundController audioSource;

    public AudioClip[] sounds = new AudioClip[22];

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    //Para los círculos rojos
    public GameObject[] IncorrectsCircles;

    int c;

    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Colors";
    private int level = 3;

    public GameObject panelGameFinished;
    private float totalGameTime;

    void Start()
    {
        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }
    //Corrutina para que se terminen de reproducir los sonidos de correcto y nice job antes de girar la ruleta otra vez.
    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 5)
        {
            mouseMovement.StopCoroutine(mouseMovement.FindColors());
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(5);
            //Para que se desactiven los textos
            foreach (GameObject textCol in mouseMovement.TextsCol)
            {
                textCol.SetActive(false);
            }
            foreach (GameObject incorrects in IncorrectsCircles)
            {
                incorrects.SetActive(false);
            }
            //Para que se elimine el text que ya salió
            mouseMovement.RemoveText(mouseMovement.n);//Se elimina el texto de la lista de textos
            //Para que empiece otra vez la corrutina que muestra todo
            mouseMovement.StartCoroutine(mouseMovement.FindColors());
        }

    }
    void Update()
    {
        totalGameTime += Time.deltaTime;

        //Para saber que tecla se presionó y así conocer si la respuesta es correcta o incorrecta
        //Butterfly (yellow)
        //Yellow (F1) o Y
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AnswerChild = "Yellow";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Flower(purple)
        //Purple (F6) o P
        if (Input.GetKeyDown(KeyCode.F6))
        {
            AnswerChild = "Purple";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Star(white)
        //White (F8) o W
        if (Input.GetKeyDown(KeyCode.F8))
        {
            AnswerChild = "White";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Bird(red)
        //Red (F3) o R
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AnswerChild = "Red";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Cat(black)
        //Black (F7) O B
        if (Input.GetKeyDown(KeyCode.F7))
        {
            AnswerChild = "Black";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Green (F4)
        //Fish(green) o G
        if (Input.GetKeyDown(KeyCode.F4))
        {
            AnswerChild = "Green";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Heart(blue)
        //Blue (F2) O U
        //Para BLUE como se repite por black
        if (Input.GetKeyDown(KeyCode.F2))
        {
            AnswerChild = "Blue";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Bear(orange)
        //Orange (F5) o O
        if (Input.GetKeyDown(KeyCode.F5))
        {
            AnswerChild = "Orange";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Se presionó una tecla y ahora se comparan si la respuesta es correcta
        //(se compara answerCorrect con answerChild)
        if (isPressing)
        {
            //Para que se reproduzcan los audios de los colores que el niño presionó
            if (AnswerChild == "Green")
            {
                sounds[c] = sounds[14];
            }
            if (AnswerChild == "Red")
            {
                sounds[c] = sounds[15];
            }
            if (AnswerChild == "White")
            {
                sounds[c] = sounds[16];
            }
            if (AnswerChild == "Purple")
            {
                sounds[c] = sounds[17];
            }
            if (AnswerChild == "Black")
            {
                sounds[c] = sounds[18];
            }
            if (AnswerChild == "Yellow")
            {
                sounds[c] = sounds[19];
            }
            if (AnswerChild == "Blue")
            {
                sounds[c] = sounds[20];
            }
            if (AnswerChild == "Orange")
            {
                sounds[c] = sounds[21];
            }
            //Si las dos son iguales
            if (AnswerChild == AnswerCorrect)
            {
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de Fantastic
                AudioClip[] audios = new AudioClip[3] { sounds[c], sounds[10], sounds[11] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
                //Corrutina para que se finalice el audio de Fantastic antes de mostrar otra instrucción
                StartCoroutine(WaitForAudio());

            }
            //Si las dos NO son iguales
            if (AnswerChild != AnswerCorrect)
            {
                //Para que aparezcan los círculos rojos
                if (AnswerCorrect == "Green")
                {
                    IncorrectsCircles[0].SetActive(true);
                }
                if (AnswerCorrect == "Red")
                {
                    IncorrectsCircles[1].SetActive(true);
                }
                if (AnswerCorrect == "White")
                {
                    IncorrectsCircles[2].SetActive(true);
                }
                if (AnswerCorrect == "Purple")
                {
                    IncorrectsCircles[3].SetActive(true);
                }
                if (AnswerCorrect == "Black")
                {
                    IncorrectsCircles[4].SetActive(true);
                }
                if (AnswerCorrect == "Yellow")
                {
                    IncorrectsCircles[5].SetActive(true);
                }
                if (AnswerCorrect == "Blue")
                {
                    IncorrectsCircles[6].SetActive(true);
                }
                if (AnswerCorrect == "Orange")
                {
                    IncorrectsCircles[7].SetActive(true);
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de Upsis
                if (AnswerIncorrects < 3)
                {
                    AudioClip[] audios = new AudioClip[4] { sounds[c], sounds[12], sounds[13], mouseMovement.soundsToPlay[0] };
                    StartCoroutine(audioSource.PlayAudio(audios));
                }
                if (AnswerIncorrects == 3)
                {
                    AudioClip[] audios = new AudioClip[3] { sounds[c], sounds[12], sounds[13] };
                    StartCoroutine(audioSource.PlayAudio(audios));
                }
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
            }
        }
        //Para cuando se ha respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            mouseMovement.StopCoroutine(mouseMovement.FindColors());
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
    }
}
