using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    ///-------------------------------------------
    //PARA MOVER OBJETOS
        //Balon
    public GameObject ballObj;


        //Animation del balon
    public Animator ballObj_AN;



    //Audios de correcto o incorrecto, numeros y caminos
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource correctAudio;
    private AudioSource incorrectAudio;

    private AudioSource greenAudio;
    private AudioSource orangeAudio;
    private AudioSource purpleAudio;

    private AudioSource audioColor;

    // Start is called before the first frame update
    void Start()
    {

         //Audios
        sounds= obj_Audio.GetComponents<AudioSource>();
        correctAudio= sounds[0];
        incorrectAudio= sounds[1];
        greenAudio= sounds[2];
        orangeAudio= sounds[3];
        purpleAudio= sounds[4];

        audioColor= new AudioSource();
        
        //Para obtener las animaciones
        ballObj_AN = ballObj.gameObject.GetComponent<Animator>();


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
            //Activo audio
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
            incorrectAudio.Play();

            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 2 errores
             if (errorCount==2){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
            }
            //Activamos sonido de color otra vez
            audioColor.PlayDelayed(incorrectAudio.clip.length);

            //NO Hago gol
            throwBall(reqColor,0);
           
        }

        
    }
        //Método que me permite saber el color solicitado
    public void requestedColor(){
       
       //Genero el número aleatorio

        randomNumber = Random.Range(0, 3);
        
        //Este for me permite saber si ya salió ese número
        for (int i = 0; i < colorsArrayCh.Length; i++)
        {
            //Si ya salió
            if(randomNumber == colorsArrayCh[i] ){
                //Vuelvalo a generar
                randomNumber = Random.Range(0, 3);
                Debug.Log("Volvio a generar" + randomNumber);
            }
        }
        //Añado a mi array el número que acabo de sacar
        colorsArrayCh[iteration]= randomNumber;
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

        }
        //si es verde
        else if(reqColor == "green"){
           
            audioColor = greenAudio;
        }
        //si es purpura
        else if(reqColor == "purple"){
            audioColor = purpleAudio;
        }

            //Si es otro nivel diferente al 1 hay que esperar a que pase el audio
        if( level!=1){
            audioColor.PlayDelayed(correctAudio.clip.length);
        }else{
            audioColor.Play();
        }
        
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

