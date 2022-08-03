using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinController : MonoBehaviour
{
    public InputField pinInputField;
    public Text instructionText;
    // Start is called before the first frame update
    void Start()
    {
        pinInputField.contentType = InputField.ContentType.Pin;
        pinInputField.characterLimit = 4;

        if(PlayerPrefs.GetString("Pin") != "")
        {
            instructionText.text = "Insert your 4 digit pin";
        }
        else
        {
            instructionText.text = "Please, define your 4 digit pin to continue";
        }
    }
}
