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

    public AudioClip[] audios = new AudioClip[4]; //O = Red, 1 = Blue, 2 = Yellow, 3 = Instruction
    public AudioController audioSource;

    //GameObjects Variables
    private GameObject gameObjectBall;
    public LaunchBall launchBall;

    private Animator cocoAnimator;

    private GameObject homeRed;
    private GameObject homeBlue;
    private GameObject homeYellow;
    
    //Logic Variables
    private string[] colors = new string[] { "red", "blue", "yellow", "red", "blue", "yellow"};
    private int random;
    public string answerColor;
    private Vector3 ballPosition;
    private int score;
    private int mistakes;
    private bool gameFinished = false;

    //Variables to send data to database
    private DatabaseController database;
    private float gameTotalTime = 0f;
    private string subject = "Colors";
    private int level = 1;

    //prefab for game finished
    public GameObject PanelGameFinished;

    private IEnumerator startGame()
    {
        float recorredTime = 0f;

        AudioClip[] instruction = new AudioClip[1]{audios[3]};
        StartCoroutine(audioSource.playAudio(instruction));
        
        while(recorredTime < audios[3].length + 0.5f)
        {
            recorredTime += Time.deltaTime;
            yield return null;
        }

        //Start game
        txtColor.text = asignColor();
    }
    
    //Asign a random color of the list to the text
    private string asignColor(){
        if(colors.Length > 0){
            random = Random.Range(0, colors.Length);

            if(colors[random] == "red"){
                AudioClip[] audiosToPlay = new AudioClip[] {audios[0]};
                StartCoroutine(audioSource.playAudio(audiosToPlay));
            } else if(colors[random] == "blue"){
                AudioClip[] audiosToPlay = new AudioClip[] {audios[1]};
                StartCoroutine(audioSource.playAudio(audiosToPlay));
            } else if(colors[random] == "yellow"){
                AudioClip[] audiosToPlay = new AudioClip[] {audios[2]};
                StartCoroutine(audioSource.playAudio(audiosToPlay));
            }

            return colors[random];
        }else {
            return "good job!";
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
            txtColor.color = Color.black;
        } else {
            mistakes++;
            imgMistake.gameObject.SetActive(true);
            switch(txtColor.text){
                case "red":
                    txtColor.color = Color.red;
                    break;
                case "blue":
                    txtColor.color = Color.blue;
                    break;
                case "yellow":
                    txtColor.color = Color.yellow;
                    break;
            }
        }
    }
    
    void Start(){
        //Database
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        
        gameObjectBall = GameObject.Find("Ball");
        ballPosition = gameObjectBall.transform.position;
        gameObjectBall.SetActive(false);
        imgMistake.gameObject.SetActive(false);

        cocoAnimator = GameObject.Find("Coco").GetComponent<Animator>();
        
        homeRed = GameObject.Find("HomeRed");
        homeBlue = GameObject.Find("HomeBlue");
        homeYellow = GameObject.Find("HomeYellow");
        
        StartCoroutine(startGame());

        score = 0;
        mistakes = 0;
    }

    void Update(){
        //Add time to total time
        gameTotalTime += Time.deltaTime;

        //To launch the ball when the player press a button in the keyboard
        if(Input.GetKeyDown(KeyCode.F3)){
            //When recieve the color equal to Red
            gameObjectBall.SetActive(true);
            imgMistake.gameObject.SetActive(false);

            //Animation of Coco
            cocoAnimator.Play("LanzarPelota");
            
            launchBall.color = "Red";
            launchBall.playedSound = false;

            StartCoroutine(launchBall.launch());

            AudioClip[] audiosToPlay = new AudioClip[] {audios[0]}; 
            StartCoroutine(audioSource.playAudio(audiosToPlay));

            answerColor = "red";
            checkAnswer();
        } else if(Input.GetKeyDown(KeyCode.F2)){
            //When recieve the color equal to Blue
            gameObjectBall.SetActive(true);
            imgMistake.gameObject.SetActive(false);
            
            //Animation of Coco
            cocoAnimator.Play("LanzarPelota");
            
            launchBall.color = "Blue";
            launchBall.playedSound = false;

            StartCoroutine(launchBall.launch());

            AudioClip[] audiosToPlay = new AudioClip[] {audios[1]};
            StartCoroutine(audioSource.playAudio(audiosToPlay));

            answerColor = "blue";
            checkAnswer();
        } else if(Input.GetKeyDown(KeyCode.F1)){
            //When recieve the color equal to Yellow
            gameObjectBall.SetActive(true);
            imgMistake.gameObject.SetActive(false);

            //Animation of Coco
            cocoAnimator.Play("LanzarPelota");
            
            launchBall.color = "Yellow";
            launchBall.playedSound = false;

            StartCoroutine(launchBall.launch());

            AudioClip[] audiosToPlay = new AudioClip[] {audios[2]};
            StartCoroutine(audioSource.playAudio(audiosToPlay));

            answerColor = "yellow";
            checkAnswer();
        }

        //To check if the player has lost
        if (mistakes >= 3 && !gameFinished){
            txtColor.text = "keep trying!";
            imgMistake.gameObject.SetActive(false);

            PanelGameFinished.SetActive(true);
            gameFinished = true;
            StartCoroutine(database.PushResult(subject, level, score, mistakes, (int)gameTotalTime));
        }

        //To check if the player has won
        if (colors.Length <= 0 && !gameFinished){
            txtColor.text = "nice job!";
            cocoAnimator.Play("CelebracionFinal");

            PanelGameFinished.SetActive(true);
            gameFinished = true;
            StartCoroutine(database.PushResult(subject, level, score, mistakes, (int)gameTotalTime));
        }
    }
}