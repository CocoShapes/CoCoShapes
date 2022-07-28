using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerCouT : MonoBehaviour
{
    //Opciones 1 (es decir las primeras pantallas que deben aparecer)
    public GameObject[] Screens1;
    //Opciones 2 (es decir las segundas pantallas que deben aparecer) 
    public GameObject[] Screens2;
    //Opciones 3 (es decir las terceras pantallas que deben aparecer)
    public GameObject[] Screens3;

    //Para que aparezcan aleatoriamente
    public int n;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[8];

    public SoundControl2 audioSource;

    //Para el tiempo
    float tiempoShow1;
    float tiempoRecorrido1 = 0;

    float tiempoShow2;
    float tiempoRecorrido2 = 0;

    float tiempoShow3;
    float tiempoRecorrido3 = 0;

    //Para las respuestas correctas
    public string AnswerCorrect;//la que el usuario debería presionar

    //Para poder utilizar la repuesta correcta en el script AnswerCouT
    public AnswerCouT answerCouT;

    //Para desactivar los círculos rojos
    public GameObject[] IncorrectsCircles;

    public Animator animator;

    public GameObject Character;

    void Start()
    {
        //Audio de la instrucción del inicio
        AudioClip[] audios = new AudioClip[1] { sounds[3] };
        StartCoroutine(audioSource.PlayAudio(audios));
        //Debug.Log("AUDIO");

        //Para la animación
        animator = Character.gameObject.GetComponent<Animator>();

        //Para que aparezcan las escenas sin presionar la barra espaciadora
        SceneNew();

        //Para que no se repitan las escenas

    }
    public void SceneNew()
    {
        //Para que aparezcan las primeras opciones
        //Para que se desactiven los círculos rojos 
        foreach (GameObject incorrects in IncorrectsCircles)
        {
            incorrects.SetActive(false);
        }
        //Para desactivar la pantalla que se está mostrando
        Screens3[n].SetActive(false);
        //Para activar la pantalla 1
        StartCoroutine(Screen1());
        Debug.Log("Aparece la primera pantalla");
    }

    //OPCIONES 1
    public IEnumerator Screen1()
    {
        foreach (GameObject incorrects in IncorrectsCircles)
        {
            incorrects.SetActive(false);
        }
        //Para desactivar la pantalla que se está mostrando
        Screens3[n].SetActive(false);
        while (tiempoRecorrido1 < tiempoShow1)
        {
            tiempoRecorrido1 += Time.deltaTime;
            yield return null;
        }
        foreach (GameObject screen1 in Screens1)
        {
            screen1.SetActive(false);
        }
        n = Random.Range(0, Screens1.Length);
        Screens1[n].SetActive(true);
        //Para que se reproduzcan los audios de los colores
        AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
        StartCoroutine(audioSource.PlayAudio(soundsToPlay));
        tiempoRecorrido1 = 0;
        yield return new WaitForSeconds(3.1f);
        StartCoroutine(Screen2());

    }
    //OPCIONES 2
    public IEnumerator Screen2()
    {
        //Para desactivar la pantalla que se está mostrando
        Screens1[n].SetActive(false);
        animator.Play("Saludando");
        while (tiempoRecorrido2 < tiempoShow2)
        {
            tiempoRecorrido2 += Time.deltaTime;
            yield return null;
        }
        foreach (GameObject screen2 in Screens2)
        {
            screen2.SetActive(false);
        }
        if (n == 0)
        {
            Screens2[0].SetActive(true);
        }
        if (n == 1)
        {
            Screens2[1].SetActive(true);
        }
        if (n == 2)
        {
            Screens2[2].SetActive(true);
        }
        tiempoRecorrido2 = 0;
        Debug.Log("Se cambió a la segunda pantalla");
        yield return new WaitForSeconds(3.1f);
        StartCoroutine(Screen3());
    }
    //OPCIONES 3
    public IEnumerator Screen3()
    {
        //Para desactivar la pantalla que se está mostrando
        Screens2[n].SetActive(false);
        while (tiempoRecorrido3 < tiempoShow3)
        {
            tiempoRecorrido3 += Time.deltaTime;
            yield return null;
        }
        foreach (GameObject screen3 in Screens3)
        {
            screen3.SetActive(false);
        }
        if (n == 0)
        {
            Screens3[0].SetActive(true);
        }
        if (n == 1)
        {
            Screens3[1].SetActive(true);
        }
        if (n == 2)
        {
            Screens3[2].SetActive(true);
        }
        tiempoRecorrido3 = 0;
        Debug.Log("Se cambió a la tercera pantalla");

        //Para definir las respuestas correctas
        if (n == 0)
        {
            answerCouT.AnswerCorrect = "2";
        }
        if (n == 1)
        {
            answerCouT.AnswerCorrect = "6";
        }
        if (n == 2)
        {
            answerCouT.AnswerCorrect = "9";
        }
        yield return new WaitForSeconds(3.1f);
    }
}




