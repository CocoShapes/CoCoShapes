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
    private GameObject GameView;

    //GameObjects Scripts
    public ConstructMountain controllerMountain;
    private AnimationControllerCon1J controllerAnimation;
    public AudioControllerCon1J controllerAudio;

    //Audio Array
    public AudioClip[] audioClips = new AudioClip[14]; //Instruction, 2, 3, 4, 5, 6, 7, 8, 9, 10, good job, wrong, correcto, error
    
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

    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Count";
    private int level = 1;

    public GameObject panelGameFinished;
    private bool gameFinished = false;
    private float totalGameTime;

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }

    private IEnumerator StartGame()
    {
        float waitTime = 0;

        selectScenario();
        AudioClip[] instruction = new AudioClip[1]{audioClips[0]};
        StartCoroutine(controllerAudio.PlayAudio(instruction));
        
        while(waitTime < instruction[0].length)
        {
            waitTime += Time.deltaTime;
            yield return null;
        }
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
        GameView.SetActive(true);
        goodResponse = false;
        hasReponse = false;
    }

    public IEnumerator goodAnswer(){
        correctAnswers++;
        GameView.SetActive(false);

        float recorredTime = 0f;

        while(recorredTime < 1f){
            recorredTime += Time.deltaTime;
            yield return null;
        }

        finalRail.GetComponent<SpriteRenderer>().color = Color.white;

        goodResponse = true;

        //Reproducir Audios de correcto
        AudioClip[] audios = new AudioClip[2]{audioClips[11], audioClips[10]};
        StartCoroutine(controllerAudio.PlayAudio(audios));
        StartCoroutine(controllerAnimation.MoveCar(car, finalRail, 2f));
    }

    public IEnumerator badAnswer(){
        wrongAnswers++;
        GameView.SetActive(false);

        float recorredTime = 0f;

        while(recorredTime < 1f){
            recorredTime += Time.deltaTime;
            yield return null;
        }

        recorredTime = 0f;
        //Reproducir Audios de incorrecto
        AudioClip[] audios = new AudioClip[2]{audioClips[13], audioClips[12]};
        StartCoroutine(controllerAudio.PlayAudio(audios));

        //Move car and return
        StartCoroutine(controllerAnimation.MoverCarAndReturn(car, finalRail, 2f));
        finalRail.GetComponent<SpriteRenderer>().color = Color.red;

        while(recorredTime < 3.5f){
            recorredTime += Time.deltaTime;
            yield return null;
        }

        GameView.SetActive(true);
    }
    
    void Start() 
    {
        //Database
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        
        //Rellenar el array de escenarios
        for (int i = 1; i <= controllerMountain.scenarios.Length; i++)
        {
            controllerMountain.scenarios[i - 1] = GameObject.Find("Escenario " + (i + 1));
            controllerMountain.scenarios[i - 1].SetActive(false);
        }

        //Asignar objeto a variable Sugar
        sugar = GameObject.Find("Sugar");

        //GameView GameObject
        GameView = GameObject.Find("GameView");
        GameView.SetActive(false);

        //Animation Controller
        controllerAnimation = GameObject.Find("AnimationController").GetComponent<AnimationControllerCon1J>();

        //Audio Controller
        controllerAudio = GameObject.Find("AudioController").GetComponent<AudioControllerCon1J>();

        //Has response 
        hasReponse = false;

        //Good Response
        goodResponse = false;

        //Iniciar juego
        StartCoroutine(StartGame());
    }

    void Update()
    {
        totalGameTime += Time.deltaTime;

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
        
        if(correctAnswers >= 5 && !gameFinished){
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, wrongAnswers, (int)totalGameTime));
            gameFinished = true;
        }

        if(wrongAnswers >= 3 && !gameFinished){
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, wrongAnswers, (int)totalGameTime));
            gameFinished = true;
        }
    }
}
