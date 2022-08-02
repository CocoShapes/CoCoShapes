using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersController : MonoBehaviour
{
    //Para llamar al código denominado ShowText para los audios.
    private ShowText showText;
    //public GameObject[] Shapes;//Figuras (imágenes)

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

    public GameObject[] IncorrectsCircles;//Las instrucciones (son imágenes)


    void Start()
    {
        //En un inicio no se presiona ninguna tecla
        isPressing = false;
        //Para poder usar lo del código ShowText para los audios
        showText = GameObject.Find("GameObject-Script").GetComponent<ShowText>();
    }

    //Corrutina para que se terminen de reproducir los sonidos de correcto y nice job antes de girar la ruleta otra vez.
    IEnumerator WaitForAudio()
    {

        if (AnswerCorrects == 5)
        {
            //Para que pare la animación "Celebrando"
            showText.animator.Play("Stop");
            showText.StopCoroutine(showText.Show());
            Debug.Log("Game Over");
        }
        else
        {
            yield return new WaitForSeconds(3);
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
            //Para que se descativen las shapes después de un tiempo
            //yield return new WaitForSeconds(1);
            foreach (GameObject shape in showText.Shapes)
            {
                shape.SetActive(false);
            }
            showText.StartCoroutine(showText.Show());
        }
    }

    void Update()
    {

        //Para presionar las teclas y obtener la respuesta AnswerChild
        //Circle (DownArrow)
        if (Input.GetKeyDown(KeyCode.C))
        {
            AnswerChild = "Circle";
            isPressing = true;
        }
        //Rectangle (Backspace)
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnswerChild = "Rectangle";
            isPressing = true;
        }
        //Square (RightArrow)
        if (Input.GetKeyDown(KeyCode.S))
        {
            AnswerChild = "Square";
            isPressing = true;
        }
        //Triangle (LeftArrow)
        if (Input.GetKeyDown(KeyCode.T))
        {
            AnswerChild = "Triangle";
            isPressing = true;
        }
        //Para star no pude repetir s por eso se usó la tecla w
        //Star (Tab)
        if (Input.GetKeyDown(KeyCode.W))
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
                //Se reproduce la animación de celebrando

                showText.animator.Play("CelebrandoShaJ");
                //Se suma una respuesta correcta
                AnswerCorrects++;
                Debug.Log("Correct");
                isPressing = false;
                //Se reproduce el sonido de correcto y el audio de NiceJob
                AudioClip[] audios = new AudioClip[2] { showText.sounds[6], showText.sounds[7] };
                StartCoroutine(audioSource.PlayAudio(audios));

                //Corrutina para que se finalize el audio de NiceJob antes mostrar otra instrucción
                StartCoroutine(WaitForAudio());
            }
            //Si las dos NO son iguales
            else
            {
                if (AnswerCorrect == "Circle")
                {
                    IncorrectsCircles[0].SetActive(true);
                }
                if (AnswerCorrect == "Rectangle")
                {
                    IncorrectsCircles[1].SetActive(true);
                }
                if (AnswerCorrect == "Square")
                {
                    IncorrectsCircles[2].SetActive(true);
                }
                if (AnswerCorrect == "Triangle")
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
                isPressing = false;
                //Se reproduce el sonido de incorrecto y el audio de KeepTrying
                AudioClip[] audios = new AudioClip[2] { showText.sounds[8], showText.sounds[9] };
                StartCoroutine(audioSource.PlayAudio(audios));
                //Se reproduce la animación de triste
                showText.animator.Play("TristeShaJ");
            }

        }
        //Para que cuando ya se hayan realizado las 8 o se hayan respondido 3 incorrectas
        if (AnswerIncorrects == 3)
        {
            Debug.Log("Game Over");

        }
    }
}
