using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level2_CouJ : MonoBehaviour
{
    //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Contador de errores
    private int errorCount;

    //Para saber si fue correcto o incorrecto
    private int decision;

    //Numero para que no se quede en el update
    private int notRepeat;

///-------------------------------------------
    //PARA CANVAS
    //Canvas en donde se va a cambiar todo
    public GameObject canvas;
    
    //Imagenes
    //Parte 1
        //Parte 1_Incorrecto
    public GameObject imgPart1_In;
            //Sprite de parte 1 incorrecto
    private Sprite imgPart1_In_SP;  


    //Parte 2
        //Parte 2 instrucción
    public  GameObject imgPart2;
            //Sprite de parte 2
    private Sprite imgPart2_SP;  

        //Parte 2 Incorrecto
    public GameObject imgPart2_In;
            //Sprite de parte 2 incorrecto
    private Sprite imgPart2_In_SP;


    //Parte 3
        //Parte 3 instrucción
    public GameObject imgPart3;
            //Sprite de parte 3
    private Sprite imgPart3_SP;

        //Parte 3 Incorrecto
    public GameObject imgPart3_In;
            //Sprite de parte 3 incorrecto
    private Sprite imgPart3_In_SP;


    ///-------------------------------------------
    //PARA MOVER OBJETOS
        //Osito
    public GameObject cocoObj;


        //Animation del osito
    private Animator cocoObj_AN;



    //Audios de correcto o incorrecto, numeros y caminos
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource correctAudio;
    private AudioSource incorrectAudio;

    private AudioSource twoAudio;
    private AudioSource threeAudio;
    private AudioSource fiveAudio;
    private AudioSource sixAudio;
    private AudioSource nineAudio;
    private AudioSource tenAudio;
    private AudioSource heartAudio;
    private AudioSource starAudio;


    private AudioSource instruction;

    private AudioSource audioNumberFeedback;
    // Start is called before the first frame update
    void Start()
    {
        //Para obtener todos los sprites
        imgPart1_In_SP= imgPart1_In.GetComponent<Image>().sprite;
        imgPart2_SP= imgPart2.GetComponent<Image>().sprite;
        imgPart2_In_SP= imgPart2_In.GetComponent<Image>().sprite;
        imgPart3_SP= imgPart3.GetComponent<Image>().sprite;
        imgPart3_In_SP= imgPart3_In.GetComponent<Image>().sprite;

         //Audios
        sounds= obj_Audio.GetComponents<AudioSource>();
        correctAudio= sounds[0];
        incorrectAudio= sounds[1];
        twoAudio= sounds[2];
        threeAudio= sounds[3];
        fiveAudio= sounds[4];
        sixAudio= sounds[5];
        nineAudio= sounds[6];
        tenAudio= sounds[7];
        starAudio= sounds[8];
        heartAudio= sounds[9];
        instruction= sounds[10];

        audioNumberFeedback= new AudioSource();
        
        //Para obtener las animaciones
        cocoObj_AN = cocoObj.gameObject.GetComponent<Animator>();


        //Otras variables
        level=1;
        
        errorCount=0;

        //Ni correcto ni verdadero
        decision=3;

        //Para not repeat
        notRepeat =0;

        //Activo la instruccion
        instruction.Play();
      
    }

    // Update is called once per frame
    void Update()
    {
        //Para cambiar imagen cuando finaliza la animacion
        if(cocoObj_AN.GetCurrentAnimatorStateInfo(0).IsName("cocoAnim_vanish") && notRepeat ==0){
            //Cambio la imagen dependiendo del nivel
            
            changeImg(level,decision);
            
         //Subo nivel
        level++;
        notRepeat=1;
       
        }

        
    }

    //Botón que se llama cuando se da click al boton correcto
    public void buttonCorrectClick(){
        //Quito errores
        errorCount=0;
        //Para el not repeat
        notRepeat =0;  
        //Indico que fue correcto
        decision = 1;
        //Animacion
            animations(level,decision);
        //Indico la retroalimentacion
            audioNumberFeedback.Play();
            
        
        //Activo audio
            correctAudio.PlayDelayed(audioNumberFeedback.clip.length);
          
        
    }

    
    //Botón que se llama cuando se da click al boton incorrecto
    public void buttonIncorrectClick(){
        //Para el not repeat
             notRepeat =1;    
        //Indico que fue incorrecto
        decision = 0;
        //Se debe verificar en qué nivel se encuentra
        

            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 3 errores
             if (errorCount==2){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
            }

            //Animacion
            animations(level,decision);
            //Cambio la imagen dependiendo del nivel
            changeImg(level,decision);

            //Indico la retroalimentacion
            audioNumberFeedback.Play();

            //Activo audio
            incorrectAudio.PlayDelayed(audioNumberFeedback.clip.length);

            //Vuelvo a activar la instrucción
            instruction.PlayDelayed(incorrectAudio.clip.length);
            
           
    }

    

    //Método que cambia la imagen de fondo
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
    public void animations(int levelCI, int num){

        if(levelCI==1){
            if(num==1){
                
                //Si es correcto en el nivel 1
                //Muevo al osito con la animacion correcta
                cocoObj_AN.Play("cocoAnim_part1Correct");
                //Las casillas correctas son 2
                audioNumberFeedback = twoAudio;
               
            }
            else if(num==0){
              
                //Si es incorrecto en el nivel 1
                //Muevo al osito con la animacion incorrecta
                cocoObj_AN.Play("cocoAnim_part1Incorrect");
                //Las casillas incorrectas son 3
                audioNumberFeedback = threeAudio;
                
            }
            
        }
        else if (levelCI==2){
            if(num==1){
                //Si es correcto en el nivel 2
                //Muevo al osito con la animacion correcta
                cocoObj_AN.Play("cocoAnim_part2Correct");
                //Las casillas correctas son 5
                audioNumberFeedback = fiveAudio;
              
                
            }
            else if(num==0){
                
                 //Si es incorrecto en el nivel 2
                //Muevo al osito con la animacion incorrecta
                cocoObj_AN.Play("cocoAnim_part2Incorrect");
                //Las casillas incorrectas son 6
                audioNumberFeedback = sixAudio;
                
            }
        }
        else if(levelCI==3){
            if(num==1){
                //Si es correcto en el nivel 3
                //Muevo al osito con la animacion correcta
                cocoObj_AN.Play("cocoAnim_part3Correct");
                //Las casillas correctas son 9
                audioNumberFeedback = nineAudio;
                
                Debug.Log("Cambio de pagina");
            }
            else if(num==0){
                //Si es incorrecto en el nivel 3
                //Muevo al osito con la animacion incorrecta
                cocoObj_AN.Play("cocoAnim_part3Incorrect");
                //Las casillas incorrectas son 10
                audioNumberFeedback = tenAudio;
               
            }
        }
        else{
            Debug.Log("Cambio pagina");
        }

    }

    public void changeImg(int levelCI, int num){

        if(levelCI==1){
            if(num==1){
                canvas.GetComponent<Image>().sprite=imgPart2_SP;
                 //Activo la instruccion
                instruction.Play();
               
            }
            else if(num==0){
               
                canvas.GetComponent<Image>().sprite=imgPart1_In_SP;
            }
            
        }
        else if (levelCI==2){
            if(num==1){
                canvas.GetComponent<Image>().sprite=imgPart3_SP;
                 //Activo la instruccion
                    instruction.Play();
            }
            else if(num==0){
                
                canvas.GetComponent<Image>().sprite=imgPart2_In_SP;
            }
        }
        else if(levelCI==3){
            if(num==1){
                Debug.Log("Cambio de pagina");
            }
            else if(num==0){
                
                canvas.GetComponent<Image>().sprite=imgPart3_In_SP;
            }
        }
        else{
            Debug.Log("Cambio pagina");
        }


    }

}
