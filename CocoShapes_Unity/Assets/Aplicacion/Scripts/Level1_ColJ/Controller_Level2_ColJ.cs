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


    //PARA MOVER OBJETOS
    public GameObject fishingObj1;
        //Animation del obj1
    private Animator fishingObj1_AN;

    public GameObject fishingObj2;
        //Animation del obj2
    private Animator fishingObj2_AN;

    public GameObject fishingObj3;
        //Animation del obj3
    private Animator fishingObj3_AN;

    // Start is called before the first frame update
    void Start()
    {
        //Para obtener todos los sprites
        imgPart1_In_SP= imgPart1_In.GetComponent<Image>().sprite;
        imgPart2_SP= imgPart2.GetComponent<Image>().sprite;
        imgPart2_In_SP= imgPart2_In.GetComponent<Image>().sprite;
        imgPart3_SP= imgPart3.GetComponent<Image>().sprite;
        imgPart3_In_SP= imgPart3_In.GetComponent<Image>().sprite;
        
        //Para obtener las animaciones
        fishingObj1_AN= fishingObj1.gameObject.GetComponent<Animator>();
        fishingObj2_AN= fishingObj2.gameObject.GetComponent<Animator>();
        fishingObj3_AN= fishingObj3.gameObject.GetComponent<Animator>();

        //Dejo todos los objetos abajo
        fishingObj1_AN.Play("fishingObjectAnim_Stop");
        fishingObj2_AN.Play("fishingObjectAnim_Stop");
        fishingObj3_AN.Play("fishingObjectAnim_Stop");

        //Desactivo el objeto 2 y 3
        fishingObj2.SetActive(false);
        fishingObj3.SetActive(false);

        //Subo el primer objeto
        fishingObj1_AN.Play("fishingObjectAnim");

        level=1;
        reqColor="";
        //Aqui se realizaria lo de la conexión del Arduino
        pressColor="";

    }

    // Update is called once per frame
    void Update()
    {
        //Método que me permite saber el color solicitado
        requestedColor(level);
        

        //Aqui se realizaria lo de la conexión del Arduino
        //Es necesario verificar que haya presionado alguno de los botones
        if(pressColor=="red"||pressColor=="yellow"||pressColor=="blue"
        ||pressColor=="orange"||pressColor=="green"||pressColor=="purple"
        ||pressColor=="black"||pressColor=="white"){
                colorButtonPress();
        //Necesario indicar que ya no se esta presionando
        pressColor="";
        }
        

        Debug.Log("Level: " + level +" ___PressColor: "+ pressColor +" ___RequestedColor: "+ reqColor);
            
    }

    //Método que se invoca cuando se presiona un boton de color, recibe la parte del nivel en la que se encuentra
    public void colorButtonPress(){
        //Si el estudiante presionó el color correcto   
        if(pressColor == reqColor){
            //Cambio la imagen dependiendo del nivel
            changeImg(level,1);
            //Aumento el nivel
            level++;  
        }
        

        //Si presiona el incorrecto
        else{
            
            Debug.Log("INCORRECTO"); 
            //Cambio la imagen dependiendo del nivel
            changeImg(level,0);
        }
    }

    //Método que me permite saber el color solicitado
    public void requestedColor(int levelRQ){
       
        if(levelRQ==1){
            reqColor ="red";
        }
        else if (levelRQ==2){
            reqColor ="yellow";
        }
        else if(levelRQ==3){
            reqColor ="green";
        }
        else{
            reqColor="";
        }
       
    }

    //Método que cambia la imagen de fondo
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
    public void changeImg(int levelCI, int num){

        if(levelCI==1){
            if(num==1){
                canvas.GetComponent<Image>().sprite=imgPart2_SP;
                //Desactivo el objeto que está arriba
                fishingObj1.SetActive(false);
                
                //Activo y Subo el siguiente objeto
                fishingObj2.SetActive(true);
                fishingObj2_AN.Play("fishingObjectAnim");
            }
            else if(num==0){
                canvas.GetComponent<Image>().sprite=imgPart1_In_SP;
            }
            
        }
        else if (levelCI==2){
            if(num==1){
                canvas.GetComponent<Image>().sprite=imgPart3_SP;
                //Desactivo el objeto que está arriba
                fishingObj2.SetActive(false);

                //Activo y Subo el siguiente objeto
                fishingObj3.SetActive(true);
                fishingObj3_AN.Play("fishingObjectAnim");
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
