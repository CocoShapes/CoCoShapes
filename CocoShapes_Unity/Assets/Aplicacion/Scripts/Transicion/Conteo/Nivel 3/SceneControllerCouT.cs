using System;
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

    public String GameObjectName;

    public String GameObjectName2;

    public String GameObjectName3;

    //GameObjects escenas 2
    public GameObject Screen12;
    public GameObject Screen22;
    public GameObject Screen32;

    //GameObjects escenas 3
    public GameObject Screen13;
    public GameObject Screen23;
    public GameObject Screen33;

    //Para que se reproduzcan los audios de los colores
    public AudioClip[] soundsToPlay = new AudioClip[1];

    void Start()
    {
        //Para iniciar la corrutina con el audio de la instrucción
        StartCoroutine(WaitInstructionCou());
    }

    //Corrutina que reproduce el audio de la instrucción
    public IEnumerator WaitInstructionCou()
    {
        ///Para que se reproduzca el audio del inicio (la instrucción)
        float recoTime = 0f;

        // AudioClip[] audios = new AudioClip[2] { sounds[8], sounds[9] };
        AudioClip[] audios = new AudioClip[1] { sounds[3] };
        StartCoroutine(audioSource.PlayAudio(audios));

        while (recoTime < sounds[3].length)
        {
            recoTime += Time.deltaTime;
            yield return null;
        }

        //Para que inicie la corrutina que muestra todo (Los textos,etc)
        StartCoroutine(Screen1());
    }

    //Para que no se repitan las instrucciones (Texts) se eliminan
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    public void RemoveText(int index)
    {
        RemoveAt(ref Screens1, index);
        RemoveAt(ref Screens2, index);
        RemoveAt(ref Screens3, index);
    }

    //OPCIONES 1
    public IEnumerator Screen1()
    {
        yield return new WaitForSeconds(2);

        while (tiempoRecorrido1 < tiempoShow1)
        {
            tiempoRecorrido1 += Time.deltaTime;
            yield return null;
        }
        n = UnityEngine.Random.Range(0, Screens1.Length);
        Screens1[n].SetActive(true);



        GameObjectName = Screens1[n].name;//Para obtener el nombre de la escena que se está mostrando

        if (GameObjectName == "Screen1.1")
        {
            soundsToPlay[0] = sounds[0];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
        }
        if (GameObjectName == "Screen2.1")
        {
            soundsToPlay[0] = sounds[1];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
        }
        if (GameObjectName == "Screen3.1")
        {
            soundsToPlay[0] = sounds[2];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
        }
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
        //Para que aparezcan las segundas pantallas
        if (GameObjectName == "Screen1.1")
        {
            Screen12.SetActive(true);
        }
        if (GameObjectName == "Screen2.1")
        {
            Screen22.SetActive(true);
        }
        if (GameObjectName == "Screen3.1")
        {
            Screen32.SetActive(true);
        }
        tiempoRecorrido2 = 0;
        //Debug.Log("Se cambió a la segunda pantalla");
        yield return new WaitForSeconds(3.1f);
        StartCoroutine(Screen3());
    }
    //OPCIONES 3
    public IEnumerator Screen3()
    {
        GameObjectName2 = Screens2[n].name;//Para obtener el nombre de la escena que se está mostrando
        //Para desactivar la pantalla que se está mostrando
        Screens2[n].SetActive(false);
        while (tiempoRecorrido3 < tiempoShow3)
        {
            tiempoRecorrido3 += Time.deltaTime;
            yield return null;
        }
        //Para que aparezcan las terceras pantallas
        if (GameObjectName2 == "Screen12")
        {
            Screen13.SetActive(true);
        }
        if (GameObjectName2 == "Screen22")
        {
            Screen23.SetActive(true);
        }
        if (GameObjectName2 == "Screen32")
        {
            Screen33.SetActive(true);
        }
        tiempoRecorrido3 = 0;
        //Debug.Log("Se cambió a la tercera pantalla");

        //Para definir las respuestas correctas según la escena que aparezca
        GameObjectName3 = Screens3[n].name;//Para obtener el nombre de la escena 3 que se está mostrando
        if (GameObjectName3 == "Screen13")
        {
            answerCouT.AnswerCorrect = "2";
        }
        if (GameObjectName3 == "Screen23")
        {
            answerCouT.AnswerCorrect = "6";
        }
        if (GameObjectName3 == "Screen33")
        {
            answerCouT.AnswerCorrect = "9";
        }
        yield return new WaitForSeconds(3.1f);
    }
}




