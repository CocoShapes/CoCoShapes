using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController_Con1T : MonoBehaviour
{
    //UI Variables
    public Text txtR1;
    public Text txtR2;
    public Text txtR3;
    public Text txtR4;
    public Text txtR5;

    public Text btn1;
    public Text btn2;
    public Text btn3;

    public Button mainBtn1;
    public Button mainBtn2;
    public Button mainBtn3;

    //Scripts Variables
    private AnimationControllerCon1T animationController;
    private AudioController audioController;

    //Logic Variables
    public int numberToEvaluate;
    public int answer;

    public int correctAnswers;
    public int wrongAnswers;

    //GameObject Variables
    private GameObject carrito;
    public AudioClip[] numbersSounds = new AudioClip[10];
    public AudioClip[] sounds = new AudioClip[9];

    //Variables to send data to database
    private DatabaseController database;
    private float gameTotalTime = 0f;
    private string subject = "Count";
    private int level = 1;

    //Variables of Game Finished
    private bool gameFinished = false;
    public GameObject panelGameFinished;

    void Start()
    {
        //Database
        database = GameObject.Find("Database").GetComponent<DatabaseController>();

        numberToEvaluate = 0;
        correctAnswers = 0;
        wrongAnswers = 0;

        animationController = GameObject.Find("AnimationController").GetComponent<AnimationControllerCon1T>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        carrito = GameObject.Find("Carrito");

        //Start Game
        StartCoroutine(StartGame());
    }

    void Update()
    {
        gameTotalTime += Time.deltaTime;

        if (correctAnswers == 5 && !gameFinished)
        {
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, wrongAnswers, (int)gameTotalTime));
        }
        if (wrongAnswers == 3 && !gameFinished)
        {
            gameFinished = true;
            panelGameFinished.SetActive(true);
            StartCoroutine(database.PushResult(subject, level, correctAnswers, wrongAnswers, (int)gameTotalTime));
        }
    }

    private IEnumerator StartGame()
    {
        float waitTime = 0f;

        AudioClip[] instruction = new AudioClip[1] { sounds[6] };
        StartCoroutine(audioController.playAudio(instruction));

        while (waitTime < instruction[0].length)
        {
            waitTime += Time.deltaTime;
            yield return null;
        }

        selectNumber();
    }

    //Definition of Methods
    public void selectNumber()
    {
        int RandomNumber = Random.Range(11, 20);

        while (numberToEvaluate == RandomNumber)
        {
            RandomNumber = Random.Range(11, 20);
        }

        numberToEvaluate = RandomNumber;

        //Call the methods to show te UI
        showNumbers();
        showButtons();
    }

    public void showNumbers()
    {
        txtR1.text = (numberToEvaluate - 2).ToString();
        txtR2.text = (numberToEvaluate - 1).ToString();
        txtR3.text = "";
        txtR4.text = (numberToEvaluate + 1).ToString();
        txtR5.text = (numberToEvaluate + 2).ToString();
    }

    public void showButtons()
    {
        int randomButton = Random.Range(1, 3);

        switch (randomButton)
        {
            case 1:
                btn1.text = numberToEvaluate.ToString();
                btn2.text = (numberToEvaluate + 1).ToString();
                btn3.text = (numberToEvaluate - 1).ToString();
                break;
            case 2:
                btn1.text = (numberToEvaluate - 1).ToString();
                btn2.text = numberToEvaluate.ToString();
                btn3.text = (numberToEvaluate + 1).ToString();
                break;
            case 3:
                btn1.text = (numberToEvaluate - 1).ToString();
                btn2.text = (numberToEvaluate + 1).ToString();
                btn3.text = numberToEvaluate.ToString();
                break;
        }
    }

    public void correctAnswer()
    {
        hideAnswerButtons();

        AudioClip[] audios = new AudioClip[3];

        int randomAudio = Random.Range(0, 2);
        audios[2] = sounds[randomAudio];
        audios[0] = numbersSounds[answer - 11];
        audios[1] = sounds[7];

        StartCoroutine(audioController.playAudio(audios));

        StartCoroutine(animationController.PlayGoodAnimation(carrito));
    }

    public void wrongAnswer()
    {
        hideAnswerButtons();

        AudioClip[] audios = new AudioClip[3];

        int randomAudio = Random.Range(3, 5);
        audios[2] = sounds[randomAudio];
        audios[0] = numbersSounds[answer - 11];
        audios[1] = sounds[8];

        StartCoroutine(audioController.playAudio(audios));

        StartCoroutine(animationController.PlayBadAnimation(carrito));
    }

    private void hideAnswerButtons()
    {
        mainBtn1.gameObject.SetActive(false);
        mainBtn2.gameObject.SetActive(false);
        mainBtn3.gameObject.SetActive(false);
    }

    public void showAnswerButtons()
    {
        mainBtn1.gameObject.SetActive(true);
        mainBtn2.gameObject.SetActive(true);
        mainBtn3.gameObject.SetActive(true);
    }
}
