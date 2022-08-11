using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCar : MonoBehaviour
{
    //Para llamar al código denominado RollerCoster para los audios.
    private RollerCoster rollerCoster;

    //Para las animaciones
    public Animator animator;

    //Para las respuestas
    public string AnswerChild;//la que el usuario presionó realmente
    public string AnswerCorrect;//la que el usuario debería presionar
    private int AnswerCorrects = 0;//Para contar las respuestas correctas
    private int AnswerIncorrects = 0;//Para contar las respuestas incorrectas

    //Para presionar el botón
    public bool isPressing;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[8];
    public AudioControl audioSource;

    //Para mostrar las instrucciones(son imágenes)
    public GameObject[] Instructions;

    //Para los audios de las instrucciones cuando es incorrecto
    int c;


    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Count";
    private int level = 3;
    public GameObject panelGameFinished;
    private float totalGameTime;

    void Start()
    {
        //En un inicio no se está presionando ningún botón
        isPressing = false;
        //Para obtener variables del código denominado RollerCoster para los audios.
        rollerCoster = GameObject.Find("Background").GetComponent<RollerCoster>();

        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }

    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 3)
        {
            StopCoroutine(rollerCoster.Rail());
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(4);
            foreach (GameObject scene in rollerCoster.Scenes)
            {
                scene.SetActive(false);
            }
            //Para que se desactiven las instrucciones
            foreach (GameObject instruction in Instructions)
            {
                instruction.SetActive(false);
            }
            //Para que se elimine el riel que ya salió
            rollerCoster.RemoveScene(rollerCoster.n);//Se elimina el riel de la lista de rieles
            StartCoroutine(rollerCoster.Rail());
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
                if (rollerCoster.GameObjectName == "Rail (4)")
                {
                    //Se ejecuta la animación
                    animator.Play("Rail4");
                }
                if (rollerCoster.GameObjectName == "Rail (7)")
                {
                    animator.Play("Rail7");
                }
                if (rollerCoster.GameObjectName == "Rail (10)")
                {
                    animator.Play("Rail10");
                }
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                //Se reproduce el sonido de correcto y el audio de fantastic
                AudioClip[] audios = new AudioClip[2] { rollerCoster.sounds[4], rollerCoster.sounds[5] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
                //Corrutina para que se finalize el audio de fantastic antes mostrar otra instrucción
                StartCoroutine(WaitForAudio());

            }
            if (AnswerChild != AnswerCorrect)
            {
                //Para poner de color rojo la instrucción
                if (rollerCoster.GameObjectName == "Rail (4)")
                {
                    //Se ejecuta la animación
                    animator.Play("Rail4IncorrectJ");
                    Instructions[0].GetComponent<SpriteRenderer>().color = Color.red;
                    sounds[c] = sounds[0];
                }
                if (rollerCoster.GameObjectName == "Rail (7)")
                {
                    //Se ejecuta la animación
                    animator.Play("Rail7IncorrectJ");
                    Instructions[1].GetComponent<SpriteRenderer>().color = Color.red;
                    sounds[c] = sounds[1];
                }
                if (rollerCoster.GameObjectName == "Rail (10)")
                {
                    //Se ejecuta la animación
                    animator.Play("Rail10IncorrectJ");
                    Instructions[2].GetComponent<SpriteRenderer>().color = Color.red;
                    sounds[c] = sounds[2];
                }
                //Se suma una respuesta incorrecta
                AnswerIncorrects++;
                Debug.Log("Incorrect");
                //Se reproduce el sonido de incorrecto y el audio de upsis
                if (AnswerIncorrects < 3)
                {
                    AudioClip[] audios = new AudioClip[3] { sounds[6], sounds[7], sounds[c] };
                    StartCoroutine(audioSource.PlayAudio(audios));
                }
                if (AnswerIncorrects == 3)
                {
                    AudioClip[] audios = new AudioClip[2] { sounds[6], sounds[7] };
                    StartCoroutine(audioSource.PlayAudio(audios));
                }

                //Ya no se está presionando un botón se sigue con otra
                isPressing = false;
            }
        }
        //Para cuando se hayan respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            StopCoroutine(rollerCoster.Rail());
            Debug.Log("Game Over");
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, level, AnswerCorrects, AnswerIncorrects, (int)totalGameTime));
            panelGameFinished.SetActive(true);
        }
    }
}
