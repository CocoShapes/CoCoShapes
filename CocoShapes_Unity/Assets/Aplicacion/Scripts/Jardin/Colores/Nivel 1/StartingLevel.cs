using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingLevel : MonoBehaviour
{
    //UI Elements
    public Text txtColor;
    public Image imgMistake;

    public AudioClip[] audios = new AudioClip[3]; //O = Red, 2 = Blue, 3 = Yellow
    public AudioController audioSource;

    //GameObjects Variables
    private GameObject gameObjectBall;
    public LaunchBall launchBall;

    private GameObject homeRed;
    private GameObject homeBlue;
    private GameObject homeYellow;
    
    //Logic Variables
    private string[] colors = new string[] { "RED", "BLUE", "YELLOW", "RED", "BLUE", "YELLOW"};
    private int random;
    private string answerColor;
    private Vector3 ballPosition;
    private int score;
    private int mistakes;

    //Asign a random color of the list to the text
    private string asignColor(){
        if(colors.Length > 0){
            random = Random.Range(0, colors.Length);

            if(colors[random] == "RED"){
                AudioClip[] audiosToPlay = new AudioClip[] {audios[0]};
                StartCoroutine(audioSource.playAudio(audiosToPlay));
            } else if(colors[random] == "BLUE"){
                AudioClip[] audiosToPlay = new AudioClip[] {audios[1]};
                StartCoroutine(audioSource.playAudio(audiosToPlay));
            } else if(colors[random] == "YELLOW"){
                AudioClip[] audiosToPlay = new AudioClip[] {audios[2]};
                StartCoroutine(audioSource.playAudio(audiosToPlay));
            }

            return colors[random];
        }else {
            return "GOOD JOB!";
        }
    }

    private void checkAnswer(){
        homeRed.transform.Rotate(new Vector3(0, 0, 0));
        homeBlue.transform.Rotate(new Vector3(0, 0, 0));
        homeYellow.transform.Rotate(new Vector3(0, 0, 0));
        
        if(answerColor == txtColor.text){
            List<string> colorsList = colors.ToList();
            colorsList.RemoveAt(random);
            colors = colorsList.ToArray();

            score++;
            txtColor.text = asignColor();
        } else {
            mistakes++;
            imgMistake.gameObject.SetActive(true);
        }
    }
    
    void Start(){
        gameObjectBall = GameObject.Find("Ball");
        ballPosition = gameObjectBall.transform.position;
        gameObjectBall.SetActive(false);

        homeRed = GameObject.Find("HomeRed");
        homeBlue = GameObject.Find("HomeBlue");
        homeYellow = GameObject.Find("HomeYellow");
        
        txtColor.text = asignColor();

        score = 0;
        mistakes = 0;
    }

    void Update(){
        //To launch the ball when the player press a button in the keyboard
        if(Input.GetKeyDown(KeyCode.R)){
            //When recieve the color equal to Red
            gameObjectBall.SetActive(true);
            imgMistake.gameObject.SetActive(false);
            gameObjectBall.transform.position = ballPosition;
            
            launchBall.color = "Red";
            launchBall.playedSound = false;

            StartCoroutine(launchBall.launch());

            AudioClip[] audiosToPlay = new AudioClip[] {audios[0]}; 
            StartCoroutine(audioSource.playAudio(audiosToPlay));

            answerColor = "RED";
            checkAnswer();
        } else if(Input.GetKeyDown(KeyCode.B)){
            //When recieve the color equal to Blue
            gameObjectBall.SetActive(true);
            imgMistake.gameObject.SetActive(false);
            gameObjectBall.transform.position = ballPosition;
            
            launchBall.color = "Blue";
            launchBall.playedSound = false;

            StartCoroutine(launchBall.launch());

            AudioClip[] audiosToPlay = new AudioClip[] {audios[1]};
            StartCoroutine(audioSource.playAudio(audiosToPlay));

            answerColor = "BLUE";
            checkAnswer();
        } else if(Input.GetKeyDown(KeyCode.Y)){
            //When recieve the color equal to Yellow
            gameObjectBall.SetActive(true);
            imgMistake.gameObject.SetActive(false);
            gameObjectBall.transform.position = ballPosition;
            
            launchBall.color = "Yellow";
            launchBall.playedSound = false;

            StartCoroutine(launchBall.launch());

            AudioClip[] audiosToPlay = new AudioClip[] {audios[2]};
            StartCoroutine(audioSource.playAudio(audiosToPlay));

            answerColor = "YELLOW";
            checkAnswer();
        }

        //To check if the player has lost
        if (mistakes >= 3){
            Debug.Log("You lost");
            txtColor.text = "KEEP TRYING!";
            imgMistake.gameObject.SetActive(false);
        }

        //To check if the player has won
        if (colors.Length <= 0){
            Debug.Log("You won");
        }
    }
}