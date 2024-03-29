using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level2_CouT : MonoBehaviour
{
    //public GameObject gameOver_OBJ;
    //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Contador de errores
    private int errorCount;
    //Para que no se repita
    private int n;
    //Para saber si esta la instruccion
    private int instructionPlaying;



    ///-------------------------------------------
    //PARA CANVAS



    public GameObject imgCanvas1;
    public GameObject imgCanvas2;
    public GameObject imgCanvas3;
    ///-------------------------------------------
    //PARA MOVER OBJETOS
    //Coco
    public GameObject cocoObj;


    //Animation del coco
    private Animator cocoObj_AN;


    //Audios de correcto o incorrecto, instrucciones
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource part1Audio;
    private AudioSource part2Audio;
    private AudioSource part3Audio;

    ///-------------------------
    //Botones
    public GameObject buttonOptionONE;
    public GameObject buttonOptionTWO;
    public GameObject buttonOptionTHREE;

    //Objetos que tienen los sprites
    public GameObject eightObj;
    public GameObject nineObj;
    public GameObject tenObj;
    public GameObject elevenObj;
    public GameObject twelveObj;
    public GameObject fifteenObj;
    public GameObject sixteenObj;
    public GameObject seventeenObj;

    //Spites
    private Sprite img_eightObj;
    private Sprite img_nineObj;
    private Sprite img_tenObj;
    private Sprite img_elevenObj;
    private Sprite img_twelveObj;
    private Sprite img_fifteenObj;
    private Sprite img_sixteenObj;
    private Sprite img_seventeenObj;


    //Audio de incorrecto
    private AudioSource[] incorrectSounds;
    public GameObject incorrect_Obj;
    private AudioSource incorrectAudio;
    private AudioSource incorrectAudioSound;

    //Audio de correcto
    private AudioSource[] correctSounds;
    public GameObject correct_Obj;
    private AudioSource correctAudio;
    private AudioSource correctAudioSound;

    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Count";
    private int levelBD = 2;

    public GameObject panelGameFinished;
    private float totalGameTime;

    // Start is called before the first frame update
    void Start()
    {
        //Audios
        sounds = obj_Audio.GetComponents<AudioSource>();
        part1Audio = sounds[0];
        part2Audio = sounds[1];
        part3Audio = sounds[2];


        //Audio de incorrecto
        incorrectSounds = incorrect_Obj.GetComponents<AudioSource>();
        incorrectAudioSound = incorrectSounds[3];

        //Audio de correcto
        correctSounds = correct_Obj.GetComponents<AudioSource>();
        correctAudioSound = correctSounds[5];


        //Para obtener las animaciones
        cocoObj_AN = cocoObj.gameObject.GetComponent<Animator>();

        //Obtener los sprites
        img_eightObj = eightObj.GetComponent<Image>().sprite;
        img_nineObj = nineObj.GetComponent<Image>().sprite;
        img_tenObj = tenObj.GetComponent<Image>().sprite;
        img_elevenObj = elevenObj.GetComponent<Image>().sprite;
        img_twelveObj = twelveObj.GetComponent<Image>().sprite;
        img_fifteenObj = fifteenObj.GetComponent<Image>().sprite;
        img_sixteenObj = sixteenObj.GetComponent<Image>().sprite;
        img_seventeenObj = seventeenObj.GetComponent<Image>().sprite;


        //Otras variables

        level=1;
        instructionPlaying =1;
        
        errorCount=0;

        //Defino las primeras opciones de respuesta
        buttonOptionONE.GetComponent<Image>().sprite = img_tenObj;
        buttonOptionTWO.GetComponent<Image>().sprite = img_nineObj;
        buttonOptionTHREE.GetComponent<Image>().sprite = img_eightObj;

        //Quito los otros rieles
        imgCanvas2.SetActive(false);
        imgCanvas3.SetActive(false);
        //Inicia la primera instrucción

        //Luego la parte 1
        part1Audio.Play();
        n = 1;

        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }

    // Update is called once per frame
    void Update(){   
        
        totalGameTime += Time.deltaTime;
        //Debo ver si se están reproduciendo las animaciones
        //Si empezó la de la parte 2
        if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoPart2Start")){
            if(n==1){
                changeImgandButton(imgCanvas1, imgCanvas2, part2Audio, img_elevenObj, img_twelveObj, img_tenObj);
            }
            
        }
        //Si empezó la parte 3
         if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoPart3Start")){
            if(n==1){
                changeImgandButton( imgCanvas2, imgCanvas3, part3Audio, img_fifteenObj, img_sixteenObj, img_seventeenObj);
            }
        }
        //Si ya se fue de la parte 3
         if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoPart3Leave")){
            if(n==1){
                Debug.Log("CAMBIO PAGINA");
                n=2;
                //Para que se muestre la pantalla de fin del juego:
                StartCoroutine(database.PushResult(subject, levelBD, (level - 1), errorCount, (int)totalGameTime));
                panelGameFinished.SetActive(true);
                //gameOver_OBJ.SetActive(true);
            }
            
        }

        //Si se está reproduciendo una instrucción el estudiante no puede
        //presionar los botones
        if(part1Audio.isPlaying || part2Audio.isPlaying ||part3Audio.isPlaying){
            Debug.Log("Se esta reproduciendo");
            instructionPlaying =1;
        }else if(!part1Audio.isPlaying || !part2Audio.isPlaying ||!part3Audio.isPlaying){
            instructionPlaying =0;
            Debug.Log("Ya no se esta reproduciendo");
        }
    }

    //Cambiar la imagen de fondo y botones
    public void changeImgandButton(GameObject canvasF, GameObject canvasT, AudioSource instruction, Sprite img1, Sprite img2, Sprite img3 ){
        n=2;
        Debug.Log("ENTRA 1");
        //Cambio la imagen de fondo
        canvasF.SetActive(false);
        canvasT.SetActive(true);
        Debug.Log("se quito canvas");
        //Activo la instrucción
        instruction.PlayDelayed(correctAudio.clip.length);
        //Activo los botones
        buttonOptionONE.SetActive(true);
        buttonOptionTWO.SetActive(true);
        buttonOptionTHREE.SetActive(true);
        //Cambio los botones
        buttonOptionONE.GetComponent<Image>().sprite=img1;
        buttonOptionTWO.GetComponent<Image>().sprite=img2;
        buttonOptionTHREE.GetComponent<Image>().sprite=img3;
    }

    //Boton que se clickea
    //Recibe el botón que le dió click
    public void buttonClick(int option){
        //SOLAMENTE SE ACTIVAN MÉTODOS SI NO SE ESTÁ REPRODUCIENDO LA INSTRUCCIÓN
        if(instructionPlaying ==0){
                //Siempre se mira en qué nivel está
            //Si el nivel es el primero, la opción correcta es la 2
            if(level==1){
                if(option==2){
                    correctClick(part1Audio, "cocoPart1Leave");
                }else{
                    incorrectClick(part1Audio);
                }
            }
            //Si el nivel es el segundo, la opción correcta en la 1
            else if(level==2){
                if(option==1){
                    correctClick(part2Audio, "cocoPart2Leave");
                }else{
                    incorrectClick(part2Audio);
                }
            }
            //Si es el tercer nivel la opción correcta es la 3
            else if(level==3){
                if(option==3){
                    correctClick(part3Audio, "cocoPart3Leave");
                }else{
                    incorrectClick(part3Audio);
                }

            }

            //Si ya llegó a los 3 errores
            if (errorCount==3){
                Debug.Log("Se acabaron los intentos");
                 
            //Para que se muestre la pantalla de fin del juego:
            StartCoroutine(database.PushResult(subject, levelBD, (level - 1), errorCount, (int)totalGameTime));
            panelGameFinished.SetActive(true);
            //gameOver_OBJ.SetActive(true);
            }
        }else{
            Debug.Log("Todavia no le puedes undir :D");

        }
        
    }

    //Opcion correcta
    private void correctClick(AudioSource instruction, string animationCoco){
        //devuelvo a la n
        n=1;
        //Desactivo el audio de la instrucción
        instruction.Stop();
        //Activo audio correcto
        correctAudio = correctSounds[ Random.Range(0, 5)];
        correctAudio.Play();
        correctAudioSound.Play();
        //Activo la animación de Coco
        cocoObj_AN.Play(animationCoco);
        //Aumento el nivel
        level++;
        //Desactivo los botones
        buttonOptionONE.SetActive(false);
        buttonOptionTWO.SetActive(false);
        buttonOptionTHREE.SetActive(false);
        
        //reinicio el contador de errores
        errorCount=0;
    }

    //Opcion incorrecta
    private void incorrectClick(AudioSource instruction){
        //Activo audio
        incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
        incorrectAudio.Play();
        incorrectAudioSound.Play();
        //Repito la instrucción
        instruction.PlayDelayed(incorrectAudio.clip.length);
        //Aumento contador de errores
        errorCount++;
    }
}
