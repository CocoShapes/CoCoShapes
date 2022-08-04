using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCouT : MonoBehaviour
{
    //Para poder utilizar la repuesta correcta que está en el script SceneControllerCout
    private SceneControllerCouT sceneControllerCouT;

    //Para las respuestas
    public string AnswerChild;//la que el usuario presionó realmente
    public string AnswerCorrect;//la que el usuario debería presionar
    private int AnswerCorrects = 0;//Para contar las respuestas correctas
    private int AnswerIncorrects = 0;//Para contar las respuestas incorrectas

    //Para presionar el botón
    public bool isPressing;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[8];
    public SoundControl2 audioSource;

    //Para las respuestas incorrectas
    public GameObject[] IncorrectsCircles;//Las instrucciones (son imágenes)

    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Count";
    private int level = 3;

    public GameObject panelGameFinished;
    private float totalGameTime;

    void Start()
    {
        // Para obtener variables del código denominado SceneControllerCouT para los audios.
        sceneControllerCouT = GameObject.Find("SceneControllerCouT").GetComponent<SceneControllerCouT>();

        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }
    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 3)
        {
            sceneControllerCouT.StopCoroutine(sceneControllerCouT.Screen1());
            Debug.Log("Game Over");
            //Desactivo botones
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);

        }
        else
        {
            yield return new WaitForSeconds(2);
            //Para que se desactiven los círculos rojos 
            foreach (GameObject incorrects in IncorrectsCircles)
            {
                incorrects.SetActive(false);
            }
            //Para que se desactiven las terceras pantallas
            foreach (GameObject screen in sceneControllerCouT.Screens3)
            {
                screen.SetActive(false);
            }
            //Para que se elimine la escena que ya salió
            sceneControllerCouT.RemoveText(sceneControllerCouT.n);
            //Para que empiece otra vez la corrutina que muestra todo
            sceneControllerCouT.StartCoroutine(sceneControllerCouT.Screen1());
        }

    }
    void Update()
    {
        totalGameTime += Time.deltaTime;

        //Se presionó un botón y ahora se compara si la respuesta es correcta
        //(se compara answerCorrect con answerChild)
        if (isPressing)
        {
            //Si las dos son iguales
            if (AnswerChild == AnswerCorrect)
            {
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
                //Animación de celebrar
                sceneControllerCouT.animator.Play("Celebrando");
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { sceneControllerCouT.sounds[4], sceneControllerCouT.sounds[5] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Debug.Log("AUDIO");
                StartCoroutine(WaitForAudio());
            }
            else
            {
                //Para poner de color rojo la instrucción
                if (AnswerCorrect == "2")
                {
                    IncorrectsCircles[0].SetActive(true);
                }
                if (AnswerCorrect == "6")
                {
                    IncorrectsCircles[1].SetActive(true);
                }
                if (AnswerCorrect == "9")
                {
                    IncorrectsCircles[2].SetActive(true);
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { sceneControllerCouT.sounds[6], sceneControllerCouT.sounds[7] };
                StartCoroutine(audioSource.PlayAudio(audios));
                sceneControllerCouT.animator.Play("Triste");
            }
        }
        //Para cuando se hayan respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
    }
}
