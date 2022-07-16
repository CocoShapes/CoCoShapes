using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject Wheel;//Ruleta 1
    public GameObject Wheel2;//Ruleta 2

    public GameObject RedIncorrect;
    public GameObject GreenIncorrect;
    public GameObject YellowIncorrect;
    public GameObject BlackIncorrect;
    public GameObject OrangeIncorrect;
    public GameObject BlueIncorrect;
    public GameObject PurpleIncorrect;
    public GameObject WhiteIncorrect;

    void Update()
    {
        //Para que se vuelva a activar la rotación de la ruleta
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Para que se desactiven los círculos rojos
            RedIncorrect.SetActive(false);
            GreenIncorrect.SetActive(false);
            YellowIncorrect.SetActive(false);
            BlackIncorrect.SetActive(false);
            OrangeIncorrect.SetActive(false);
            BlueIncorrect.SetActive(false);
            PurpleIncorrect.SetActive(false);
            WhiteIncorrect.SetActive(false);
            Wheel.SetActive(Wheel.activeSelf ? false : true);
            Wheel2.SetActive(Wheel2.activeSelf ? true : false);
        }
    }
}

