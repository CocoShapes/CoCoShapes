using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level2_CouT : MonoBehaviour
{
     //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Contador de errores
    private int errorCount;
    //Para que no se repita
    private int n;



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

    //Audio de correcto
    private AudioSource[] correctSounds;
    public GameObject correct_Obj;
    private AudioSource correctAudio;

    // Start is called before the first frame update
    void Start()
    {
         //Audios
        sounds= obj_Audio.GetComponents<AudioSource>();
        part1Audio = sounds[0];
        part2Audio = sounds[1];
        part3Audio = sounds[2];

        
        //Audio de incorrecto
        incorrectSounds= incorrect_Obj.GetComponents<AudioSource>();

        //Audio de correcto
        correctSounds= correct_Obj.GetComponents<AudioSource>();
        
        //Para obtener las animaciones
        cocoObj_AN= cocoObj.gameObject.GetComponent<Animator>();

        //Obtener los sprites
        img_eightObj=eightObj.GetComponent<Image>().sprite;
        img_nineObj=nineObj.GetComponent<Image>().sprite;
        img_tenObj=tenObj.GetComponent<Image>().sprite;
        img_elevenObj=elevenObj.GetComponent<Image>().sprite;
        img_twelveObj=twelveObj.GetComponent<Image>().sprite;
        img_fifteenObj=fifteenObj.GetComponent<Image>().sprite;
        img_sixteenObj=sixteenObj.GetComponent<Image>().sprite;
        img_seventeenObj=seventeenObj.GetComponent<Image>().sprite;

        
        //Otras variables
        level=1;
        
        errorCount=0;

        //Defino las primeras opciones de respuesta
        buttonOptionONE.GetComponent<Image>().sprite=img_tenObj;
        buttonOptionTWO.GetComponent<Image>().sprite=img_nineObj;
        buttonOptionTHREE.GetComponent<Image>().sprite=img_eightObj;

        //Quito los otros rieles
        imgCanvas2.SetActive(false);
        imgCanvas3.SetActive(false);
        //Inicia la primera instrucción
        
        //Luego la parte 1
        part1Audio.Play();
        n=1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debo ver si se están reproduciendo las animaciones
        //Si empezó la de la parte 2
        if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoPart2Start") && n==1){
            //Cambio la imagen de fondo
            imgCanvas1.SetActive(false);
            imgCanvas2.SetActive(true);
            
            //Activo la instrucción
            part2Audio.PlayDelayed(correctAudio.clip.length);
            //Activo los botones
            buttonOptionONE.SetActive(true);
            buttonOptionTWO.SetActive(true);
            buttonOptionTHREE.SetActive(true);
            //Cambio los botones
            buttonOptionONE.GetComponent<Image>().sprite=img_elevenObj;
            buttonOptionTWO.GetComponent<Image>().sprite=img_tenObj;
            buttonOptionTHREE.GetComponent<Image>().sprite=img_twelveObj;
            n=2;
        }
        //Si empezó la parte 3
        else if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoPart3Start")&& n==1){
            //Cambio la imagen de fondo
            imgCanvas2.SetActive(false);
            imgCanvas3.SetActive(true);
            //Activo la instrucción
            part3Audio.PlayDelayed(correctAudio.clip.length);
            //Activo los botones
            buttonOptionONE.SetActive(true);
            buttonOptionTWO.SetActive(true);
            buttonOptionTHREE.SetActive(true);
            //Cambio los botones
            buttonOptionONE.GetComponent<Image>().sprite=img_fifteenObj;
            buttonOptionTWO.GetComponent<Image>().sprite=img_sixteenObj;
            buttonOptionTHREE.GetComponent<Image>().sprite=img_seventeenObj;
            n=2;
        }
        //Si ya se fue de la parte 3
        else if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoPart3Leave")&& n==1){
            Debug.Log("CAMBIO PAGINA");
            n=2;
        }
    }

    //Boton que se clickea
    //Recibe el botón que le dió click
    public void buttonClick(int option){
        
        //Siempre se mira en qué nivel está
        //Si el nivel es el primero, la opción correcta es la 2
        if(level==1){
            if(option==2){
                //Activo audio correcto
                correctAudio = correctSounds[ Random.Range(0, 5)];
                correctAudio.Play();
                //Activo la animación de Coco
                cocoObj_AN.Play("cocoPart1Leave");
                //Aumento el nivel
                level++;
                //Desactivo los botones
                buttonOptionONE.SetActive(false);
                buttonOptionTWO.SetActive(false);
                buttonOptionTHREE.SetActive(false);
                //devuelvo a la n
                n=1;
                //reinicio el contador de errores
                errorCount=0;
            }else{
                //Activo audio
                incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
                incorrectAudio.Play();
                //Repito la instrucción
                part1Audio.PlayDelayed(incorrectAudio.clip.length);
                //Aumento contador de errores
                errorCount++;
            }
        }
        //Si el nivel es el segundo, la opción correcta en la 1
        else if(level==2){
            if(option==1){
                //Activo audio correcto
                 correctAudio = correctSounds[ Random.Range(0, 5)];
                correctAudio.Play();
                //Activo la animación de Coco
                cocoObj_AN.Play("cocoPart2Leave");
                //Aumento el nivel
                level++;
                //Desactivo los botones
                buttonOptionONE.SetActive(false);
                buttonOptionTWO.SetActive(false);
                buttonOptionTHREE.SetActive(false);
                //devuelvo a la n
                n=1;
                //reinicio el contador de errores
                errorCount=0;
                
            }else{
                //Activo audio
                incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
                incorrectAudio.Play();
                //Repito la instrucción
                part2Audio.PlayDelayed(incorrectAudio.clip.length);
                //Aumento contador de errores
                errorCount++;
            }
        }
        //Si es el tercer nivel la opción correcta es la 3
        else if(level==3){
            if(option==3){
                //Activo audio correcto
                correctAudio = correctSounds[ Random.Range(0, 5)];
                correctAudio.Play();
                //Activo la animación de Coco
                cocoObj_AN.Play("cocoPart3Leave");
                //Aumento el nivel
                level++;
                //Desactivo los botones
                buttonOptionONE.SetActive(false);
                buttonOptionTWO.SetActive(false);
                buttonOptionTHREE.SetActive(false);
                //devuelvo a la n
                n=1;
                //reinicio el contador de errores
                errorCount=0;
            }else{
                //Activo audio
                 incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
                  incorrectAudio.Play();
                //Repito la instrucción
                part3Audio.PlayDelayed(incorrectAudio.clip.length);
                //Aumento contador de errores
                errorCount++;
            }
        }

        //Si ya llegó a los 3 errores
        if (errorCount==3){
            Debug.Log("Se acabaron los intentos");
        }
    }
}
