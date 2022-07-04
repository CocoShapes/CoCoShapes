using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Logic Variables
    public Sprite[] spritesShapes = new Sprite[10]; // Array of sprites for shapes: Circle, Square, Triangle, Star, Rectangle
    public Sprite[] spritesBubbles = new Sprite[5]; // Array of sprites for bubbles: Circle, Square, Triangle, Star, Rectangle
    public AudioClip[] audios = new AudioClip[2]; // Array of Audios to play: Good, Bad

    private string answer; //Variable for answer of user
    private string correctAnswer; // Variable for correct answer
    private bool getResponse;

    public bool isPlaying;

    private int randomShape;

    private int correctAnswers = 0; // Variable for correct answers
    private int incorrectAnswers = 0; // Variable for incorrect answers

    //Other Scripts Variables
    private LaunchShape launchShape;
    private AudioControlLev1FigJ audioControl;

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
        isPlaying = true;
        getResponse = false;
        
        launchShape = GameObject.Find("AnimationController").GetComponent<LaunchShape>();
        launchShape.Launch(selectShape());

        audioControl = GameObject.Find("AudioController").GetComponent<AudioControlLev1FigJ>();
    }

    void Update()
    {
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
            isPlaying = true;
        }

        if(getResponse){
            if(answer == correctAnswer){
                //Correct answer
                correctAnswers++;

                //Play Audio
                audioControl.playAudio(audios[0]);

                //Remove the shape from the array
                if(spritesShapes.Length > 1){
                    RemoveAt(ref spritesShapes, randomShape);
                }
    
                launchShape.Launch(selectShape());
                Debug.Log("Correct answer");
            }else{
                //Wrong answer
                incorrectAnswers++;

                //Play Audio
                audioControl.playAudio(audios[1]);

                launchShape.Launch(spritesShapes[randomShape]);
                Debug.Log("Wrong answer");
            }
            getResponse = false;
        }

        if(correctAnswers >= 10){
            //When the student has finished the level
            Debug.Log("Level finished");
        }
        
        if(incorrectAnswers >= 3){
            //Game Over
            Debug.Log("Game Over");
        }
    }
}