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

    //logic Variables
    public AudioClip[] audioClips = new AudioClip[10]; //Green, Orange, Purple, GM Red, GM Blue, GM Yellow. Good Job, Keep Trying, Good, Wrong, Doesn't have that color.
    private string[] possibleColors = new string[3] {"green", "orange", "purple" };
    
    private string correctColor;
    private string[] orderOfColors;
    private string pressedColor;

    private int finishColors = 0;
    private int correctAnswers = 0;

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }
    
    private string colorToPrepare(){
        int randomColor = UnityEngine.Random.Range(0, possibleColors.Length);
        return possibleColors[randomColor];
    }

    private IEnumerator assignColor(){
        string color = colorToPrepare();
        bubble.SetActive(true);

        AudioClip[] audios = new AudioClip[1];

        switch(color){
            case "green":
                correctColor = "green";
                cottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.4182863f, 0.6792453f, 0.3043788f);
                audios[0] = audioClips[0];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "orange":
                correctColor = "orange";
                cottomCandy.GetComponent<SpriteRenderer>().color = new Color(1f, 0.6070782f, 0.3349057f);
                audios[0] = audioClips[1];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "purple":
                correctColor = "purple";
                cottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.7988421f, 0.3803922f, 1f);
                audios[0] = audioClips[2];
                StartCoroutine(audioController.playAudio(audios));
                break;
        }

        float recorredTime = 0;
        while(recorredTime < 2f){
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
                audios[0] = audioClips[5];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "blue":
                txtColorSolicited.text = "blue";
                txtColorSolicited.color = Color.blue;
                audios[0] = audioClips[4];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "red":
                txtColorSolicited.text = "red";
                txtColorSolicited.color = Color.red;
                audios[0] = audioClips[3];
                StartCoroutine(audioController.playAudio(audios));
                break;
        }
        correctCottomCandy.SetActive(false);
    }
    
    private IEnumerator checkColor(){
        if(pressedColor == orderOfColors[0]){
            Debug.Log("Correct, pressed color " + pressedColor);
            correctAnswers++;

            AudioClip[] audios = new AudioClip[2]{audioClips[8], audioClips[6]};
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
                correctCottomCandy.SetActive(true);

                switch(correctColor){
                    case "green":
                        correctCottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.4182863f, 0.6792453f, 0.3043788f);
                        break;
                    case "orange":
                        correctCottomCandy.GetComponent<SpriteRenderer>().color = new Color(1f, 0.6070782f, 0.3349057f);
                        break;
                    case "purple":
                        correctCottomCandy.GetComponent<SpriteRenderer>().color = new Color(0.7988421f, 0.3803922f, 1f);
                        break;
                }
                RemoveAt(ref possibleColors, Array.IndexOf(possibleColors, correctColor));

                StartCoroutine(assignColor());
                finishColors++;
                correctAnswers = 0;
            }
        }
        else if(pressedColor == "green" || pressedColor == "orange" || pressedColor == "purple" || pressedColor == "black" || pressedColor == "white"){
            Debug.Log("Coco doesn't have color " + pressedColor);

            AudioClip[] audios = new AudioClip[2]{audioClips[9], audioClips[9]}; //Cambiar segundo audio por el final
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
            Debug.Log("Incorrect, pressed color " + pressedColor);
            AudioClip[] audios = new AudioClip[2]{audioClips[9], audioClips[7]};
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
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        animationController = GameObject.Find("AnimationController").GetComponent<AnimationControllerExpT>();

        bubble = GameObject.Find("Burbuja");
        cottomCandy = GameObject.Find("Algodon");
        corrector = GameObject.Find("Corrector");

        yellowSugar = GameObject.Find("AzucarAmarilla");
        blueSugar = GameObject.Find("AzucarAzul");
        redSugar = GameObject.Find("AzucarRoja");

        correctCottomCandy = GameObject.Find("AlgodonCorrecto");

        noAvailableColor = GameObject.Find("ColorNoDisponible");
        noAvailableColor.SetActive(false);
        
        bubble.SetActive(false);
        corrector.SetActive(false);
        correctCottomCandy.SetActive(false);

        StartCoroutine(assignColor());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            corrector.SetActive(false);
            pressedColor = "red";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.B)){
            corrector.SetActive(false);
            pressedColor = "blue";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.Y)){
            corrector.SetActive(false);
            pressedColor = "yellow";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.G)){
            corrector.SetActive(false);
            pressedColor = "green";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.O)){
            corrector.SetActive(false);
            pressedColor = "orange";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.P)){
            corrector.SetActive(false);
            pressedColor = "purple";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.L)){
            corrector.SetActive(false);
            pressedColor = "black";
            StartCoroutine(checkColor());
        }
        if(Input.GetKeyDown(KeyCode.W)){
            corrector.SetActive(false);
            pressedColor = "white";
            StartCoroutine(checkColor());
        }

        if(finishColors == 3){
            Debug.Log("--------------------");
            Debug.Log("Game Finished");
            Debug.Log("--------------------");
        }
    }
}
