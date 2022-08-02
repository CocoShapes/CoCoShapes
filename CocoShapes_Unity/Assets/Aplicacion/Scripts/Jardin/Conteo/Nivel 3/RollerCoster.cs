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

    void Start()
    {
        //Para que se reproduzca el audio del inicio (la instrucción)
        AudioClip[] audios = new AudioClip[1] { sounds[3] };
        StartCoroutine(audioSource.PlayAudio(audios));
    }
    public IEnumerator Rail()
    {
        animationCar.isPressing = false;
        //Para reacomodar el carro al inicio
        car.GetComponent<Animator>().Play("Stop");

        yield return new WaitForSeconds(5);

        //Para que los rieles se muestren aleatoriamente
        n = Random.Range(0, Scenes.Length);
        animationCar.n = n;//Para en animationCar poder usar la n (que me indica que riel se está mostrando)

        Scenes[n].SetActive(true);//Para que se activen los rieles
        Instructions[n].SetActive(true);//Para que se activen las instrucciones

        //Para los audios de las instrucciones
        AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
        StartCoroutine(audioSource.PlayAudio(soundsToPlay));

        //Para definir las respuestas correctas
        if (n == 0)
        {
            animationCar.AnswerCorrect = "Popcorn";
        }
        if (n == 1)
        {
            animationCar.AnswerCorrect = "Popcorn";
        }
        if (n == 2)
        {
            animationCar.AnswerCorrect = "Icecream";
        }
        yield return null;
    }
    //Para la corrutina
    void OnEnable()
    {
        StartCoroutine(Rail());
    }
}


