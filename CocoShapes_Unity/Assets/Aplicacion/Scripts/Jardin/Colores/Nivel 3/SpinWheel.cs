using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    public WheelController wheelController;

    public AnswerController answerController;

    //Para array de colores
    public GameObject[] ObjectColors;
    private float angle;
    private float speed;

    private float rotationTime;
    private float recorredTime;

    public GameObject Wheel2;

    //Variable selector
    public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    float distanceMin;

    public string AnswerCorrect; //la que el usuario deberia presionar



    //Para lo de los sonidos
    public AudioClip[] sounds = new AudioClip[10];
    public AudioControl1 audioSource;

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

        //Ruleta 2 tome el mismo angulo de la ruleta que gira (ruleta 1)
        Wheel2.SetActive(true);
        Wheel2.transform.rotation = transform.rotation;

        //Para el array de colores SABER QUE COLOR ESTÁ MÁS CERCA


        for (int i = 0; i < ObjectColors.Length; i++)
        {
            //Calculo la distancia entre el color y el selector
            float distance = Vector3.Distance(Selector.transform.position, ObjectColors[i].transform.position);
            Debug.Log("Color" + ObjectColors[i] + "Distance: " + distance);

            //Calcular de distance la mas pequeña

            if (i == 0)
            {
                distanceMin = distance;

            }
            else
            {
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    StartCoroutine(audioSource.PlayAudio(sounds[i]));
                    answerController.AnswerCorrect = ObjectColors[i].name;

                }
            }

        }

        Debug.Log("DistanceMin: " + distanceMin);

        //Desactiva la ruleta que gira
        this.gameObject.SetActive(false);
        yield return null;
    }

    void OnEnable()
    {
        speed = Random.Range(250f, 280f);
        angle = Random.Range(0f, 360f);

        rotationTime = Random.Range(4f, 5f);
        recorredTime = 0f;

        transform.Rotate(0, 0, angle);

        StartCoroutine(Rotate());

    }
}


















