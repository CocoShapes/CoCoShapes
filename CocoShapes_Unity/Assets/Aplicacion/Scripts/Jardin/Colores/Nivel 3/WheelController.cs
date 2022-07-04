using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject Wheel;
    public GameObject Wheel2;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Wheel.SetActive(Wheel.activeSelf ? false : true);
            Wheel2.SetActive(Wheel2.activeSelf ? true : false);
        }
    }
}

