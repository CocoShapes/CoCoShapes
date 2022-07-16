using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovementSha : MonoBehaviour
{
    public AnswerControlSha answerControlSha;

    //Para el movimiento del mouse
    public float rate;//para que el movimiento se mueva más o menos

    //Para la respuestas
    public string AnswerCorrect;//la que el usuario deberia presionar

    //Para el array de los objetos vacíos que ayudan a obtener la distancia de cada objeto.
    public GameObject[] Shapes;
    public GameObject Selector;

    //Para calcular la distancia más cercana al selector
    float distanceMin;

    //Para que se sepa si se presionó una tecla
    public bool isPressing;

    void Start()
    {
        isPressing = false;
    }
    void Update()
    {
        //Para el movimiento del mouse
        rate = 1;
        Vector2 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = pz / rate;

        //Opción 1:
        // if (Input.touchCount > 0)
        // {
        //     Vector2 pz2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //     gameObject.transform.position = pz2 / rate;
        // }

        //Opción 2:
        // if (Input.touchCount > 0)
        // {
        //     // The screen has been touched so store the touch

        //     Touch touch = Input.GetTouch(0);

        //     if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        //     {

        //         // If the finger is on the screen, move the object smoothly to the touch position

        //         Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.0f));

        //         gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(touchPosition.x, 0.0f, touchPosition.z), Time.deltaTime * 5.0f);
        //     }

        // }

        //Opción 3:
        // if (Input.touchCount > 0)
        // {
        //     _touch = Input.GetTouch(0); // screen has been touched, store the touch 

        //     if (_touch.phase == TouchPhase.Began)
        //     {
        //         isDragging = true;

        //         offset = Camera.main.ScreenToWorldPoint(new Vector2(_touch.position.x, _touch.position.y)) - gameObject.transform.position;

        //     }
        //     else if (_touch.phase == TouchPhase.Ended)
        //     {
        //         offset = Vector2.zero;
        //         isDragging = false;
        //     }

        // }

        // if (isDragging)
        // {
        //     Vector2 _dir = Camera.main.ScreenToWorldPoint(new Vector2(_touch.position.x, _touch.position.y));
        //     _dir = _dir - offset;

        //     gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, _dir, Time.deltaTime * speed);

        // }

        //Para definir las respuestas correctas
        //para que no se mueva mas
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < Shapes.Length; i++)
            {
                //Para calcular la distancia entre el objeto y el selector
                float distance = Vector3.Distance(Selector.transform.position, Shapes[i].transform.position);
                //Debug.Log("Objeto" + Objects[i] + "Distance: " + distance);

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
                        //Para saber el nombre del objeto 
                        answerControlSha.AnswerCorrect = Shapes[i].name;
                    }
                }
            }
            Debug.Log("DistanceMin: " + distanceMin);
        }
    }
}
