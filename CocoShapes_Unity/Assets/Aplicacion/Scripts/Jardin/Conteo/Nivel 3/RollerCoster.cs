using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCoster : MonoBehaviour
{
    public AnsController answerController;
    public GameObject[] Scenes;

    public GameObject[] Instructions;

    //Para lo de los sonidos
    public AudioClip[] sounds = new AudioClip[12];
    public AudioControl audioSource;

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject scene in Scenes)
            {
                scene.SetActive(false);
            }
            foreach (GameObject instruction in Instructions)
            {
                instruction.SetActive(false);
            }

            //Para que las instrucciones se muestren aleatoriamente
            int n = Random.Range(0, Scenes.Length);

            Scenes[n].SetActive(true);
            Instructions[n].SetActive(true);

            //Para los audios de las instrucciones
            StartCoroutine(audioSource.PlayAudio(sounds[n]));

            //Para definir las respuestas correctas
            if (n == 0)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
            if (n == 1)
            {
                answerController.AnswerCorrect = "IceCream";
            }
            if (n == 2)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
            if (n == 3)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
            if (n == 4)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
            if (n == 5)
            {
                answerController.AnswerCorrect = "IceCream";
            }
            if (n == 6)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
            if (n == 7)
            {
                answerController.AnswerCorrect = "IceCream";
            }
            if (n == 8)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
            if (n == 9)
            {
                answerController.AnswerCorrect = "Popcorn";
            }
        }
    }
}
