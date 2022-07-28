using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    private SequenceController sequence;
    public GameObject panelLogin;
    public GameObject gardenMap;
    public GameObject transitionMap;
    
    void OnEnable()
    {
        sequence = GameObject.Find("SequenceController").GetComponent<SequenceController>();
        
        if(sequence.gamePlayed){
            if(sequence.gradeMap == "Garden"){
                gardenMap.SetActive(true);
                panelLogin.SetActive(false);
            }
            if(sequence.gradeMap == "Transition"){
                transitionMap.SetActive(true);
                panelLogin.SetActive(false);
            }
        }
    }
}
