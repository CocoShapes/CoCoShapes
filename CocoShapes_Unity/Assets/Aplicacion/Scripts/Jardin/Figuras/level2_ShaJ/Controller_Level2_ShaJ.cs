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
        //FIGURA IZQUIERDA
    public GameObject shapeObjL;
        //Animation del obj1
    private Animator shapeObjL_AN;

          //FIGURA DERECHA
    public GameObject shapeObjR;
        //Animation del obj2
    private Animator shapeObjR_AN;

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
        //Para obtener todos los sprites
        imgPart1_In_SP= imgPart1_In.GetComponent<Image>().sprite;
        imgPart2_SP= imgPart2.GetComponent<Image>().sprite;
        imgPart2_In_SP= imgPart2_In.GetComponent<Image>().sprite;
        imgPart3_SP= imgPart3.GetComponent<Image>().sprite;
        imgPart3_In_SP= imgPart3_In.GetComponent<Image>().sprite;
        

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
        shapeObjL_AN= shapeObjL.gameObject.GetComponent<Animator>();
        shapeObjR_AN= shapeObjR.gameObject.GetComponent<Animator>();
        weightObj_AN = weightObj.gameObject.GetComponent<Animator>();

        //Dejo todas las figuras y la pesa arriba
        shapeObjL_AN.Play("shape_Left_STOP");
        shapeObjR_AN.Play("shape_Right_STOP");
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
            changeImg(level,1);
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
            changeImg(level,0);
            //Activamos sonido de color otra vez
            audioShape.PlayDelayed(incorrectAudio.clip.length);
        }
    }

    //Método que me permite saber la figura solicitada
    public void requestedShape(){
       //Genero el número aleatorio

        randomNumber = Random.Range(0, 4);
        
        //Este for me permite saber si ya salió ese número
        for (int i = 0; i < shapesArrayCh.Length; i++)
        {
            //Si ya salió
            if(randomNumber == shapesArrayCh[i] ){
                //Vuelvalo a generar
                randomNumber = Random.Range(0, 4);
                
            }
        }
        //Añado a mi array el número que acabo de sacar
        shapesArrayCh[iteration]= randomNumber;
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

    public void checkSide(){
        //si esta en la izquierda
        if(side==2){
            //Activo la figura de la izquierda
            shapeObjL.SetActive(true);
            //Desaparezco la otra
            shapeObjR.SetActive(false);
            //Ahora, hacemos la animación de bajar la figura
            shapeObjL_AN.Play("shape_Left");
            //Tambien la animación de la pesa inclinándose
            weightObj_AN.Play("weight_Right");
            //Indico que estoy al otro lado
            side=1;
        }else{
            //Activo la figura de la derecha
            shapeObjR.SetActive(true);
            //Desaparezco la otra
            shapeObjL.SetActive(false);
            //Ahora, hacemos la animación de bajar la figura
            shapeObjR_AN.Play("shape_Right");
            //Tambien la animación de la pesa inclinándose
            weightObj_AN.Play("weight_Left");
            //Indico que estoy al otro lado
            side=2;
        }
    }

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
