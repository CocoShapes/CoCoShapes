using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCoster : MonoBehaviour
{
    //Para llamar al código denominado AnimationCar para obtener AnswerCorrect.
    public AnimationCar animationCar;

    //Para mostrar los rieles (son imágenes) 
    public GameObject[] Scenes;//Scenes son los rieles: Rail1...

    //Para mostrar las instrucciones(son imágenes)
    public GameObject[] Instructions;

    //Para los audios
    public AudioClip[] sounds = new AudioClip[8];
    public AudioControl audioSource;

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    //Para que se muestren aleatoriamente los rieles
    public int n;

    //Para que se reacomode el carro al inicio
    public GameObject car;

    //Para conocer el nombre del riel que se está mostrando
    public String GameObjectName;

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

        AudioClip[] audios = new AudioClip[1] { sounds[3] };
        StartCoroutine(audioSource.PlayAudio(audios));

        while (recoTime < sounds[3].length)
        {
            recoTime += Time.deltaTime;
            yield return null;
        }

        //Para que inicie la corrutina que muestra todo (Los textos,etc)
        StartCoroutine(Rail());
    }

    //Para que no se repitan las instrucciones (Texts) se eliminan
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    public void RemoveScene(int index)
    {
        RemoveAt(ref Scenes, index);
    }

    public IEnumerator Rail()
    {
        animationCar.isPressing = false;
        //Para reacomodar el carro al inicio
        car.GetComponent<Animator>().Play("Stop");

        yield return new WaitForSeconds(1);

        //Para que los rieles se muestren aleatoriamente
        n = UnityEngine.Random.Range(0, Scenes.Length);

        Scenes[n].SetActive(true);//Para que se activen los rieles

        //Para los audios de las instrucciones
        AudioClip[] soundsToPlay = new AudioClip[1];

        //Para definir las respuestas correctas según el riel que aparezca
        GameObjectName = Scenes[n].name;//Para obtener el nombre de la escena
        if (GameObjectName == "Rail (4)")
        {
            Instructions[0].SetActive(true);
            soundsToPlay[0] = sounds[0];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            animationCar.AnswerCorrect = "Popcorn";
        }
        if (GameObjectName == "Rail (7)")
        {
            Instructions[1].SetActive(true);
            soundsToPlay[0] = sounds[1];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            animationCar.AnswerCorrect = "Popcorn";
        }
        if (GameObjectName == "Rail (10)")
        {
            Instructions[2].SetActive(true);
            soundsToPlay[0] = sounds[2];
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));
            animationCar.AnswerCorrect = "Icecream";
        }
        yield return null;
    }
}


