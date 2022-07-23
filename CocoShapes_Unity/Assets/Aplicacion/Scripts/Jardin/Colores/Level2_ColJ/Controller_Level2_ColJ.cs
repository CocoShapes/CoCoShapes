using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level2_ColJ : MonoBehaviour
{
    //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Variable que llega del arduino con el nombre del color presionado
    public string pressColor; 

    //Variable del color solicitado
    public string reqColor;

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

    

    ///-------------------------------------------
    //PARA redCircle

    //Circulo que permite ver el error
    public GameObject redCircle;

    //Animation
    private Animator redCircle_AN;

    ///-------------------------------------------
    //PARA MOVER OBJETOS
    public GameObject fishingObj1;
        //Animation del obj1
    private Animator fishingObj1_AN;


    //IMAGENES DE LOS OBJETOS
    public GameObject imgStrawberry;
        //Sprite 
    private Sprite imgStrawberry_SP;

    public GameObject imgSun;
        //Sprite 
    private Sprite imgSun_SP;

    public GameObject imgWhale;
        //Sprite 
    private Sprite imgWhale_SP;

    public GameObject imgCarrot;
        //Sprite 
    private Sprite imgCarrot_SP;

    public GameObject imgFrog;
        //Sprite 
    private Sprite imgFrog_SP;

    public GameObject imgGrapes;
        //Sprite 
    private Sprite imgGrapes_SP;

    //Audios de Colores
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource redAudio;
    private AudioSource blueAudio;
    private AudioSource yellowAudio;
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

        //Objetos
        imgStrawberry_SP = imgStrawberry.GetComponent<Image>().sprite; 
        imgSun_SP = imgSun.GetComponent<Image>().sprite;
        imgWhale_SP = imgWhale.GetComponent<Image>().sprite;
        imgCarrot_SP = imgCarrot.GetComponent<Image>().sprite;
        imgFrog_SP = imgFrog.GetComponent<Image>().sprite;
        imgGrapes_SP = imgGrapes.GetComponent<Image>().sprite;

         //Audios
        sounds= obj_Audio.GetComponents<AudioSource>();
        redAudio = sounds[0];
        blueAudio = sounds[1];
        yellowAudio = sounds[2];
        greenAudio = sounds[3];
        orangeAudio = sounds[4];
        purpleAudio = sounds[5];

        //Audio de incorrecto
        incorrectSounds= incorrect_Obj.GetComponents<AudioSource>();

        //Audio de correcto
        correctSounds= correct_Obj.GetComponents<AudioSource>();

        audioColor= new AudioSource();
        correctAudio = new AudioSource();
        incorrectAudio = new AudioSource();
        //Para obtener las animaciones
        fishingObj1_AN= fishingObj1.gameObject.GetComponent<Animator>();
        redCircle_AN =redCircle.gameObject.GetComponent<Animator>();

        //Dejo todos los objetos abajo
        fishingObj1_AN.Play("fishingObjectAnim_Stop");
        //Quito circulo
        redCircle_AN.Play("red circle_disappear");

        //Otras variables
        level=1;
        
        errorCount=0;
        randomNumber=0;
        iteration=0;
       

        colorsArray = new string []{ "red", "blue", "yellow", "green", "purple", "orange"};
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
        
        
       // Debug.Log("Level: " + level +" ___PressColor: "+ pressColor +" ___RequestedColor: "+ reqColor + "____ErrorCount: " + errorCount);
            
    }

    //Método que se invoca cuando se presiona un boton de color
    public void colorButtonPress(){
        //Si el estudiante presionó el color correcto   
        if(pressColor == reqColor){
            //Activo audio correcto
             correctAudio = correctSounds[ Random.Range(0, 5)];
             correctAudio.Play();

            
            //Cambio la imagen dependiendo del nivel
            redCirclePut(level,1);
            //Aumento el nivel
            level++;  
            //Reseteo el numero de errores
            errorCount=0;
            //Devuelvo el objeto abajo
            fishingObj1_AN.Play("fishingObjectAnim_Stop");
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

            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 3 errores
             if (errorCount==3){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
            }
            //Cambio la imagen dependiendo del nivel
            redCirclePut(level,0);
            //Activamos sonido de color otra vez
            audioColor.PlayDelayed(incorrectAudio.clip.length);
        }
    }

    //Método que me permite saber el color solicitado
    public void requestedColor(){

        //Este método me permite saber si ya salió ese número
        //Recibe el arreglo, y la cantidad de numeros a generar
       colorsArrayCh[iteration]= randomGenerate(colorsArrayCh, 6);
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

        //Aqui vamos a asignar la imagen del color solicitado al objeto.
        //    0       1        2        3         4        5
        //{ "red", "blue", "yellow", "green", "purple", "orange"};

        //Si es rojo
        if(reqColor == "red"){
            //Le coloco la fresa
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgStrawberry_SP;
            audioColor = redAudio;
        }
        //si es azul
        else if(reqColor == "blue"){
            //Le coloco la ballena
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgWhale_SP;
            audioColor = blueAudio;
        }
        //si es amarillo
        else if(reqColor == "yellow"){
            //Le coloco el sol
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgSun_SP;
            audioColor = yellowAudio;
        }
        //si es naranja
        else if(reqColor == "orange"){
            //Le coloco la zanahoria
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgCarrot_SP;
            audioColor = orangeAudio;

        }
        //si es verde
        else if(reqColor == "green"){
            //Le coloco la rana
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgFrog_SP;
            audioColor = greenAudio;
        }
        //si es purpura
        else if(reqColor == "purple"){
            //Le coloco las uvas
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgGrapes_SP;
            audioColor = purpleAudio;
        }
        //Activamos sonido de color

            //Si es otro nivel diferente al 1 hay que esperar a que pase el audio
        if( level!=1){
            audioColor.PlayDelayed(correctAudio.clip.length);
        }else{
            audioColor.Play();
        }

        
        
        //Ahora, hacemos la animación de subir el objeto
        fishingObj1_AN.Play("fishingObjectAnim");
        
    }

    //Método para generar los aleatorios y confirmar que no salgan
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
        Debug.Log("Arreglo: "+ array[0] + " " + array[1] + " " +array[2]);
        return(randomNumber);
    }

    //Método que permite colocar o quitar el circulo rojo del error
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
            //Si es correcto lo desactiva
            //Si es incorrecto lo activa
    public void redCirclePut(int levelCI, int num){
        //Solo si estoy en el nivel 3 verifico lo de cambiar pagina
         if(levelCI==3){
            if(num==1){
                Debug.Log("Cambio de pagina");
            }
            else if(num==0){
                redCircle_AN.Play("red circle");
            }
        }
        else{
            if(num==0){
                     redCircle_AN.Play("red circle");
            }
        }
          
    }

    
}
