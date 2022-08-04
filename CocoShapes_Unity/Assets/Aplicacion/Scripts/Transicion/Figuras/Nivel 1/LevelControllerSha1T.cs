using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerSha1T : MonoBehaviour
{
    //Variables of Gameobjects and Arrays
    public Sprite[] spritesShapes = new Sprite[5];
    private GameObject[] shapes = new GameObject[7];
    public AudioClip[] audioClips = new AudioClip[11]; //GM Circle, GM Square, GM Triangle, GM Rectangle, GM Star, Good Job, Keep Trying, Instruction 1, Instruction 2

    private GameObject coco;
    private GameObject arrowError;
    private GameObject apple;

    private AudioController audioController;
    private AnimationControllerSha1T animationController;

    //Logic Variables
    private string[] sequenceOfShapes = new string[5];
    private string answer;

    private int correctAnswers;
    private int wrongAnswers;

    //Variables to send data to database
    private DatabaseController database;
    private float gameTotalTime = 0f;
    private string subject = "Shapes";
    private int level;

    //Variables of Game Finished
    private bool gameFinished = false;
    public GameObject panelGameFinished;

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        arr[index] = arr[arr.Length - 1];
        Array.Resize(ref arr, arr.Length - 1);
    }

    private IEnumerator StartGame()
    {
        float recorredTime = 0;

        AudioClip[] audios = new AudioClip[2] { audioClips[7], audioClips[8] };
        StartCoroutine(audioController.playAudio(audios));

        for (int i = 0; i < shapes.Length; i++)
        {
            int random = UnityEngine.Random.Range(0, spritesShapes.Length);

            switch (i)
            {
                case 0:
                    shapes[i].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    RemoveAt(ref spritesShapes, random);
                    break;
                case 1:
                    shapes[i].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    RemoveAt(ref spritesShapes, random);
                    break;
                case 2:
                    shapes[i].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    shapes[i + 1].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    RemoveAt(ref spritesShapes, random);
                    break;
                case 3:
                    break;
                case 4:
                    shapes[i].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    RemoveAt(ref spritesShapes, random);
                    break;
                case 5:
                    shapes[i].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    shapes[i + 1].GetComponent<SpriteRenderer>().sprite = spritesShapes[random];
                    RemoveAt(ref spritesShapes, random);
                    break;
                case 6:
                    break;
            }
        }

        assignResponses();

        while (recorredTime < 12f)
        {
            recorredTime += Time.deltaTime;
            yield return null;
        }

        playNextShape();
    }

    private void assignResponses()
    {
        string shape;
        for (int i = 0; i < shapes.Length; i++)
        {
            switch (i)
            {
                case 0:
                    shape = shapes[i].GetComponent<SpriteRenderer>().sprite.name;
                    sequenceOfShapes[i] = shape;
                    break;
                case 1:
                    shape = shapes[i].GetComponent<SpriteRenderer>().sprite.name;
                    sequenceOfShapes[i] = shape;
                    break;
                case 2:
                    shape = shapes[i].GetComponent<SpriteRenderer>().sprite.name;
                    sequenceOfShapes[i] = shape;
                    break;
                case 3:
                    break;
                case 4:
                    shape = shapes[i].GetComponent<SpriteRenderer>().sprite.name;
                    sequenceOfShapes[i - 1] = shape;
                    break;
                case 5:
                    shape = shapes[i].GetComponent<SpriteRenderer>().sprite.name;
                    sequenceOfShapes[i - 1] = shape;
                    break;
                case 6:
                    break;
            }
        }
    }

    private void playNextShape()
    {
        string shape = sequenceOfShapes[0];
        AudioClip[] audios = new AudioClip[1];

        switch (shape)
        {
            case "Circle":
                audios[0] = audioClips[0];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "Square":
                audios[0] = audioClips[1];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "Triangle":
                audios[0] = audioClips[2];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "Rectangle":
                audios[0] = audioClips[3];
                StartCoroutine(audioController.playAudio(audios));
                break;
            case "Star":
                audios[0] = audioClips[4];
                StartCoroutine(audioController.playAudio(audios));
                break;
        }
    }

    private IEnumerator checkAnswer()
    {
        if (answer == sequenceOfShapes[0])
        {
            if (sequenceOfShapes.Length > 1)
            {
                sequenceOfShapes = sequenceOfShapes.Skip(1).ToArray();
            }

            float recorredTime = 0;

            AudioClip[] audioToPlay = new AudioClip[2] { audioClips[10], audioClips[5] };
            StartCoroutine(audioController.playAudio(audioToPlay));

            while (recorredTime < audioToPlay[0].length + audioToPlay[1].length)
            {
                recorredTime += Time.deltaTime;
                yield return null;
            }
            {
                recorredTime += Time.deltaTime;
                yield return null;
            }

            correctAnswers++;

            switch (correctAnswers)
            {
                case 1:
                    StartCoroutine(animationController.MoveCoco(coco, shapes[0]));
                    shapes[0].SetActive(false);
                    break;
                case 2:
                    StartCoroutine(animationController.MoveCoco(coco, shapes[1]));
                    shapes[1].SetActive(false);
                    break;
                case 3:
                    StartCoroutine(animationController.MoveCoco(coco, shapes[2]));
                    shapes[2].SetActive(false);
                    shapes[3].SetActive(false);
                    break;
                case 4:
                    StartCoroutine(animationController.MoveCoco(coco, shapes[4]));
                    shapes[4].SetActive(false);
                    break;
                case 5:
                    StartCoroutine(animationController.MoveCoco(coco, shapes[5]));
                    shapes[5].SetActive(false);
                    shapes[6].SetActive(false);
                    break;
            }

            if (sequenceOfShapes.Length > 0)
            {
                playNextShape();
            }
            else
            {
                AudioClip[] audios = new AudioClip[1] { audioClips[5] };
                StartCoroutine(audioController.playAudio(audios));
            }
        }
        else
        {
            wrongAnswers++;
            AudioClip[] audioToPlay = new AudioClip[2] { audioClips[9], audioClips[6] };
            StartCoroutine(audioController.playAudio(audioToPlay));

            arrowError.SetActive(true);

            switch (correctAnswers)
            {
                case 0:
                    arrowError.transform.position = new Vector3(shapes[0].transform.position.x, shapes[0].transform.position.y + 2f, shapes[0].transform.position.z);
                    break;
                case 1:
                    arrowError.transform.position = new Vector3(shapes[1].transform.position.x, shapes[1].transform.position.y + 2f, shapes[1].transform.position.z);
                    break;
                case 2:
                    arrowError.transform.position = new Vector3(shapes[2].transform.position.x, shapes[2].transform.position.y + 2f, shapes[2].transform.position.z);
                    break;
                case 3:
                    arrowError.transform.position = new Vector3(shapes[4].transform.position.x, shapes[4].transform.position.y + 2f, shapes[4].transform.position.z);
                    break;
                case 4:
                    arrowError.transform.position = new Vector3(shapes[5].transform.position.x, shapes[5].transform.position.y + 2f, shapes[5].transform.position.z);
                    break;
            }

            float recorredTime = 0;
            while (recorredTime < audioToPlay[0].length)
            {
                recorredTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    // private IEnumerator executeInput()
    // {
    //     StartCoroutine(audioController.playAudio(audiosToPlay));

    //     arrowError.SetActive(false);

    //     float recorredTime = 0;
    //     while (recorredTime < audiosToPlay[0].length)
    //     {
    //         recorredTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     StartCoroutine(checkAnswer());
    // }

    // Start is called before the first frame update
    void Start()
    {
        //Database
        database = GameObject.Find("Database").GetComponent<DatabaseController>();

        if (SceneManager.GetActiveScene().name == "Level1_ShaT")
        {
            level = 1;
        }
        else
        {
            level = 2;
        }

        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        animationController = GameObject.Find("AnimationController").GetComponent<AnimationControllerSha1T>();

        coco = GameObject.Find("Coco");

        arrowError = GameObject.Find("Arrow");
        arrowError.SetActive(false);

        apple = GameObject.Find("Apple");

        for (int i = 1; i < shapes.Length + 1; i++)
        {
            shapes[i - 1] = GameObject.Find("Figura" + (i));
        }

        correctAnswers = 0;
        wrongAnswers = 0;

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        //Add Time to total Time
        gameTotalTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            answer = "Circle";
            //AudioClip[] audios = new AudioClip[1] { audioClips[0] };
            //StartCoroutine(executeInput(audios));

            StartCoroutine(checkAnswer());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            answer = "Square";
            //AudioClip[] audios = new AudioClip[1] { audioClips[1] };
            //StartCoroutine(executeInput(audios));

            StartCoroutine(checkAnswer());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            answer = "Triangle";
            //AudioClip[] audios = new AudioClip[1] { audioClips[2] };
            //StartCoroutine(executeInput(audios));

            StartCoroutine(checkAnswer());
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            answer = "Rectangle";
            //AudioClip[] audios = new AudioClip[1] { audioClips[3] };
            //StartCoroutine(executeInput(audios));

            StartCoroutine(checkAnswer());
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            answer = "Star";
            //AudioClip[] audios = new AudioClip[1] { audioClips[4] };
            //StartCoroutine(executeInput(audios));

            StartCoroutine(checkAnswer());
        }

        if (correctAnswers == 5 && gameFinished == false)
        {
            coco.transform.position = new Vector3(apple.transform.position.x, coco.transform.position.y, coco.transform.position.z);
            Animator cocoAnimator = coco.GetComponent<Animator>();
            cocoAnimator.Play("Celebración");

            gameFinished = true;
            panelGameFinished.SetActive(true);

            StartCoroutine(database.PushResult(subject, level, correctAnswers, wrongAnswers, (int)gameTotalTime));
        }
        if (wrongAnswers == 3 && gameFinished == false)
        {
            gameFinished = true;
            panelGameFinished.SetActive(true);

            StartCoroutine(database.PushResult(subject, level, correctAnswers, wrongAnswers, (int)gameTotalTime));
        }
    }
}
