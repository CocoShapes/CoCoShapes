using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    public string color;
    public bool playedSound = false;

    Vector3 ballPosition = new Vector3(-5.52f, -0.45f, 0); 
    
    public IEnumerator launch()
    {
        float timeElapsed = 0;
        float lerpDuration = 0.8f;

        //Activar Sprite Renderer de la bola
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = ballPosition;

        string colorGameObject = "Home" + color;
        Transform colorTransform = GameObject.Find(colorGameObject).transform;

        float dist = Vector2.Distance(transform.position, colorTransform.position);
        float step = dist / (lerpDuration * 50);

        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(transform.position, colorTransform.position, step);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //Desactivar Sprite Renderer de la bola
        GetComponent<SpriteRenderer>().enabled = false;
        
        if (!playedSound)
            {
                playedSound = true;
                GetComponent<AudioSource>().Play();
            }

        //Activar Animación de la casa

    }
}
