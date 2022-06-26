using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    public string color;
    public bool playedSound = false;
    
    void Update()
    {
        float speed = 7.5f;

        string colorGameObject = "Home" + color;
        Transform colorTransform = GameObject.Find(colorGameObject).transform;

        transform.position = Vector3.Lerp(transform.position, colorTransform.position, Time.deltaTime * speed);

        if (transform.position == colorTransform.position)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            colorTransform.Rotate(new Vector3(300 * Time.deltaTime, 0, 0));
            if (!playedSound)
            {
                playedSound = true;
                GetComponent<AudioSource>().Play();
            }

            if(colorTransform.rotation.eulerAngles.x >= 85)
            {
                this.gameObject.SetActive(false);
            }
            
        }
    }
}
