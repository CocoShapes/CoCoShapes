using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerController : MonoBehaviour
{
    //Para llamar al código denominado SpinWheel para los audios.
    private SpinWheel spinWheel;

    //Para los círculos rojos cuando es incorrecta la respuesta.
    public GameObject RedIncorrect;
    public GameObject GreenIncorrect;
    public GameObject YellowIncorrect;
    public GameObject BlackIncorrect;
    public GameObject OrangeIncorrect;
    public GameObject BlueIncorrect;
    public GameObject PurpleIncorrect;
    public GameObject WhiteIncorrect;

    //Para lo de las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar
    private string AnswerChild;//la que el usuario presionó realmente
    private int AnswerCorrects = 0;//para contar las respuestas correctas
    private int AnswerIncorrects = 0;//para contar las respuestas incorrectas

    //Para los audios
    public AudioControl1 audioSource;

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Colors";
    private int level = 3;

    public GameObject panelGameFinished;
    private float totalGameTime;

    void Start()
    {
        //Al inicio no se ha presionado ninguna tecla
        isPressing = false;
        //Para poder usar lo del código SpinWheel para los audios
        spinWheel = GameObject.Find("Wheel").GetComponent<SpinWheel>();

        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }

    //Corrutina para que se terminen de reproducir los sonidos de correcto y nice job antes de girar la ruleta otra vez.
    IEnumerator WaitForAudio()
    {
        if (AnswerCorrects == 5)
        {
            //Para que pare la animación "Celebrando"
            spinWheel.animator.Play("Stop");
            spinWheel.Wheel2.SetActive(true);
            spinWheel.Wheel.SetActive(false);
            spinWheel.StopCoroutine(spinWheel.Rotate());
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(4);
            //spinWheel.animator.Play("GiraRuleta");
            spinWheel.Wheel2.SetActive(false);
            spinWheel.Wheel.SetActive(true);
            //Para que empiece otra vez la corrutina que muestra todo
            spinWheel.StartCoroutine(spinWheel.Rotate());
        }
    }
    void Update()
    {
        totalGameTime += Time.deltaTime;

        //Para saber que tecla se presonó y así conocer si la respuesta es correcta o incorrecta
        //Red (F3) o R
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AnswerChild = "Red";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Green (F4) O G
        if (Input.GetKeyDown(KeyCode.F4))
        {
            AnswerChild = "Green";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Yellow (F1) o Y
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AnswerChild = "Yellow";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Black (F7) o B
        if (Input.GetKeyDown(KeyCode.F7))
        {
            AnswerChild = "Black";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Orange (F5) o O
        if (Input.GetKeyDown(KeyCode.F5))
        {
            AnswerChild = "Orange";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Para BLUE como se repite por black
        //Blue (F2) o U
        if (Input.GetKeyDown(KeyCode.F2))
        {
            AnswerChild = "Blue";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //Purple (F6) o P
        if (Input.GetKeyDown(KeyCode.F6))
        {
            AnswerChild = "Purple";
            //Debug.Log("AnswerChild: " + AnswerChild);
            isPressing = true;
        }
        //White (F8) o W
        if (Input.GetKeyDown(KeyCode.F8))
        {
            AnswerChild = "White";
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

                //Para que se elimine la respuesta correcta del juego (FALTA)
                //Destroy(GameObject.Find(AnswerCorrect));

                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { spinWheel.sounds[9], spinWheel.sounds[10] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
                //Se reproduce la animación celebrando
                spinWheel.animator.Play("Celebrando");

                //Corrutina para que se finalize el audio de NiceJob antes de girar la ruleta otra vez
                StartCoroutine(WaitForAudio());
            }

            //Si las dos NO son iguales
            if (AnswerChild != AnswerCorrect)
            {
                //Para que aparezcan los círculos rojos
                if (AnswerCorrect == "Red")
                {
                    RedIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "Green")
                {
                    GreenIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "Yellow")
                {
                    YellowIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "Black")
                {
                    BlackIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "Orange")
                {
                    OrangeIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "Blue")
                {
                    BlueIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "Purple")
                {
                    PurpleIncorrect.SetActive(true);
                }
                if (AnswerCorrect == "White")
                {
                    WhiteIncorrect.SetActive(true);
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { spinWheel.sounds[11], spinWheel.sounds[12] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando una tecla se sigue con otra
                isPressing = false;
                //Se reproduce la animación triste
                spinWheel.animator.Play("Triste");
            }
        }
        //Para que cuando ya se hayan realizado las 8 o se hayan respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
    }
}
