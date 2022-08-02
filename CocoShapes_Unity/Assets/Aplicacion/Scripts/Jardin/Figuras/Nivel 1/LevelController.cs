using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Logic Variables
    public Sprite[] spritesShapes = new Sprite[10]; // Array of sprites for shapes: Circle, Square, Triangle, Star, Rectangle
    public Sprite[] spritesBubbles = new Sprite[5]; // Array of sprites for bubbles: Circle, Square, Triangle, Star, Rectangle
    public AudioClip[] audios = new AudioClip[4]; // Array of Audios to play: Good, Bad, Good Job, Keep Trying

    // GameObject Variables
    private GameObject canyon;

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

    private Sprite selectShape()
    {
        randomShape = UnityEngine.Random.Range(0, spritesShapes.Length);

        switch (spritesShapes[randomShape].name)
        {
            case "Circle":
                correctAnswer = "Circle";
                break;
            case "Square":
                correctAnswer = "Square";
                break;
            case "Triangle":
                correctAnswer = "Triangle";
                break;
            case "Star":
                correctAnswer = "Star";
                break;
            case "Rectangle":
                correctAnswer = "Rectangle";
                break;
        }
        return spritesShapes[randomShape];
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    
    void Start()
    {
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        
        isPlaying = true;
        getResponse = false;
        
        canyon = GameObject.Find("Canyon");
        
        launchShape = GameObject.Find("AnimationController").GetComponent<LaunchShape>();
        launchShape.Launch(selectShape());
        canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);

        audioControl = GameObject.Find("AudioController").GetComponent<AudioControlLev1FigJ>();
    }

    void Update()
    {
        totalGameTime += Time.deltaTime;

        if(isPlaying){
            //When the system is waiting for the answer of student
            if(Input.GetKeyDown(KeyCode.C)){
                answer = "Circle";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.S)){
                answer = "Square";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.T)){
                answer = "Triangle";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.R)){
                answer = "Rectangle";
                getResponse = true;
            }
            if(Input.GetKeyDown(KeyCode.A)){
                answer = "Star";
                getResponse = true;
            }
        }else {
            //When the time for the answer is finished
            Debug.Log("Incorret answer ++");
            incorrectAnswers++;
            launchShape.Launch(selectShape());
            canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
            isPlaying = true;
        }

        if(getResponse){
            if(answer == correctAnswer){
                //Correct answer
                correctAnswers++;

                //Play Audio
                AudioClip[] audiosToPlay = new AudioClip[2]{audios[0], audios[2]};
                StartCoroutine(audioControl.PlayAudio(audiosToPlay));

                //Remove the shape from the array
                if(spritesShapes.Length > 1){
                    RemoveAt(ref spritesShapes, randomShape);
                }
    
                launchShape.Launch(selectShape());
                canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
                Debug.Log("Correct answer");
            }else{
                //Wrong answer
                incorrectAnswers++;

                //Play Audio
                AudioClip[] audiosToPlay = new AudioClip[2]{audios[1], audios[3]};
                StartCoroutine(audioControl.PlayAudio(audiosToPlay));

                launchShape.Launch(spritesShapes[randomShape]);
                canyon.GetComponent<Animator>().Play("Cañon", -1, 0f);
                Debug.Log("Wrong answer");
            }
            getResponse = false;
        }

        if(correctAnswers >= 10 && !gameFinished){
            //When the student has finished the level
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, incorrectAnswers, (int)totalGameTime));
        }
        
        if(incorrectAnswers >= 3 && !gameFinished){
            //Game Over
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, incorrectAnswers, (int)totalGameTime));
        }
    }
}