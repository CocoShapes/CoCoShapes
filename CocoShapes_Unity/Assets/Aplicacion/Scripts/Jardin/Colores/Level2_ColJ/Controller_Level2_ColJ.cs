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

    //Audios de correcto o incorrecto y colores
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
        
        //Quitamos el circulo rojo del error
        redCircle.SetActive(false);

        //Objetos
        imgStrawberry_SP = imgStrawberry.GetComponent<Image>().sprite; 
        imgSun_SP = imgSun.GetComponent<Image>().sprite;
        imgWhale_SP = imgWhale.GetComponent<Image>().sprite;
        imgCarrot_SP = imgCarrot.GetComponent<Image>().sprite;
        imgFrog_SP = imgFrog.GetComponent<Image>().sprite;
        imgGrapes_SP = imgGrapes.GetComponent<Image>().sprite;

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
        fishingObj1_AN= fishingObj1.gameObject.GetComponent<Animator>();

        //Dejo todos los objetos abajo
        fishingObj1_AN.Play("fishingObjectAnim_Stop");

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
            //Activo audio
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
       //Genero el número aleatorio

        randomNumber = Random.Range(0, 6);
        
        //Este for me permite saber si ya salió ese número
        for (int i = 0; i < colorsArrayCh.Length; i++)
        {
            //Si ya salió
            if(randomNumber == colorsArrayCh[i] ){
                //Vuelvalo a generar
                randomNumber = Random.Range(0, 6);
                
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

    //Método que permite colocar o quitar el circulo rojo del error
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
            //Si es correcto lo desactiva
            //Si es incorrecto lo activa
    public void redCirclePut(int levelCI, int num){

        if(levelCI==1){
            if(num==1){
                redCircle.SetActive(false);
            }
            else if(num==0){
                redCircle.SetActive(true);
            }
            
        }
        else if (levelCI==2){
            if(num==1){
               redCircle.SetActive(false);
            }
            else if(num==0){
                redCircle.SetActive(true);
            }
        }
        else if(levelCI==3){
            if(num==1){
                Debug.Log("Cambio de pagina");
                redCircle.SetActive(false);
            }
            else if(num==0){
                redCircle.SetActive(true);
            }
        }
        else{
            Debug.Log("Cambio pagina");
        }

    }
}
