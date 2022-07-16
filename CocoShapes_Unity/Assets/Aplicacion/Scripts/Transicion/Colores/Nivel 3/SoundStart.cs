using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStart : MonoBehaviour
{
    //Para los audios
    public AudioClip[] sounds = new AudioClip[8];
    public SoundController audioSource;

    //Para que se muestren aleatoriamente los audios
    public int n;

    //Instrucciones(imágenes)
    public GameObject[] TextsCol;

    //Para desactivar los círculos rojos
    public GameObject IncorrectGreen;
    public GameObject IncorrectRed;
    public GameObject IncorrectWhite;
    public GameObject IncorrectPurple;
    public GameObject IncorrectBlack;
    public GameObject IncorrectYellow;
    public GameObject IncorrectBlue;
    public GameObject IncorrectOrange;

    void Start()
    {

    }
    void Update()
    {
        //Para que suenen las instrucciones
        if (Input.GetKeyDown(KeyCode.S))
        {
            n = Random.Range(0, 7);
            AudioClip[] soundsToPlay = new AudioClip[1] { sounds[n] };
            StartCoroutine(audioSource.PlayAudio(soundsToPlay));

            //Para mostrar las instrucciones
            TextsCol[n].SetActive(true);//Para que se activen

            //Para que se desactiven los círculos rojos
            IncorrectGreen.SetActive(false);
            IncorrectRed.SetActive(false);
            IncorrectWhite.SetActive(false);
            IncorrectPurple.SetActive(false);
            IncorrectBlack.SetActive(false);
            IncorrectYellow.SetActive(false);
            IncorrectBlue.SetActive(false);
            IncorrectOrange.SetActive(false);
        }
    }
}
