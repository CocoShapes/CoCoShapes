using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SequenceController : MonoBehaviour
{
    public static SequenceController InstanceSequence;

    public bool gamePlayed = false;
    public string gradeMap;
    
    void Awake()
    {
        if(InstanceSequence)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            InstanceSequence = this;
        }
    }
}
