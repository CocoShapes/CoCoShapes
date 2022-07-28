using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Level2_ShaJ : MonoBehaviour
{
    //
    public GameObject gameOver_OBJ;

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

    //Animation de los circulos
    private Animator redCircle_L_AN;
    private Animator redCircle_R_AN;

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

    private AudioSource squareAudio;
    private AudioSource triangleAudio;
    private AudioSource circleAudio;
    private AudioSource rectangleAudio;
    private AudioSource starAudio;

    private AudioSource audioShape;

    //Audio de incorrecto
    private AudioSource[] incorrectSounds;
    public GameObject incorrect_Obj;
    private AudioSource incorrectAudio;

    //Audio de correcto
    private AudioSource[] correctSounds;
    public GameObject correct_Obj;
    private AudioSource correctAudio;

     //Para coco
    public GameObject cocoOBJ;
    private Animator cocoOBJ_AN;

    // Start is called before the first frame update
    void Start()
    {


        //Objetos
        imgCircle_SP = imgCircle.GetComponent<Image>().sprite; 
        imgSquare_SP = imgSquare.GetComponent<Image>().sprite;
        imgTriangle_SP = imgTriangle.GetComponent<Image>().sprite;
        imgRectangle_SP = imgRectangle.GetComponent<Image>().sprite;
        imgStar_SP = imgStar.GetComponent<Image>().sprite;

         //Audios
        sounds= obj_Audio.GetComponents<AudioSource>();
        circleAudio = sounds[0];
        squareAudio = sounds[1];
        triangleAudio = sounds[2];
        rectangleAudio = sounds[3];
        starAudio = sounds[4];

        //Audio de incorrecto
        incorrectSounds= incorrect_Obj.GetComponents<AudioSource>();

        //Audio de correcto
        correctSounds= correct_Obj.GetComponents<AudioSource>();

        correctAudio = new AudioSource();
        incorrectAudio = new AudioSource();
        audioShape= new AudioSource();
        
        //Para obtener las animaciones
        weightObj_AN = weightObj.gameObject.GetComponent<Animator>();
        redCircle_L_AN = redCircle_L.gameObject.GetComponent<Animator>();
        redCircle_R_AN = redCircle_R.gameObject.GetComponent<Animator>();
        //De coco
        cocoOBJ_AN = cocoOBJ.GetComponent<Animator>();

        //Dejo la pesa arriba
        weightObj_AN.Play("weight_Stop");

        //Desaparecer circulos
        redCircle_L_AN.Play("red circle_disappear");
        redCircle_R_AN.Play("red circle_disappear");


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
        /*

        Niveles de Figuras:
        Circle --> DownArrow
        Square --> RightArrow
        Triangle --> LeftArrow
        Rectangle --> Backspace
        Star --> Tab
        */
        if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.LeftArrow)
        ||Input.GetKeyDown(KeyCode.Backspace)||Input.GetKeyDown(KeyCode.Tab)){
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                insertedShape="circle";
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow)){
                insertedShape="square";
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow)){
                insertedShape="triangle";
            }
            else if(Input.GetKeyDown(KeyCode.Backspace)){
                insertedShape="circle";
            }
            else if(Input.GetKeyDown(KeyCode.Tab)){
                insertedShape="star";
            }
                shapeInserted();
        //Necesario indicar que ya no se esta insertando
        insertedShape="";
        }
    }

    //Método que se invoca cuando se inserta una figura
    public void shapeInserted(){
        //Si el estudiante inserta la figura correcta  
        if(insertedShape == reqShape){
            //Activo audio correcto
             correctAudio = correctSounds[ Random.Range(0, 5)];
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
            incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
            incorrectAudio.Play();

            Debug.Log("INCORRECTO"); 
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 3 errores
             if (errorCount==3){
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
                gameOver_OBJ.SetActive(true);
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
            //SI EL NIVEL ES EL PRIMERO NO TENGO QUE BAJAR LA PESA
            if(level==1){
                //Inclino solamente la pesa
                weightObj_AN.Play("weight_Right");
                //Oso
                cocoOBJ_AN.Play("cocoRight");
            }else{
                //Hago la animación completa
                weightObj_AN.Play("weight_Left_DOWN");
                //Oso
                cocoOBJ_AN.Play("cocoLeftRev");
            }
            //Tambien la animación de la pesa inclinándose
            
            //Indico que estoy al otro lado
            side=1;
        }else{
            //Activo la figura de la derecha
            shapeObjR.SetActive(true);
            //Desaparezco la otra
            shapeObjL.SetActive(false);
            //SI EL NIVEL ES EL PRIMERO NO TENGO QUE BAJAR LA PESA
            if(level==1){
                 //Inclino solamente la pesa
                weightObj_AN.Play("weight_Left");
                //Oso
                cocoOBJ_AN.Play("cocoLeft");
            }else{
                //Hago la animación completa
                weightObj_AN.Play("weight_Right_DOWN");
                cocoOBJ_AN.Play("cocoRightRev");
            }
            
            //Indico que estoy al otro lado
            side=2;
        }
    }

        //Método que permite colocar o quitar el circulo rojo del error
        //Recibe el nivel y también una variable que le indica si fue correcto o incorrecto
            //Se debe comprobar el lado de la pesa en el que está 1 izquierda 2 derecha
            //El num es para el final, si fue correcto cambio la pagina
    public void redCirclePut(int levelCI, int num){

        //Solo si estoy en el nivel 3 verifico lo de cambiar pagina
         if(levelCI==3){
            if(num==1){
                //Bajo la pesa y desactivo la figura
                if(side==1){
                    weightObj_AN.Play("weight_Right_DOWN_EXIT");
                    //Oso
                    cocoOBJ_AN.Play("cocoRightRev");
                    
                }else{
                    weightObj_AN.Play("weight_Left_DOWN_EXIT");
                    //Oso
                    cocoOBJ_AN.Play("cocoLeftRev");
                    
                }
                shapeObjR.SetActive(false);
                shapeObjL.SetActive(false);
                Debug.Log("Cambio de pagina");
                gameOver_OBJ.SetActive(true);
            }
            else if(num==0){
                 if(side==1){
                     redCircle_R_AN.Play("red circle");
                }else{
                    redCircle_L_AN.Play("red circle");
                }
            }
        }
        else{
            if(num==0){
                 if(side==1){
                     redCircle_R_AN.Play("red circle");
                }else{
                    redCircle_L_AN.Play("red circle");
                }
            }
        }

    }
        
                

        
        

}
