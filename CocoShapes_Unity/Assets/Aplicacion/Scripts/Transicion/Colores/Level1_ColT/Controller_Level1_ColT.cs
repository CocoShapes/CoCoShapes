using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level1_ColT : MonoBehaviour
{
     //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Contador de errores
    private int errorCount;

    //Numero aleatorio
    private int randomNumber; 

    //Arreglo de colores
    private string[] colorsArray;
    //Arreglo de numeros que ya salieron
    private int[] colorsArrayCh;
    //Numero que me permite saber el numero de iteraciones
    private int iteration;

    //Variable que llega del arduino con el nombre del color presionado
    public string pressColor; 

    //Variable del color solicitado
    public string reqColor;

   
///-------------------------------------------
    //PARA CANVAS

    //Imagenes de instrucciones
    public GameObject instructionsRB_OBJ;
    public GameObject instructionsRY_OBJ;
    public GameObject instructionsYB_OBJ;

    //Animacion de las instrucciones
    private Animator instructionsRB_AN;
    private Animator instructionsRY_AN;
    private Animator instructionsYB_AN;

    //Animacion de instruccion
    private Animator instruction_AN;

    ///-------------------------------------------
    //PARA MOVER OBJETOS
        //Balon
    public GameObject ballObj;


        //Animation del balon
    private Animator ballObj_AN;



    //Audios de correcto o incorrecto, numeros y caminos
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource greenAudio;
    private AudioSource orangeAudio;
    private AudioSource purpleAudio;

    private AudioSource audioColor;

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
        
        greenAudio= sounds[0];
        orangeAudio= sounds[1];
        purpleAudio= sounds[2];

        //Audio de incorrecto
        incorrectSounds= incorrect_Obj.GetComponents<AudioSource>();

        //Audio de correcto
        correctSounds= correct_Obj.GetComponents<AudioSource>();

        correctAudio = new AudioSource();
        incorrectAudio = new AudioSource();
        audioColor= new AudioSource();
        
        //Para obtener las animaciones
        ballObj_AN = ballObj.gameObject.GetComponent<Animator>();
        //De las instrucciones
        instructionsRB_AN = instructionsRB_OBJ.GetComponent<Animator>();
        instructionsRY_AN = instructionsRY_OBJ.GetComponent<Animator>();
        instructionsYB_AN = instructionsYB_OBJ.GetComponent<Animator>();
        instruction_AN = new Animator();

        //Otras variables
        level=1;
        randomNumber=0;
        errorCount=0;
        iteration=0;


        colorsArray = new string []{ "green", "purple", "orange"};
        //Colocar numeros muy altos que nunca saldran
        colorsArrayCh = new int []{ 10, 10, 10};

        //Aplico el método que me genera el color solicitado
        requestedColor();

        //Aqui se realizaria lo de la conexión del Arduino
        pressColor="";

    }

    // Update is called once per frame
    void Update()
    {
        //Aqui se realizaria lo de la conexión del Arduino
        //Es necesario verificar que haya presionado alguno de los botones
        if(pressColor=="red"||pressColor=="yellow"||pressColor=="blue"
        ||pressColor=="orange"||pressColor=="green"||pressColor=="purple"
        ||pressColor=="black"||pressColor=="white"){
                colorButtonPress();
        //Necesario indicar que ya no se esta presionando
        pressColor="";
        }
    }

    //Método que se invoca cuando se presiona un boton de color
    public void colorButtonPress(){
        //Si el estudiante presionó el color correcto   
        if(pressColor == reqColor){
            //Activo audio correcto
             correctAudio = correctSounds[ Random.Range(0, 5)];
             correctAudio.Play();

            
            //Aumento el nivel
            level++;  
            //Reseteo el numero de errores
            errorCount=0;
             //Hago gol
            throwBall(reqColor,1);
            
            //Devuelvo el balón al inicio
            //ballObj_AN.Play("ball_start");
            if(level<4){
                //Vuelvo a generar un color solicitado
            requestedColor();
            }

           
            
        }

        //Si presiona el incorrecto
        else{
            //Activo audio
            incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
            incorrectAudio.Play();
            //NO Hago gol
            throwBall(reqColor,0);

            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 2 errores
             if (errorCount==2){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
            } else{
                    //Activamos sonido de color otra vez y la animación
            audioColor.PlayDelayed(incorrectAudio.clip.length);
            instruction_AN.Play("instruction_appear");
            }
            
            
            
           
        }

        
    }
        //Método que me permite saber el color solicitado
    public void requestedColor(){
       //Este método me permite saber si ya salió ese número
        //Recibe el arreglo, y la cantidad de numeros a generar
       colorsArrayCh[iteration]= randomGenerate(colorsArrayCh, 3);
       iteration++;

        
        //Recorremos el arreglo de colores para devolver el color solicitado
        for (int n = 0; n < colorsArray.Length; n++)
        {
            //Si encuentra la posición
            if(n == randomNumber ){
                reqColor = colorsArray[n];
               Debug.Log ("RN: " + randomNumber + "CL: " + colorsArray[n]  );
            }
        }

        //si es naranja
         if(reqColor == "orange"){
    
            audioColor = orangeAudio;
            instruction_AN =  instructionsRY_AN;
          

        }
        //si es verde
        else if(reqColor == "green"){
           
            audioColor = greenAudio;
            instruction_AN =  instructionsYB_AN;
            
        }
        //si es purpura
        else if(reqColor == "purple"){
            audioColor = purpleAudio;
            instruction_AN =  instructionsRB_AN;
            
            
        }

            //Si es otro nivel diferente al 1 hay que esperar a que pase el audio
        if( level!=1){
            audioColor.PlayDelayed(correctAudio.clip.length);
        }else{
            audioColor.Play();
        }
        instruction_AN.Play("instruction_appear");
    }

    public int randomGenerate(int [] array, int number){
         //Genero el número aleatorio
        randomNumber = Random.Range(0, number);

        for (int i = 0; i < array.Length; i++)
        {
            
            //Si ya salió
            while(randomNumber == array[i] ){
                
                //Vuelvalo a generar
                randomNumber = Random.Range(0, number);
                //Doble confirmación
                for (int o = 0; o < array.Length; o++)
                {
                    while(randomNumber == array[i] ){
                       
                        //Vuelvalo a generar
                        randomNumber = Random.Range(0, number);
                    }
                }
            }
        }
        return(randomNumber);
    }

  

     //Método que permite lanzar el balón
        //Recibe un color y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
            //Si es correcto hace gol
            //Si es incorrecto no hace gol
    public void throwBall(string color, int num){
        Debug.Log("ENTRA METODO" +  color + " "+ num);

        
            if(num==1){
                if(reqColor == "orange"){
                    
                    ballObj_AN.Play("ball_orange_throw");
                    if(level==3){
                        Debug.Log("Cambio pagina");
                    }
                }
                else if(reqColor == "green"){
                    
                    ballObj_AN.Play("ball_green_throw");
                     if(level==3){
                        Debug.Log("Cambio pagina");
                    }
                }else if(reqColor == "purple"){
                    
                    ballObj_AN.Play("ball_purple_throw");
                     if(level>3){
                        Debug.Log("Cambio pagina");
                    }
                }
                
            }
            else if (num==0){
                ballObj_AN.Play("ball_wrong_throw");
            }
         
     

    }
}
