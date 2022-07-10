using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Level2_ColT : MonoBehaviour
{
    //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Contador de errores
    private int errorCount;

    //Numero aleatorio 1
    private int randomNumber; 

    
    //Numero aleatorio 2
    private int randomNumber2; 

    //Numero que me permite saber la combinación
    private int combNumber;

    //Variables para que no vuelva a undir el mismo color
    //0 es que no lo ha undido, 1 que si
    private int yellowPress;
    private int bluePress;
    private int redPress;

    //Arreglo de colores
    private string[] colorsArray;
    //Arreglo de numeros que ya salieron
    private int[] colorsArrayCh;
     //Arreglo de numeros que ya salieron en animaciones
    private int[] animArrayCh;
    //Numero que me permite saber el numero de iteraciones
    private int iteration;
    //Numero 2 que me permite saber el numero de iteraciones
    private int iteration2;

    //Variable que llega del arduino con el nombre del color presionado
    public string pressColor; 

    //Variable del color solicitado
    public string reqColor;

    //Variables de los colores que se necesitan para realizar la combinacion
    private string reqColorComb1;
    private string reqColorComb2;

    //Arreglo de animaciones para los globos rojos
    private string[] redAnimations;
    //Arreglo de animaciones para los globos azules
    private string[] blueAnimations;
    //Arreglo de animaciones para los globos amarillos
    private string[] yellowAnimations;
    //Arreglo de animaciones erroneas
    private string[] wrongAnimations;

    //GameObjects de todos los globos con sus animaciones
    public GameObject redBallonObj1;
    public GameObject redBallonObj2;
    public GameObject redBallonObj3;
    public GameObject redBallonObj4;

    public GameObject blueBallonObj1;
    public GameObject blueBallonObj2;
    public GameObject blueBallonObj3;
    public GameObject blueBallonObj4;

    public GameObject yellowBallonObj1;
    public GameObject yellowBallonObj2;
    public GameObject yellowBallonObj3;
    public GameObject yellowBallonObj4;

    //Arreglo de animaciones de los globos
    private Animator[] redBallonObj_ARR;
    //Arreglo de globos azules
    private Animator[] blueBallonObj_ARR;
    //Arreglo de globos amarillos
    private Animator[] yellowBallonObj_ARR;
///-------------------------------------------
    //PARA CANVAS

    ///-------------------------------------------
    //PARA MOVER OBJETOS
        //dardo
    public GameObject dartObj;


        //Animation del dardo
    private Animator dartObj_AN;


    //Audios de correcto o incorrecto, numeros y caminos
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource correctAudio;
    private AudioSource incorrectAudio;

    private AudioSource redAudio;
    private AudioSource blueAudio;
    private AudioSource yellowAudio;
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
        redAudio = sounds[2];
        blueAudio = sounds[3];
        yellowAudio = sounds[4];
        greenAudio = sounds[5];
        orangeAudio = sounds[6];
        purpleAudio = sounds[7];

        audioColor= new AudioSource();
        
        //Para obtener las animaciones
        dartObj_AN= dartObj.gameObject.GetComponent<Animator>();

        //Arreglo de animaciones
        redAnimations = new string []{"dart_red_throw_1", "dart_red_throw_2", "dart_red_throw_3", "dart_red_throw_4"};
        blueAnimations = new string []{"dart_blue_throw_1", "dart_blue_throw_2", "dart_blue_throw_3", "dart_blue_throw_4"};
        yellowAnimations = new string []{"dart_yellow_throw_1", "dart_yellow_throw_2", "dart_yellow_throw_3", "dart_yellow_throw_4"};
        wrongAnimations = new string []{"dart_wrong_throw_1", "dart_wrong_throw_2", "dart_wrong_throw_3", "dart_wrong_throw_4"};

        //Arreglo de globos
        redBallonObj_ARR = new Animator []{redBallonObj1.gameObject.GetComponent<Animator>(), redBallonObj2.gameObject.GetComponent<Animator>(), redBallonObj3.gameObject.GetComponent<Animator>(), redBallonObj4.gameObject.GetComponent<Animator>()};       
        blueBallonObj_ARR = new Animator []{blueBallonObj1.gameObject.GetComponent<Animator>(), blueBallonObj2.gameObject.GetComponent<Animator>(), blueBallonObj3.gameObject.GetComponent<Animator>(), blueBallonObj4.gameObject.GetComponent<Animator>()}; 
        yellowBallonObj_ARR = new Animator []{yellowBallonObj1.gameObject.GetComponent<Animator>(), yellowBallonObj2.gameObject.GetComponent<Animator>(), yellowBallonObj3.gameObject.GetComponent<Animator>(), yellowBallonObj4.gameObject.GetComponent<Animator>()};  

        //Otras variables
        level=1;
        
        errorCount=0;
        randomNumber=0;
        randomNumber2=0;
        iteration=0;
        iteration2=0;
        combNumber =0;

        yellowPress =0;
        bluePress =0;
        redPress =0;

        colorsArray = new string []{"green", "purple", "orange"};
        //Colocar numeros muy altos que nunca saldran
        colorsArrayCh = new int []{ 10, 10, 10};
        animArrayCh = new int []{ 10, 10, 10, 10, 10, 10};

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

        Debug.Log("Y: "+ yellowPress + ", B: " + bluePress +", R: " + redPress + ", Comb: " + combNumber);
    }





    //Método que se invoca cuando se presiona un boton de color
    public void colorButtonPress(){
        //Si el estudiante presionó uno de los colores necesarios para la combinación
        if(pressColor == reqColorComb1 || pressColor == reqColorComb2){
                   
            //Si presionó el amarillo y no lo ha presionado

            if(pressColor== "yellow" && yellowPress==0){
                //Activo audio
                correctAudio.Play();

                
                //Cambio la imagen dependiendo del nivel
                throwDart(pressColor,1);
                
                //Aumento el numero de la combinación
                combNumber++;
               
            }

            else if(pressColor== "blue" && bluePress==0){
                //Activo audio
                correctAudio.Play();

                
                //Cambio la imagen dependiendo del nivel
                throwDart(pressColor,1);
                
                //Aumento el numero de la combinación
                combNumber++;
               
            }

            else if(pressColor== "red" && redPress==0){
                //Activo audio
                correctAudio.Play();

                
                //Cambio la imagen dependiendo del nivel
                throwDart(pressColor,1);
                
                //Aumento el numero de la combinación
                combNumber++;
             
            }
            

             //Si no ha acabado los niveles y ya terminó la combinación, genera otro número
             if(combNumber==2){
                 //Aumento el nivel
                level++;
                if(level<4){
                //Vuelvo a generar un color solicitado
                 requestedColor();
                }
                 //Reinicio el contador de la combinación
                combNumber=0;
                //Reseteo el numero de errores
                errorCount=0;
                //Reinicio los contadores de colores
                yellowPress=0;
                bluePress = 0;
                redPress=0;
            }
            
        }
        

        //Si presiona el incorrecto
        else{
            //Activo audio
            incorrectAudio.Play();

            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 3 errores
             if (errorCount==3){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
            }
            //Cambio la imagen dependiendo del nivel
            throwDart(pressColor,0);
            //Activamos sonido de color otra vez
            audioColor.PlayDelayed(incorrectAudio.clip.length);
        }
    }





    //Método que me permite saber el color solicitado y los colores que lo generan
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
                
            }
        }

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
            //Colores que lo generan
            reqColorComb1 = "red";
            reqColorComb2 = "yellow";
        }
        //si es verde
        else if(reqColor == "green"){         
            audioColor = greenAudio;
            //Colores que lo generan
            reqColorComb1 = "blue";
            reqColorComb2 = "yellow";
        }
        //si es purpura
        else if(reqColor == "purple"){
            audioColor = purpleAudio;
            //Colores que lo generan
            reqColorComb1 = "blue";
            reqColorComb2 = "red";
        }
        //Activamos sonido de color

            //Si es otro nivel diferente al 1 hay que esperar a que pase el audio
        if( level!=1){
            audioColor.PlayDelayed(correctAudio.clip.length);
        }else{
            audioColor.Play();
        }
        
    }




    //Método que permite animar el dardo
        //Recibe un color y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
            //Si es correcto explota un globo del color
            //Si es incorrecto falla
    public void throwDart(string color, int num){
        //Genero un número aleatorio para las animaciones
        randomNumber2 = Random.Range(0, 4);
        
        //Este for me permite saber si ya salió ese número
        for (int i = 0; i < animArrayCh.Length; i++)
        {
            //Si ya salió
            if(randomNumber2 == animArrayCh[i] ){
                //Vuelvalo a generar
                randomNumber2 = Random.Range(0, 4);
                
            }
        }
        //Añado a mi array el número que acabo de sacar
        animArrayCh[iteration2]= randomNumber2;
        iteration2++;


         if(num==1){
                //Si presionó el amarillo y no lo ha presionado
                if(color == "yellow" && yellowPress==0){
                    //Hago una animación aleatoria
                    dartObj_AN.Play(yellowAnimations[randomNumber2]);    
                    //Desaparezco el globo
                    yellowBallonObj_ARR[randomNumber2].Play("disappear");  
                    yellowPress++;  
                 
                }
                else if(color == "blue" && bluePress==0){
                    dartObj_AN.Play(blueAnimations[randomNumber2]);  
                    //Desaparezco el globo
                    blueBallonObj_ARR[randomNumber2].Play("disappear"); 
                    bluePress++; 
                    
                }
                else if(color=="red" && redPress==0){
                    dartObj_AN.Play(redAnimations[randomNumber2]);  
                    //Desaparezco el globo
                    redBallonObj_ARR[randomNumber2].Play("disappear");
                    redPress++;     
                    
                }

                else if (level==3){
                    Debug.Log("Pasa pagina");
                }
                 
            }
            else if (num==0){
               dartObj_AN.Play(wrongAnimations[randomNumber2]);
            }
    }
}
