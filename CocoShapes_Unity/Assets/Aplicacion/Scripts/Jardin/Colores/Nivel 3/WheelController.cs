using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject Wheel;//Ruleta 1
    public GameObject Wheel2;//Ruleta 2

    void Update()
    {
        //Para que se vuelva a activar la rotación de la ruleta
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Wheel.SetActive(Wheel.activeSelf ? false : true);
            Wheel2.SetActive(Wheel2.activeSelf ? true : false);
        }
    }
}

