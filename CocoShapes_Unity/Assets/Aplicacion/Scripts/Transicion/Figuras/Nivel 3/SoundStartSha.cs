using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStartSha : MonoBehaviour
{
    //Para los audios
    public AudioClip[] sounds = new AudioClip[5];
    public SoundControl audioSource;

    //Instrucciones(imágenes)
    public GameObject[] TextsSha;

    //Para desactivar los círculos rojos
    public GameObject IncorrectCircle;
    public GameObject IncorrectTriangle;
    public GameObject IncorrectSquare;
    public GameObject IncorrectRectangle;
    public GameObject IncorrectStar;

    //Para que se muestren aleatoriamente los audios
    public int n;
    void Start()
    {

    }
    void Update()
    {
        //Para que suenen las instrucciones
        if (Input.GetKeyDown(KeyCode.E))
        {
            n = Random.Range(0, 4);
            AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));

            //Para mostrar las instrucciones
            TextsSha[n].SetActive(true);//Para que se activen

            //Para que se desactiven los círculos rojos
            IncorrectCircle.SetActive(false);
            IncorrectTriangle.SetActive(false);
            IncorrectSquare.SetActive(false);
            IncorrectRectangle.SetActive(false);
            IncorrectStar.SetActive(false);
        }
    }
}
