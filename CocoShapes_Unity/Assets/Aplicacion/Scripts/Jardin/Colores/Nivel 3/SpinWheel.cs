using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    //Para llamar al código denominado WheelController para la rotación.
    public WheelController wheelController;
    //Para llamar al código denominado AnswerController para poder usar el answerCorrect en ese código.
    public AnswerController answerController;

    //Para el array de los objetos vacíos que ayudan a obtener la distancia de cada color.
    public GameObject[] ObjectColors;

    //Para la rotación
    private float angle;
    private float speed;
    private float rotationTime;
    private float recorredTime;
    public GameObject Wheel2;//La ruleta 2 

    //Variable selector
    public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    float distanceMin;

    //Para las respuestas
    public string AnswerCorrect; //la que el usuario debería presionar

    //Para los audios
    public AudioClip[] sounds = new AudioClip[12];
    public AudioControl1 audioSource;

    //Método para la rotación de la ruleta.
    public IEnumerator Rotate()
    {
        //El while se ejecuta mientras el tiempo que debe girarse la ruleta no halla terminado
        while (recorredTime < rotationTime)
        {
            transform.Rotate(0, 0, -speed * Time.deltaTime);
            recorredTime += Time.deltaTime;
            yield return null;
        }
        //Esto es lo que pasa despues de que la ruleta se detiene
        Debug.Log("After rotation, Time: " + recorredTime);

        //Para que la Ruleta 2 tome el mismo angulo de la ruleta que gira (ruleta 1)
        Wheel2.SetActive(true);
        Wheel2.transform.rotation = transform.rotation;

        //PARA SABER QUE COLOR ESTÁ MÁS CERCA DEL SELECTOR
        for (int i = 0; i < ObjectColors.Length; i++)
        {
            //Para calcular la distancia entre el color y el selector
            float distance = Vector3.Distance(Selector.transform.position, ObjectColors[i].transform.position);
            Debug.Log("Color" + ObjectColors[i] + "Distance: " + distance);

            //Para calcular la distancia más pequeña
            if (i == 0)
            {
                distanceMin = distance;
            }
            else
            {
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    //Para saber el nombre del color que está más cerca del selector
                    answerController.AnswerCorrect = ObjectColors[i].name;
                    //Para que se reproduzcan los audios de los colores
                    AudioClip[] soundsToPlay = new AudioClip[1] { sounds[i] };
                    StartCoroutine(audioSource.PlayAudio(soundsToPlay));
                }
            }
        }
        Debug.Log("DistanceMin: " + distanceMin);

        //Para desactivar la ruleta que gira
        this.gameObject.SetActive(false);
        yield return null;
    }

    //Para la rotación de la ruleta.
    void OnEnable()
    {
        speed = Random.Range(250f, 280f);
        angle = Random.Range(0f, 360f);

        rotationTime = Random.Range(4f, 5f);
        recorredTime = 0f;

        transform.Rotate(0, 0, angle);

        //Para que no se repitan los colores


        StartCoroutine(Rotate());
    }
}


















