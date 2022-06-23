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


   

    // Start is called before the first frame update
    void Start()
    {
        
        //Para obtener todos los sprites
        imgPart1_In_SP= imgPart1_In.GetComponent<Image>().sprite;
        imgPart2_SP= imgPart2.GetComponent<Image>().sprite;
        imgPart2_In_SP= imgPart2_In.GetComponent<Image>().sprite;
        imgPart3_SP= imgPart3.GetComponent<Image>().sprite;
        imgPart3_In_SP= imgPart3_In.GetComponent<Image>().sprite;
        

        //Objetos
        imgStrawberry_SP = imgStrawberry.GetComponent<Image>().sprite; 
        imgSun_SP = imgSun.GetComponent<Image>().sprite;
        imgWhale_SP = imgWhale.GetComponent<Image>().sprite;
        imgCarrot_SP = imgCarrot.GetComponent<Image>().sprite;
        imgFrog_SP = imgFrog.GetComponent<Image>().sprite;
        imgGrapes_SP = imgGrapes.GetComponent<Image>().sprite;
            
        
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

    //Método que se invoca cuando se presiona un boton de color, recibe la parte del nivel en la que se encuentra
    public void colorButtonPress(){
        //Si el estudiante presionó el color correcto   
        if(pressColor == reqColor){
            //Vuelvo al objeto hacia abajo 
            //Cambio la imagen dependiendo del nivel
            changeImg(level,1);
            //Aumento el nivel
            level++;  
            //Reseteo el numero de errores
            errorCount=0;
            //Devuelvo el objeto abajo
            fishingObj1_AN.Play("fishingObjectAnim_Stop");
            //Vuelvo a generar un color solicitado
            requestedColor();
        }
        

        //Si presiona el incorrecto
        else{
            
            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 3 errores
             if (errorCount==3){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
            }
            //Cambio la imagen dependiendo del nivel
            changeImg(level,0);
        }
    }

    //Método que me permite saber el color solicitado
    public void requestedColor(){
       //Genero el número aleatorio

        randomNumber = Random.Range(0, 5);
        
        //Este for me permite saber si ya salió ese número
        for (int i = 0; i < colorsArrayCh.Length; i++)
        {
            //Si ya salió
            if(randomNumber == colorsArrayCh[i] ){
                //Vuelvalo a generar
                randomNumber = Random.Range(0, 5);
                
            }
        }
        //Añado a mi array el número que acabo de sacar
        colorsArrayCh[iteration]= randomNumber;
        iteration++;

        
        //Recorremos el arreglo de colores para devolver el color solicitado

        for (int n = 0; n < colorsArray.Length; n++)
        {
            Debug.Log("entra");
            //Si encuentra la posición
            if(n == randomNumber ){
                reqColor = colorsArray[n];
               
            }
        }

        //Aqui vamos a asignar la imagen del color solicitado al objeto.
        //    0       1        2        3         4        5
        //{ "red", "blue", "yellow", "green", "purple", "orange"};

        //Si es rojo
        if(reqColor == "red"){
            //Le coloco la fresa
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgStrawberry_SP;
        }
        //si es azul
        else if(reqColor == "blue"){
            //Le coloco la ballena
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgWhale_SP;
        }
        //si es amarillo
        else if(reqColor == "yellow"){
            //Le coloco el sol
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgSun_SP;
        }
        //si es naranja
        else if(reqColor == "orange"){
            //Le coloco la zanahoria
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgCarrot_SP;
        }
        //si es verde
        else if(reqColor == "green"){
            //Le coloco la rana
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgFrog_SP;
        }
        //si es purpura
        else if(reqColor == "purple"){
            //Le coloco las uvas
            fishingObj1.gameObject.GetComponent<Image>().sprite= imgGrapes_SP;
        }

        //Ahora, hacemos la animación de subir el objeto
        fishingObj1_AN.Play("fishingObjectAnim");
        
    }

    //Método que cambia la imagen de fondo
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
    public void changeImg(int levelCI, int num){

        if(levelCI==1){
            if(num==1){
                canvas.GetComponent<Image>().sprite=imgPart2_SP;
            }
            else if(num==0){
                canvas.GetComponent<Image>().sprite=imgPart1_In_SP;
            }
            
        }
        else if (levelCI==2){
            if(num==1){
                canvas.GetComponent<Image>().sprite=imgPart3_SP;
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
