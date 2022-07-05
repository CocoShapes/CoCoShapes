using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControllerCont1J : MonoBehaviour
{
    //UI Elements
    public Text txtRespuesta1;
    public Text txtRespuesta2;
    public Text txtRespuesta3;

    //GameObjects Scripts
    public ConstructMountain controllerMountain;
    private AnimationControllerCon1J controllerAnimation;
    private AudioControllerCon1J controllerAudio;

    //Audio Array
    public AudioClip[] audioClips; //Correct Sound, Good job, Wrong Sound, Keep Trying
    
    //GameObjects Variables
    public GameObject car;
    public GameObject finalRail;
    private GameObject sugar;

    //Logic Variables
    public int correctAnswer;
    public int wrongAnswer1;
    public int wrongAnswer2;

    private int correctAnswers;
    private int wrongAnswers;

    private bool goodResponse;

    public bool hasReponse;

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }

    public void selectScenario(){
        controllerMountain.constructMountain();

        //Igualar posición de Sugar a Rail Final
        sugar.transform.position = new Vector3(finalRail.transform.position.x, finalRail.transform.position.y + 0.75f, finalRail.transform.position.z);

        //Asignar opciones de respuesta a botones de UI aleatoriamente
        wrongAnswer1 = UnityEngine.Random.Range(correctAnswer - 2, correctAnswer + 2);
        wrongAnswer2 = UnityEngine.Random.Range(correctAnswer - 2, correctAnswer + 2);

        while (wrongAnswer1 == correctAnswer || wrongAnswer1 == wrongAnswer2)
        {
            wrongAnswer1 = UnityEngine.Random.Range(correctAnswer - 2, correctAnswer + 2);
        }

        while (wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1)
        {
            wrongAnswer2 = UnityEngine.Random.Range(correctAnswer - 2, correctAnswer + 2);
        }

        int[] possibleAnswers = new int[3]{correctAnswer, wrongAnswer1, wrongAnswer2};

        for(int i = 0; i < 3; i++){
            int random = UnityEngine.Random.Range(0, possibleAnswers.Length);
            if(i == 0)
                txtRespuesta1.text = possibleAnswers[random].ToString();
            else if(i == 1)
                txtRespuesta2.text = possibleAnswers[random].ToString();
            else
                txtRespuesta3.text = possibleAnswers[random].ToString();

            RemoveAt(ref possibleAnswers, random);
        }

        hasReponse = false;
    }

    public void goodAnswer(){
        correctAnswers++;
        finalRail.GetComponent<SpriteRenderer>().color = Color.white;

        goodResponse = true;

        //Reproducir Audios de correcto
        AudioClip[] audios = new AudioClip[2]{audioClips[0], audioClips[1]};
        StartCoroutine(controllerAudio.PlayAudio(audios));
        
        //Animación de carrito
        StartCoroutine(controllerAnimation.MoveCar(car, finalRail, 5.0f));

        //Animación de Coco Feliz
    }

    public void badAnswer(){
        wrongAnswers++;

        //Reproducir Audios de incorrecto
        AudioClip[] audios = new AudioClip[2]{audioClips[2], audioClips[3]};
        StartCoroutine(controllerAudio.PlayAudio(audios));
        
        finalRail.GetComponent<SpriteRenderer>().color = Color.red;

        //Animación de Coco triste
    }
    
    void Start() 
    {
        //Rellenar el array de escenarios
        for (int i = 1; i <= controllerMountain.scenarios.Length; i++)
        {
            controllerMountain.scenarios[i - 1] = GameObject.Find("Escenario " + (i + 1));
            controllerMountain.scenarios[i - 1].SetActive(false);
        }

        //Asignar objeto a variable Sugar
        sugar = GameObject.Find("Sugar");

        //Iniciar juego
        selectScenario();

        //Animation Controller
        controllerAnimation = GameObject.Find("AnimationController").GetComponent<AnimationControllerCon1J>();

        //Audio Controller
        controllerAudio = GameObject.Find("AudioController").GetComponent<AudioControllerCon1J>();

        //Has response 
        hasReponse = false;

        //Good Response
        goodResponse = false;
    }

    void Update()
    {
        if(hasReponse){
            //Lo que pasa cuando termina la animación de Coco en la montaña rusa
            if(goodResponse){
                //Animación de Coco con el algodon de azucar

                //Desactivar escenario actual
                controllerMountain.scenarios[controllerMountain.scenarioNumber].SetActive(false);

                //Remover escenario actual del array
                RemoveAt(ref controllerMountain.scenarios, controllerMountain.scenarioNumber);
                selectScenario();
            }
            hasReponse = false;
        }
        
        if(correctAnswers >= 5){
            Debug.Log("Level Complete");
        }

        if(wrongAnswers >= 3){
            Debug.Log("Level Failed");
        }
    }
}
