using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController_ExpT : MonoBehaviour
{
    //UI Elements Variables
    public Text txtColorSolicited;
    public Sprite[] spritesNoAvailableColors = new Sprite[5]; //Green, Orange, Purple, Black, White
    
    //Controllers Scripts Variables
    private AudioController audioController;
    private AnimationControllerExpT animationController;
    
    //GameObject Variables
    private GameObject bubble;
    private GameObject cottomCandy;
    private GameObject corrector;
    
    private GameObject yellowSugar;
    private GameObject blueSugar;
    private GameObject redSugar;

    private GameObject correctCottomCandy;

    private GameObject noAvailableColor;

    private Animator CocoAnimator;

    //logic Variables
    public AudioClip[] audioClips = new AudioClip[13]; //Instruction, makegreen, makeorange, makepurple, coolgreen, coolorange, coolpurple, firstred, firstblue, firstyellow, not color, good, wrong
    private string[] possibleColors = new string[3] {"green", "orange", "purple" };
    
    private string correctColor;
    private string[] orderOfColors;
    private string pressedColor;

    private int finishColors = 0;
    private int correctAnswers = 0;
    private int incorrectAnswers = 0;

    //Variables to send data to database
    private DatabaseController database;
    private float gameTotalTime = 0f;
    private string subject = "Colors";
    private int level = 0;

    //Variables of Game Finished
    private bool gameFinished = false;
    public GameObject panelGameFinished;

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    
    private string colorToPrepare(){
        int randomColor = UnityEngine.Random.Range(0, possibleColors.Length);
        return possibleColors[randomColor];
    }

    private IEnumerator startGame(){
        float recorredTime = 0;

        AudioClip[] audios = new AudioClip[]{audioClips[0]};
        StartCoroutine(audioController.playAudio(audios));

        while(recorredTime < 7f){
            recorredTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(assignColor());
    }
    
    private IEnumerator assignColor(){
        string color = colorToPrepare();
        bubble.SetActive(true);

        AudioClip[] audios = new AudioClip[1];

        switch(color){
            case "green":
                correctColor = "green";
                cottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.4182863f, 0.6792453f, 0.3043788f);
                audios[0] = audioClips[1];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "orange":
                correctColor = "orange";
                cottomCandy.GetComponent<SpriteRenderer>().color = new Color(1f, 0.6070782f, 0.3349057f);
                audios[0] = audioClips[2];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "purple":
                correctColor = "purple";
                cottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.7988421f, 0.3803922f, 1f);
                audios[0] = audioClips[3];
                StartCoroutine(audioController.playAudio(audios));
                break;
        }

        float recorredTime = 0;
        while(recorredTime < 4f){
            recorredTime += Time.deltaTime;
            yield return null;
        }
        
        sequenceOfColors();
        requestColor();
    }
    
    private void sequenceOfColors(){
        orderOfColors = new string[2];
        switch(correctColor){
            case "green":
                orderOfColors[0] = "yellow";
                orderOfColors[1] = "blue";
                break;
            case "orange":
                orderOfColors[0] = "red";
                orderOfColors[1] = "yellow";
                break;
            case "purple":
                orderOfColors[0] = "blue";
                orderOfColors[1] = "red";
                break;
        }
    }

    public void requestColor(){
        AudioClip[] audios = new AudioClip[1];
        string color = orderOfColors[0];
        switch(color){
            case "yellow":
                txtColorSolicited.text = "yellow";
                txtColorSolicited.color = Color.yellow;
                audios[0] = audioClips[9];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "blue":
                txtColorSolicited.text = "blue";
                txtColorSolicited.color = Color.blue;
                audios[0] = audioClips[8];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "red":
                txtColorSolicited.text = "red";
                txtColorSolicited.color = Color.red;
                audios[0] = audioClips[7];
                StartCoroutine(audioController.playAudio(audios));
                break;
        }
        correctCottomCandy.SetActive(false);
    }
    
    private IEnumerator checkColor(){
        if(pressedColor == orderOfColors[0]){
            correctAnswers++;

            CocoAnimator.Play("CocoFelizChef");

            AudioClip[] audios = new AudioClip[1]{audioClips[11]};
            StartCoroutine(audioController.playAudio(audios));

            float recorredTime = 0;
            
            switch(pressedColor){
                case "yellow":
                    RemoveAt(ref orderOfColors, 0);
                    StartCoroutine(animationController.PlayAnimationSugar(yellowSugar, "VertirAmarilla"));
                    break;
                case "blue":
                    RemoveAt(ref orderOfColors, 0);
                    StartCoroutine(animationController.PlayAnimationSugar(blueSugar, "VertirAzul"));
                    break;
                case "red":
                    RemoveAt(ref orderOfColors, 0);
                    StartCoroutine(animationController.PlayAnimationSugar(redSugar, "VertirRojo"));
                    break;
            }

            while(recorredTime < 4f){
                recorredTime += Time.deltaTime;
                yield return null;
            }

            if(correctAnswers == 2){
                float waitTime = 0;
                
                correctCottomCandy.SetActive(true);

                AudioClip[] audios2 = new AudioClip[1];

                switch(correctColor){
                    case "green":
                        correctCottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.4182863f, 0.6792453f, 0.3043788f);
                        audios2[0] = audioClips[4];
                        break;
                    case "orange":
                        correctCottomCandy.GetComponent<SpriteRenderer>().color = new Color(1f, 0.6070782f, 0.3349057f);
                        audios2[0] = audioClips[5];
                        break;
                    case "purple":
                        correctCottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.7988421f, 0.3803922f, 1f);
                        audios2[0] = audioClips[6];
                        break;
                }

                StartCoroutine(audioController.playAudio(audios2));

                RemoveAt(ref possibleColors, Array.IndexOf(possibleColors, correctColor));

                while(waitTime < 4f){
                    waitTime += Time.deltaTime;
                    yield return null;
                }

                StartCoroutine(assignColor());
                finishColors++;
                correctAnswers = 0;
            }
        }
        else if(pressedColor == "green" || pressedColor == "orange" || pressedColor == "purple" || pressedColor == "black" || pressedColor == "white"){
            incorrectAnswers++;
            
            CocoAnimator.Play("CocoTristeChef");

            AudioClip[] audios = new AudioClip[2]{audioClips[12], audioClips[10]}; //Cambiar segundo audio por el final
            StartCoroutine(audioController.playAudio(audios));

            switch (pressedColor){
                case "green":
                    noAvailableColor.SetActive(true);
                    noAvailableColor.GetComponent<SpriteRenderer>().sprite = spritesNoAvailableColors[0];
                    break;
                case "orange":
                    noAvailableColor.SetActive(true);
                    noAvailableColor.GetComponent<SpriteRenderer>().sprite = spritesNoAvailableColors[1];
                    break;
                case "purple":
                    noAvailableColor.SetActive(true);
                    noAvailableColor.GetComponent<SpriteRenderer>().sprite = spritesNoAvailableColors[2];
                    break;
                case "black":
                    noAvailableColor.SetActive(true);
                    noAvailableColor.GetComponent<SpriteRenderer>().sprite = spritesNoAvailableColors[3];
                    break;
                case "white":
                    noAvailableColor.SetActive(true);
                    noAvailableColor.GetComponent<SpriteRenderer>().sprite = spritesNoAvailableColors[4];
                    break;
            }

            float recorredTime = 0;
            while(recorredTime < 2f){
                recorredTime += Time.deltaTime;
                yield return null;
            }

            noAvailableColor.SetActive(false);
        }else{
            incorrectAnswers++;
            CocoAnimator.Play("CocoTristeChef");

            AudioClip[] audios = new AudioClip[1]{audioClips[12]};
            StartCoroutine(audioController.playAudio(audios));
 
            float recorredTime = 0;

            while(recorredTime < 4.5f){
                recorredTime += Time.deltaTime;
                yield return null;
            }
            
            corrector.SetActive(true);
            string color = orderOfColors[0];
            switch(color){
                case "yellow":
                    corrector.transform.position = new Vector3(yellowSugar.transform.position.x, -3f, 1);
                    break;
                case "blue":
                    corrector.transform.position = new Vector3(blueSugar.transform.position.x, -3f, 1);
                    break;
                case "red":
                    corrector.transform.position = new Vector3(redSugar.transform.position.x, -3f, 1);
                    break;
            }
            requestColor();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Database
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        animationController = GameObject.Find("AnimationController").GetComponent<AnimationControllerExpT>();

        bubble = GameObject.Find("Burbuja");
        cottomCandy = GameObject.Find("Algodon");
        corrector = GameObject.Find("Corrector");

        yellowSugar = GameObject.Find("AzucarAmarilla");
        blueSugar = GameObject.Find("AzucarAzul");
        redSugar = GameObject.Find("AzucarRoja");

        correctCottomCandy = GameObject.Find("AlgodonCorrecto");

        CocoAnimator = GameObject.Find("Coco").GetComponent<Animator>();

        noAvailableColor = GameObject.Find("ColorNoDisponible");
        noAvailableColor.SetActive(false);
        
        bubble.SetActive(false);
        corrector.SetActive(false);
        correctCottomCandy.SetActive(false);

        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        gameTotalTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.F3)){
            corrector.SetActive(false);
            pressedColor = "red";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F2)){
            corrector.SetActive(false);
            pressedColor = "blue";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F1)){
            corrector.SetActive(false);
            pressedColor = "yellow";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F4)){
            corrector.SetActive(false);
            pressedColor = "green";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F5)){
            corrector.SetActive(false);
            pressedColor = "orange";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F6)){
            corrector.SetActive(false);
            pressedColor = "purple";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F7)){
            corrector.SetActive(false);
            pressedColor = "black";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.F8)){
            corrector.SetActive(false);
            pressedColor = "white";
            StartCoroutine(checkColor());
        }

        if(finishColors == 3 && !gameFinished){
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, finishColors, incorrectAnswers, (int)gameTotalTime));
        }
    }
}
