using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Level2_ColJ : MonoBehaviour
{
    //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Variable que llega del arduino con el nombre del color presionado
    public string pressColor; 

    //Variable del color solicitado
    public string reqColor;


    // Start is called before the first frame update
    void Start()
    {
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
        //Si no ha presionado ningun boton
        if(pressColor==""){
            
        }
        else{
            colorButtonPress();
        }

        Debug.Log("Level: " + level +" ___PressColor: "+ pressColor +" ___RequestedColor: "+ reqColor);
            
    }

    //Método que se invoca cuando se presiona un boton de color, recibe la parte del nivel en la que se encuentra
    public void colorButtonPress(){
        //Si el estudiante presionó el color correcto   
        if(pressColor == reqColor){
            //Aumento el nivel
            level++;  
        }

        //Si presiona el incorrecto
        else{
               Debug.Log("INCORRECTO"); 
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
}
