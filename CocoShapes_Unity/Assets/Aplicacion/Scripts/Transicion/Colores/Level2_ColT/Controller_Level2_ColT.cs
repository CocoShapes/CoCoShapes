using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller_Level2_ColT : MonoBehaviour
{
    //public GameObject gameOver_OBJ;
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

    //Numero que me permite saber el numero de iteraciones
    private int iteration;
    //Iteraciones por colores
    private int iterationY;
    private int iterationR;
    private int iterationB;

    //Variable que llega del arduino con el nombre del color presionado
    public string pressColor;

    //Variable del color solicitado
    public string reqColor;

    //Variables de los colores que se necesitan para realizar la combinacion
    private string reqColorComb1;
    private string reqColorComb2;

    //Arreglo de animaciones para los globos rojos
    private string[] redAnimations;
    //Arreglo de las que ya salieron
    private string[] redAnimationsDone;

    //Arreglo de animaciones para los globos azules
    private string[] blueAnimations;
    //Arreglo de las que ya salieron
    private string[] blueAnimationsDone;

    //Arreglo de animaciones para los globos amarillos
    private string[] yellowAnimations;
    //Arreglo de las que ya salieron
    private string[] yellowAnimationsDone;

    //Variable animacion
    private string animationRandom;

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

    private AudioSource redAudio;
    private AudioSource blueAudio;
    private AudioSource yellowAudio;
    private AudioSource greenAudio;
    private AudioSource orangeAudio;
    private AudioSource purpleAudio;
    private AudioSource pressSecond;
    private AudioSource audioCPress;
    private AudioSource purpleCAudio;
    private AudioSource orangeCAudio;
    private AudioSource greenCAudio;
    private AudioSource whiteAudio;
    private AudioSource blackAudio;

    private AudioSource audioColor;

    //Audio de incorrecto
    private AudioSource[] incorrectSounds;
    public GameObject incorrect_Obj;
    private AudioSource incorrectAudio;
    private AudioSource incorrectAudioSound;

    //Audio de correcto
    private AudioSource[] correctSounds;
    public GameObject correct_Obj;
    private AudioSource correctAudio;
    private AudioSource correctAudioSound;


    //Para coco
    public GameObject cocoOBJ;
    private Animator cocoOBJ_AN;

    //Para la base de datos
    //Database and Game Finished
    private DatabaseController database;
    private string subject = "Colors";
    private int levelBD = 2;

    public GameObject panelGameFinished;
    private float totalGameTime;


    // Start is called before the first frame update
    void Start()
    {
        //Audios
        sounds = obj_Audio.GetComponents<AudioSource>();


        greenAudio = sounds[0];
        orangeAudio = sounds[1];
        purpleAudio = sounds[2];
        pressSecond = sounds [3];
        redAudio = sounds [4];
        yellowAudio = sounds [5];
        blueAudio = sounds [6];
        purpleCAudio = sounds [7];
        orangeCAudio = sounds [8];
        greenCAudio = sounds [9];
        whiteAudio = sounds [10];
        blackAudio = sounds [11];

        //Audio de incorrecto
        incorrectSounds = incorrect_Obj.GetComponents<AudioSource>();
        incorrectAudioSound = incorrectSounds[3];

        //Audio de correcto
        correctSounds = correct_Obj.GetComponents<AudioSource>();
        correctAudioSound = correctSounds[5];

        correctAudio = new AudioSource();
        incorrectAudio = new AudioSource();
        audioColor = new AudioSource();

        //Para obtener las animaciones
        dartObj_AN = dartObj.gameObject.GetComponent<Animator>();
        //De coco
        cocoOBJ_AN = cocoOBJ.GetComponent<Animator>();

        //Arreglo de animaciones
        redAnimations = new string[] { "dart_red_throw_1", "dart_red_throw_2", "dart_red_throw_3", "dart_red_throw_4" };
        blueAnimations = new string[] { "dart_blue_throw_1", "dart_blue_throw_2", "dart_blue_throw_3", "dart_blue_throw_4" };
        yellowAnimations = new string[] { "dart_yellow_throw_1", "dart_yellow_throw_2", "dart_yellow_throw_3", "dart_yellow_throw_4" };
        wrongAnimations = new string[] { "dart_wrong_throw_1", "dart_wrong_throw_2", "dart_wrong_throw_3", "dart_wrong_throw_4" };

        //Arreglo de las animaciones que ya salieron
        redAnimationsDone = new string[] { "uno", "dos", "tres", "cuatro" };
        blueAnimationsDone = new string[] { "uno", "dos", "tres", "cuatro" };
        yellowAnimationsDone = new string[] { "uno", "dos", "tres", "cuatro" };

        //Arreglo de globos
        redBallonObj_ARR = new Animator[] { redBallonObj1.gameObject.GetComponent<Animator>(), redBallonObj2.gameObject.GetComponent<Animator>(), redBallonObj3.gameObject.GetComponent<Animator>(), redBallonObj4.gameObject.GetComponent<Animator>() };
        blueBallonObj_ARR = new Animator[] { blueBallonObj1.gameObject.GetComponent<Animator>(), blueBallonObj2.gameObject.GetComponent<Animator>(), blueBallonObj3.gameObject.GetComponent<Animator>(), blueBallonObj4.gameObject.GetComponent<Animator>() };
        yellowBallonObj_ARR = new Animator[] { yellowBallonObj1.gameObject.GetComponent<Animator>(), yellowBallonObj2.gameObject.GetComponent<Animator>(), yellowBallonObj3.gameObject.GetComponent<Animator>(), yellowBallonObj4.gameObject.GetComponent<Animator>() };

        //Otras variables
        level = 1;

        errorCount = 0;
        randomNumber = 0;
        randomNumber2 = 0;
        iteration = 0;
        iterationB = 0;
        iterationY = 0;
        iterationR = 0;
        combNumber = 0;

        yellowPress = 0;
        bluePress = 0;
        redPress = 0;

        colorsArray = new string[] { "green", "purple", "orange" };
        //Colocar numeros muy altos que nunca saldran
        colorsArrayCh = new int[] { 10, 10, 10 };

        //Aplico el método que me genera el color solicitado
        requestedColor();


        //Aqui se realizaria lo de la conexión del Arduino
        pressColor = "";

        //Para la base de datos:
        //Obtain database gameobject
        database = GameObject.Find("Database").GetComponent<DatabaseController>();

    }




    // Update is called once per frame
    void Update()
    {
        totalGameTime += Time.deltaTime;

        //Aqui se realizaria lo de la conexión del Arduino
        /*Niveles de Colores:
            Yellow --> F1
            Blue --> F2
            Red --> F3
            Green --> F4
            Orange --> F5
            Purple --> F6
            Black --> F7
            White --> F8*/
        //Es necesario verificar que haya presionado alguno de los botones

                //Asigno variable
                if(Input.GetKeyDown(KeyCode.F1)){
                        audioCPress=yellowAudio;
                        pressColor="yellow";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                        
                }
                else if(Input.GetKeyDown(KeyCode.F2)){
                        audioCPress=blueAudio;
                        pressColor="blue";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                else if(Input.GetKeyDown(KeyCode.F3)){
                        audioCPress=redAudio;
                        pressColor="red";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                else if(Input.GetKeyDown(KeyCode.F4)){
                        audioCPress=greenCAudio;
                        pressColor="green";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                else if(Input.GetKeyDown(KeyCode.F5)){
                        audioCPress=orangeCAudio;
                        pressColor="orange";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                else if(Input.GetKeyDown(KeyCode.F6)){
                        audioCPress=purpleCAudio;
                        pressColor="purple";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                else if(Input.GetKeyDown(KeyCode.F7)){
                        audioCPress=blackAudio;
                        pressColor="black";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                else if(Input.GetKeyDown(KeyCode.F8)){
                        audioCPress=whiteAudio;
                        pressColor="white";
                        //Detengo instrucción si esta sonando
                        audioColor.Stop();
                        audioCPress.Play();
                        //Activo color presionado
                
                        colorButtonPress();
                        //Necesario indicar que ya no se esta presionando
                        pressColor="";
                }
                
                
        

        



    }


    //Método que se invoca cuando se presiona un boton de color
    public void colorButtonPress()
    {

        //Si el estudiante presionó uno de los colores necesarios para la combinación

        if(pressColor == reqColorComb1 || pressColor == reqColorComb2){
                      

            //Si presionó el amarillo
             
             if(pressColor== "yellow"){
                //Si no lo ha presionado antes
                if(yellowPress==0){
                    //Activo audio correcto
                    correctAudio = correctSounds[ Random.Range(0, 5)];
                    correctAudio.PlayDelayed(audioCPress.clip.length);
                    correctAudioSound.PlayDelayed(audioCPress.clip.length);

                
                    //Cambio la imagen dependiendo del nivel
                    throwDart(pressColor,1);
                
                    //Aumento el numero de la combinación
                    combNumber++;
                    //Hago la animación de Coco
                    cocoOBJ_AN.Play("cocoDartThrowHappy");
                }
                //Si ya lo presionó
                else{
                    pressSecond.Play();
                }
                
               
            }

            else if(pressColor== "blue"){
                //Si no lo ha presionado antes
                if(bluePress==0){
                //Activo audio correcto
                    correctAudio = correctSounds[ Random.Range(0, 5)];
                    correctAudio.PlayDelayed(audioCPress.clip.length);
                    correctAudioSound.PlayDelayed(audioCPress.clip.length);

                    
                    //Cambio la imagen dependiendo del nivel
                    throwDart(pressColor,1);
                    
                    //Aumento el numero de la combinación
                    combNumber++;
                    //Hago la animación de Coco
                    cocoOBJ_AN.Play("cocoDartThrowHappy");
                }
                //Si ya lo presionó
                else{
                    pressSecond.Play();
                }
               
            }

            else if(pressColor== "red"){
                //Si no lo ha presionado antes
                if(redPress==0){
                    //Activo audio correcto
                    correctAudio = correctSounds[ Random.Range(0, 5)];
                    correctAudio.PlayDelayed(audioCPress.clip.length);
                    correctAudioSound.PlayDelayed(audioCPress.clip.length);

                    
                    //Cambio la imagen dependiendo del nivel
                    throwDart(pressColor,1);
                    
                    //Aumento el numero de la combinación
                    combNumber++;
                    //Hago la animación de Coco
                    cocoOBJ_AN.Play("cocoDartThrowHappy");
                    }
                //Si ya lo presionó
                else{
                    
                    pressSecond.PlayDelayed(audioCPress.clip.length);
                }
             
            }

            




            //Si no ha acabado los niveles y ya terminó la combinación, genera otro número
            if (combNumber == 2)
            {
                //Aumento el nivel
                level++;
                if (level < 4)
                {
                    //Vuelvo a generar un color solicitado
                    requestedColor();
                }
                else
                {
                    Debug.Log("Pasa pagina");
                    //Para que se muestre la pantalla de fin del juego:
                    StartCoroutine(database.PushResult(subject, levelBD, (level - 1), errorCount, (int)totalGameTime));
                    panelGameFinished.SetActive(true);
                    //gameOver_OBJ.SetActive(true);
                }
                //Reinicio el contador de la combinación
                combNumber = 0;
                //Reseteo el numero de errores
                errorCount = 0;
                //Reinicio los contadores de colores
                yellowPress = 0;
                bluePress = 0;
                redPress = 0;
                Debug.Log("level " + level);
            }



        }


        //Si presiona el incorrecto
        else
        {
            //Hago la animación de Coco
            cocoOBJ_AN.Play("cocoDartThrowSad");
            //Activo audio incorrecto

            incorrectAudio = incorrectSounds[ Random.Range(0, 3)];
            incorrectAudio.PlayDelayed(audioCPress.clip.length);
            incorrectAudioSound.PlayDelayed(audioCPress.clip.length);


            Debug.Log("INCORRECTO");
            //Aumento numero de errores
            errorCount++;
            //Si en algun momento llego a los 3 errores
            if (errorCount == 3)
            {
                Debug.Log("SE TE ACABARON LOS INTENTOS :(" + level);
                //Para que se muestre la pantalla de fin del juego:
                StartCoroutine(database.PushResult(subject, levelBD, (level - 1), errorCount, (int)totalGameTime));
                panelGameFinished.SetActive(true);
                //gameOver_OBJ.SetActive(true);
            }
            //Cambio la imagen dependiendo del nivel
            throwDart(pressColor, 0);
            //Activamos sonido de color otra vez
            audioColor.PlayDelayed(incorrectAudio.clip.length + audioCPress.clip.length);


        }
    }


    //Método que me permite saber el color solicitado y los colores que lo generan
    public void requestedColor()
    {

        //Este método me permite saber si ya salió ese número
        //Recibe el arreglo, y la cantidad de numeros a generar
        colorsArrayCh[iteration] = randomGenerate(colorsArrayCh, 3);
        iteration++;
        //Recorremos el arreglo de colores para devolver el color solicitado

        for (int n = 0; n < colorsArray.Length; n++)
        {
            //Si encuentra la posición
            if (n == randomNumber)
            {
                reqColor = colorsArray[n];
                Debug.Log("RN: " + randomNumber + "CL: " + colorsArray[n]);
            }
        }
        //si es naranja
        if (reqColor == "orange")
        {

            audioColor = orangeAudio;
            //Colores que lo generan
            reqColorComb1 = "red";
            reqColorComb2 = "yellow";
        }
        //si es verde
        else if (reqColor == "green")
        {
            audioColor = greenAudio;
            //Colores que lo generan
            reqColorComb1 = "blue";
            reqColorComb2 = "yellow";
        }
        //si es purpura
        else if (reqColor == "purple")
        {
            audioColor = purpleAudio;
            //Colores que lo generan
            reqColorComb1 = "blue";
            reqColorComb2 = "red";
        }
        //Activamos sonido de color


            //Si es otro nivel diferente al 1 hay que esperar a que pase el audio
        if( level!=1){
            audioColor.PlayDelayed(correctAudio.clip.length + audioCPress.clip.length);
        }else{

            audioColor.Play();
        }

    }


    //Método que permite animar el dardo
    //Recibe un color y también una variable que le indica si fue correcto o incorrecto
    //1 es correcto y 0 incorrecto
    //Si es correcto explota un globo del color
    //Si es incorrecto falla
    public void throwDart(string color, int num)
    {


        if (num == 1)
        {
            //Si presionó el amarillo y no lo ha presionado
            if (color == "yellow" && yellowPress == 0)
            {
                //Busco aleatorio
                randomNumber2 = randomGenerateString(yellowAnimations, 4, yellowAnimationsDone);
                //Hago una animación aleatoria
                dartObj_AN.Play(yellowAnimations[randomNumber2]);
                //Asigno que ya se utilizó
                yellowAnimationsDone[iterationY] = yellowAnimations[randomNumber2];
                //Aumento iteración
                iterationY++;
                //Desaparezco el globo
                yellowBallonObj_ARR[randomNumber2].Play("disappear");
                yellowPress++;

            }
            else if (color == "blue" && bluePress == 0)
            {
                //Busco aleatorio
                randomNumber2 = randomGenerateString(blueAnimations, 4, blueAnimationsDone);
                //Reproduzco la animación del dardo a ese globo
                dartObj_AN.Play(blueAnimations[randomNumber2]);
                //Asigno que ya se utilizó
                blueAnimationsDone[iterationB] = blueAnimations[randomNumber2];
                //Aumento iteración
                iterationB++;
                //Desaparezco el globo
                blueBallonObj_ARR[randomNumber2].Play("disappear");
                bluePress++;

            }
            else if (color == "red" && redPress == 0)
            {
                //Busco aleatorio
                randomNumber2 = randomGenerateString(redAnimations, 4, redAnimationsDone);
                dartObj_AN.Play(redAnimations[randomNumber2]);
                //Asigno que ya se utilizó
                redAnimationsDone[iterationR] = redAnimations[randomNumber2];
                //Aumento iteración
                iterationR++;
                //Desaparezco el globo
                redBallonObj_ARR[randomNumber2].Play("disappear");
                redPress++;

            }



        }
        else if (num == 0)
        {
            dartObj_AN.Play(wrongAnimations[randomNumber2]);
        }
    }

    //Método para generar los aleatorios y confirmar que no salgan
    public int randomGenerate(int[] array, int number)
    {
        //Genero el número aleatorio
        randomNumber = Random.Range(0, number);

        for (int i = 0; i < array.Length; i++)
        {

            //Si ya salió
            while (randomNumber == array[i])
            {

                //Vuelvalo a generar
                randomNumber = Random.Range(0, number);
                //Doble confirmación
                for (int o = 0; o < array.Length; o++)
                {
                    while (randomNumber == array[i])
                    {

                        //Vuelvalo a generar
                        randomNumber = Random.Range(0, number);
                    }
                }
            }
        }

        return (randomNumber);
    }

    //Método para generar aleatorios y confirmar que no salgan CON STRING
    public int randomGenerateString(string[] array, int number, string[] arrayCH)
    {
        //Genero el número aleatorio
        randomNumber = Random.Range(0, number);
        //Guardo esa animacion
        animationRandom = array[randomNumber];
        //Busco en el arreglo de animaciones esa
        Debug.Log(animationRandom);

        for (int i = 0; i < arrayCH.Length; i++)
        {


            //Si ya salió
            while (string.Equals(animationRandom, arrayCH[i]))
            {
                Debug.Log("LLEGO AL IGUAL");
                //Vuelvalo a generar
                randomNumber = Random.Range(0, number);
                animationRandom = array[randomNumber];
                //Doble confirmación
                for (int o = 0; o < arrayCH.Length; o++)
                {
                    while (string.Equals(animationRandom, arrayCH[i]))
                    {
                        Debug.Log("LLEGO AL WHILE");
                        //Vuelvalo a generar
                        randomNumber = Random.Range(0, number);
                        animationRandom = array[randomNumber];
                    }
                }
            }
            Debug.Log(i + " " + animationRandom);
        }


        return (randomNumber);
    }
}


