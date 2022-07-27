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
    //PARA imgPart1
    //imgPart1 en donde se va a cambiar todo
    public GameObject imgPart1;
    
    //Imagenes
    //Parte 1


    //Parte 2
        //Parte 2 instrucción
    public  GameObject imgPart2;
         



    //Parte 3
        //Parte 3 instrucción
    public GameObject imgPart3;



    ///-------------------------------------------
    //PARA MOVER OBJETOS
        //Osito
    public GameObject cocoObj;


        //Animation del osito
    private Animator cocoObj_AN;



    //Audios de correcto o incorrecto, numeros y caminos
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource twoAudio;
    private AudioSource threeAudio;
    private AudioSource fiveAudio;
    private AudioSource sixAudio;
    private AudioSource nineAudio;
    private AudioSource tenAudio;



    private AudioSource instruction;

    private AudioSource audioNumberFeedback;

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
        twoAudio= sounds[0];
        threeAudio= sounds[1];
        fiveAudio= sounds[2];
        sixAudio= sounds[3];
        nineAudio= sounds[4];
        tenAudio= sounds[5];
        instruction= sounds[6];

        audioNumberFeedback= new AudioSource();

        //Audio de incorrecto
        incorrectSounds= incorrect_Obj.GetComponents<AudioSource>();

        //Audio de correcto
        correctSounds= correct_Obj.GetComponents<AudioSource>();
        
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

        //Desactivo las otras 2 imagenes
        imgPart2.SetActive(false);
        imgPart3.SetActive(false);

      
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

    //Método cuando toca un botón
    //1 es corazón, 2 es estrella
    public void buttonClick(int option){
        //Todo dependende del nivel
        //Si es el primero, la opción correcta es corazón
        //Si le dió al corazón
        //Solo es correcto en el nivel 1
        if(option==1){
            if(level==1){
                correctOption();
            }else{
                incorrectOption();
            }
        }
        //Si le dió a la estrella, es correcta en nivel 2 y 3
        else{
            //Incorrecto solo en nivel 1
            if(level==1){
                incorrectOption();
            }else{
                correctOption();
            }
        }

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
                //Desactivo la 1 activo la 2
                imgPart1.SetActive(false);
                imgPart2.SetActive(true);
                 //Activo la instruccion
                 
                instruction.PlayDelayed(correctAudio.clip.length + audioNumberFeedback.clip.length);
               
            }
            
        }
        else if (levelCI==2){
            if(num==1){
                //Activo la tercera imagen y desativo la 2
                imgPart2.SetActive(false);
                imgPart3.SetActive(true);
               
                 //Activo la instruccion
                   instruction.PlayDelayed(correctAudio.clip.length + audioNumberFeedback.clip.length);
            }
        
        }
        else if(levelCI==3){
            if(num==1){
                Debug.Log("Cambio de pagina");
            }
        }
        else{
            Debug.Log("Cambio pagina");
        }


    }

    public void correctOption(){
        //Si es correcto
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
                correctAudio = correctSounds[ Random.Range(0, 5)];
                correctAudio.PlayDelayed(audioNumberFeedback.clip.length);
    }

    public void incorrectOption(){
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
            //Activo audio
            incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
            incorrectAudio.PlayDelayed(audioNumberFeedback.clip.length);

            //Vuelvo a activar la instrucción
            instruction.PlayDelayed(incorrectAudio.clip.length + audioNumberFeedback.clip.length);
    }

}
