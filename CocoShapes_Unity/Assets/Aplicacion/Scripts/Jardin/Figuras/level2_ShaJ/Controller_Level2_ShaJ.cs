using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level2_ShaJ : MonoBehaviour
{
    //Variables necesarias
    //Parte del nivel en el que se encuentra
    public int level;

    //Variable que llega del arduino con el nombre de la figura insertada
    public string insertedShape; 

    //Variable de la figura solicitada
    public string reqShape;

    //Contador de errores
    private int errorCount;

    //Numero aleatorio
    private int randomNumber; 

    //Arreglo de figuras
    private string[] shapesArray;
    //Arreglo de figuras que ya salieron
    private int[] shapesArrayCh;
    //Numero que me permite saber el numero de iteraciones
    private int iteration;

///-------------------------------------------

     //PARA redCircle

    //Circulo que permite ver el error
    public GameObject redCircle_L;
    public GameObject redCircle_R;

    ///-------------------------------------------
    //PARA MOVER OBJETOS
        //FIGURA IZQUIERDA
    public GameObject shapeObjL;

          //FIGURA DERECHA
    public GameObject shapeObjR;

        //PESA
    public GameObject weightObj;
        //Animation de la pesa
    private Animator weightObj_AN;


    //variable que me permite saber si es el lado izquierdo o derecho
    private int side;


    //IMAGENES DE LOS OBJETOS

    //Cuadrado
    public GameObject imgSquare;
        //Sprite 
    private Sprite imgSquare_SP;

    //Triangulo
    public GameObject imgTriangle;
        //Sprite 
    private Sprite imgTriangle_SP;

    //Circulo
    public GameObject imgCircle;
        //Sprite 
    private Sprite imgCircle_SP;

    //Rectangulo
    public GameObject imgRectangle;
        //Sprite 
    private Sprite imgRectangle_SP;

    //Star
    public GameObject imgStar;
        //Sprite 
    private Sprite imgStar_SP;


    //Audios de correcto o incorrecto y colores
    private AudioSource[] sounds;
    public GameObject obj_Audio;

    private AudioSource correctAudio;
    private AudioSource incorrectAudio;

    private AudioSource squareAudio;
    private AudioSource triangleAudio;
    private AudioSource circleAudio;
    private AudioSource rectangleAudio;
    private AudioSource starAudio;

    private AudioSource audioShape;

    // Start is called before the first frame update
    void Start()
    {
        //Quitamos el circulo rojo del error
        redCircle_L.SetActive(false);
        redCircle_R.SetActive(false);


        //Objetos
        imgCircle_SP = imgCircle.GetComponent<Image>().sprite; 
        imgSquare_SP = imgSquare.GetComponent<Image>().sprite;
        imgTriangle_SP = imgTriangle.GetComponent<Image>().sprite;
        imgRectangle_SP = imgRectangle.GetComponent<Image>().sprite;
        imgStar_SP = imgStar.GetComponent<Image>().sprite;

         //Audios
        sounds= obj_Audio.GetComponents<AudioSource>();
        correctAudio= sounds[0];
        incorrectAudio= sounds[1];
        circleAudio = sounds[2];
        squareAudio = sounds[3];
        triangleAudio = sounds[4];
        rectangleAudio = sounds[5];
        starAudio = sounds[6];

        audioShape= new AudioSource();
        
        //Para obtener las animaciones
        weightObj_AN = weightObj.gameObject.GetComponent<Animator>();

        //Dejo la pesa arriba
        weightObj_AN.Play("weight_Stop");


        //Otras variables
        level=1;
        
        errorCount=0;
        randomNumber=0;
        iteration=0;

        //1= izquierda 2=derecha
        side = 1;
       

        shapesArray = new string []{ "circle", "square", "triangle", "rectangle", "star"};
        //Colocar numeros muy altos que nunca saldran
        shapesArrayCh = new int []{ 10, 10, 10};

        //Aplico el método que me genera la figura solicitada
        requestedShape();

        //Aqui se realizaria lo de la conexión del Arduino
        insertedShape="";
    }

    // Update is called once per frame
    void Update()
    {
        //Aqui se realizaria lo de la conexión del Arduino
        //Es necesario verificar que haya insertado alguna de las figuras
        if(insertedShape=="circle"||insertedShape=="square"||insertedShape=="triangle"
        ||insertedShape=="rectangle"||insertedShape=="star"){
                shapeInserted();
        //Necesario indicar que ya no se esta insertando
        insertedShape="";
        }
    }

    //Método que se invoca cuando se inserta una figura
    public void shapeInserted(){
        //Si el estudiante inserta la figura correcta  
        if(insertedShape == reqShape){
            //Activo audio
             correctAudio.Play();

            
            //Cambio la imagen dependiendo del nivel
            redCirclePut(level,1);
            //Aumento el nivel
            level++;  
            //Reseteo el numero de errores
            errorCount=0;

            
           
            if(level<4){
                //Vuelvo a generar un una figura solicitada
            requestedShape();
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
            audioShape.PlayDelayed(incorrectAudio.clip.length);
        }
    }

    //Método que me permite saber la figura solicitada
    public void requestedShape(){
       //Este método me permite saber si ya salió ese número
        //Recibe el arreglo, y la cantidad de numeros a generar
       shapesArrayCh[iteration]= randomGenerate(shapesArrayCh, 5);
       iteration++;
    
        //Recorremos el arreglo de figuras para devolver la figura solicitada

        for (int n = 0; n < shapesArray.Length; n++)
        {
            //Si encuentra la posición
            if(n == randomNumber ){
                reqShape = shapesArray[n];
               Debug.Log ("RN: " + randomNumber + "CL: " + shapesArray[n]  );
            }
        }

        //Aqui vamos a asignar la imagen de la figura solicitada al objeto.
        //Se va a asignar a ambos lados, la diferencia está en cual se muestra
        
        //Si es circulo
        if(reqShape == "circle"){
            //Le coloco el circulo
            shapeObjL.gameObject.GetComponent<Image>().sprite= imgCircle_SP;
            shapeObjR.gameObject.GetComponent<Image>().sprite= imgCircle_SP;
            audioShape = circleAudio;
        }
        //si es cuadrado
        else if(reqShape == "square"){
            //Le coloco el cuadrado
            shapeObjL.gameObject.GetComponent<Image>().sprite= imgSquare_SP;
            shapeObjR.gameObject.GetComponent<Image>().sprite= imgSquare_SP;
            audioShape = squareAudio;
        }
        //si es triangulo
        else if(reqShape == "triangle"){
            //Le coloco el triangulo
            shapeObjL.gameObject.GetComponent<Image>().sprite= imgTriangle_SP;
            shapeObjR.gameObject.GetComponent<Image>().sprite= imgTriangle_SP;
            audioShape = triangleAudio;
        }
        //si es rectangulo
        else if(reqShape == "rectangle"){
            //Le coloco el rectangulo
            shapeObjL.gameObject.GetComponent<Image>().sprite= imgRectangle_SP;
            shapeObjR.gameObject.GetComponent<Image>().sprite= imgRectangle_SP;
            audioShape = rectangleAudio;

        }
        //si es estrella
        else if(reqShape == "star"){
            //Le coloco el rectangulo
            shapeObjL.gameObject.GetComponent<Image>().sprite= imgStar_SP;
            shapeObjR.gameObject.GetComponent<Image>().sprite= imgStar_SP;
            audioShape = starAudio;

        }
        
        //Activamos sonido de color

            //Si es otro nivel diferente al 1 hay que esperar a que pase el audio
        if( level!=1){
            audioShape.PlayDelayed(correctAudio.clip.length);
        }else{
            audioShape.Play();
        }

        //Aqui debo verificar si es la figura de la izquierda o de la derecha 
        checkSide();
        
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
        Debug.Log("Arreglo: "+ array[0] + " " + array[1] + " " +array[2]);
        return(randomNumber);
    }

    public void checkSide(){
        //si esta en la izquierda
        if(side==2){
            //Activo la figura de la izquierda
            shapeObjL.SetActive(true);
            //Desaparezco la otra
            shapeObjR.SetActive(false);
            //Tambien la animación de la pesa inclinándose
            weightObj_AN.Play("weight_Right");
            //Indico que estoy al otro lado
            side=1;
        }else{
            //Activo la figura de la derecha
            shapeObjR.SetActive(true);
            //Desaparezco la otra
            shapeObjL.SetActive(false);
            //Tambien la animación de la pesa inclinándose
            weightObj_AN.Play("weight_Left");
            //Indico que estoy al otro lado
            side=2;
        }
    }

        //Método que permite colocar o quitar el circulo rojo del error
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //1 es correcto y 0 incorrecto
            //Si es correcto lo desactiva
            //Si es incorrecto lo activa
            //Se debe comprobar también el lado de la pesa en el que está 1 izquierda 2 derecha
    public void redCirclePut(int levelCI, int num){

        if(levelCI==1){
            if(num==1){
                //Desactivo todo
                    redCircle_L.SetActive(false);
                    redCircle_R.SetActive(false);
            }
            else if(num==0){
                if(side==1){
                    redCircle_R.SetActive(true);
                }else{
                    redCircle_L.SetActive(true);
                }
            }
            
        }
        else if (levelCI==2){
            if(num==1){
              //Desactivo todo
                    redCircle_L.SetActive(false);
                    redCircle_R.SetActive(false);
            }
            else if(num==0){
                if(side==1){
                    redCircle_R.SetActive(true);
                }else{
                    redCircle_L.SetActive(true);
                }
            }
        }
        else if(levelCI==3){
            if(num==1){
                Debug.Log("Cambio de pagina");
                 //Desactivo todo
                    redCircle_L.SetActive(false);
                    redCircle_R.SetActive(false);
            }
            else if(num==0){
                 if(side==1){
                    redCircle_R.SetActive(true);
                }else{
                    redCircle_L.SetActive(true);
                }
            }
        }
        else{
            Debug.Log("Cambio pagina");
        }

    }
        
                

        
        

}
