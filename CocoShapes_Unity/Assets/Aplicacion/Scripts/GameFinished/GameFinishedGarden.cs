using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedGarden : MonoBehaviour
{
    public void Home()
    {
        SequenceController sequence = GameObject.Find("SequenceController").GetComponent<SequenceController>();
        sequence.gamePlayed = true;

        string sceneName = SceneManager.GetActiveScene().name;
        char lastCharacter = sceneName[sceneName.Length-1];
        if(lastCharacter.ToString() == "J"){
            sequence.gradeMap = "Garden";
        }else {
            sequence.gradeMap = "Transition";
        }
        
        SceneManager.LoadScene("Admin");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
