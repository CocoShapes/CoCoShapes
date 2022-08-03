using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Logic Variables
    private string[] spritesShapes = new string[5]{"Circle", "Square", "Triangle", "Star","Rectangle"}; // Array of sprites for shapes: Circle, Square, Triangle, Star, Rectangle
    public Sprite[] spritesBubbles = new Sprite[5]; // Array of sprites for bubbles: Circle, Square, Triangle, Star, Rectangle
    public AudioClip[] audios = new AudioClip[10]; // Array of Audios to play: Instruction, Im Circle, Im Square, Im Triangle, Im Star, Im Rectangle, Good Job, Bad Job, Correct, error 

    // GameObject Variables
    private GameObject canyon;
    private GameObject Coco;

    private string answer; //Variable for answer of user
    public string correctAnswer; // Variable for correct answer
    private bool getResponse;

    public bool isPlaying;

    private int randomShape;

    private int correctAnswers = 0; // Variable for correct answers
    private int incorrectAnswers = 0; // Variable for incorrect answers

    //Other Scripts Variables
    private LaunchShape launchShape;
    private AudioControlLev1FigJ audioControl;

    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Shapes";
    private int level = 1;

    public GameObject panelGameFinished;
    private bool gameFinished = false;
    private float totalGameTime;

    private IEnumerator StartGame()
    {
        float recorredTime = 0f;
        AudioClip[] instruction = new AudioClip[1]{audios[0]};
        StartCoroutine(audioControl.PlayAudio(instruction));

        while (recorredTime < instruction[0].length + 0.3f)
        {
            recorredTime += Time.deltaTime;
            yield return null;
        }
        
        launchShape.Launch(selectShape());
        Coco.GetComponent<Animator>().Play("CocoCanon");
        canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
    }

    private string selectShape()
    {
        randomShape = UnityEngine.Random.Range(0, spritesShapes.Length);

        AudioClip[] audioShape = new AudioClip[1];

        switch (spritesShapes[randomShape])
        {
            case "Circle":
                correctAnswer = "Circle";
                audioShape[0] = audios[1];
                break;
            case "Square":
                correctAnswer = "Square";
                audioShape[0] = audios[2];
                break;
            case "Triangle":
                correctAnswer = "Triangle";
                audioShape[0] = audios[3];
                break;
            case "Star":
                correctAnswer = "Star";
                audioShape[0] = audios[4];
                break;
            case "Rectangle":
                correctAnswer = "Rectangle";
                audioShape[0] = audios[5];
                break;
        }
        StartCoroutine(audioControl.PlayAudio(audioShape));
        return spritesShapes[randomShape];
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }

    private IEnumerator CorrectAnswer()
    {
        //Play Audio
        AudioClip[] audiosToPlay = new AudioClip[2]{audios[8], audios[6]};
        StartCoroutine(audioControl.PlayAudio(audiosToPlay));

        float waitTime = 0;

        while (waitTime < audiosToPlay[0].length + audiosToPlay[1].length)
        {
            waitTime += Time.deltaTime;
            yield return null;
        }

        //Remove the shape from the array
        if(spritesShapes.Length > 1){
            RemoveAt(ref spritesShapes, randomShape);
        }

        launchShape.Launch(selectShape());
        Coco.GetComponent<Animator>().Play("CocoCanon");
        canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
    }

    private IEnumerator IncorrectAnswer()
    {
        //Play Audio
        AudioClip[] audiosToPlay = new AudioClip[2]{audios[9], audios[7]};
        StartCoroutine(audioControl.PlayAudio(audiosToPlay));

        float waitTime = 0;

        while (waitTime < audiosToPlay[0].length + audiosToPlay[1].length)
        {
            waitTime += Time.deltaTime;
            yield return null;
        }

        launchShape.Launch(selectShape());
        Coco.GetComponent<Animator>().Play("CocoCanon");
        canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
    }
    
    void Start()
    {
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        
        isPlaying = true;
        getResponse = false;
        
        canyon = GameObject.Find("Canyon");
        Coco = GameObject.Find("Coco");
        
        launchShape = GameObject.Find("AnimationController").GetComponent<LaunchShape>();
        audioControl = GameObject.Find("AudioController").GetComponent<AudioControlLev1FigJ>();

        StartCoroutine(StartGame());
    }

    void Update()
    {
        totalGameTime += Time.deltaTime;

        if(isPlaying){
            //When the system is waiting for the answer of student
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                answer = "Circle";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                answer = "Square";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                answer = "Triangle";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.Backspace)){
                answer = "Rectangle";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.Tab)){
                answer = "Star";
                getResponse = true;
            }
        }else {
            //When the time for the answer is finished
            incorrectAnswers++;
            launchShape.Launch(selectShape());
            canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
            Coco.GetComponent<Animator>().Play("CocoCanon");
            isPlaying = true;
        }

        if(getResponse){
            if(answer == correctAnswer){
                //Correct answer
                correctAnswers++;
                StartCoroutine(CorrectAnswer());
            }else{
                //Wrong answer
                incorrectAnswers++;
                StartCoroutine(IncorrectAnswer());
            }
            getResponse = false;
        }

        if(correctAnswers >= 5 && !gameFinished){
            launchShape.gameObject.SetActive(false);
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, incorrectAnswers, (int)totalGameTime));
            this.gameObject.SetActive(false);
        }
        
        if(incorrectAnswers >= 3 && !gameFinished){
            launchShape.gameObject.SetActive(false);
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, incorrectAnswers, (int)totalGameTime));
            this.gameObject.SetActive(false);
        }
    }
}