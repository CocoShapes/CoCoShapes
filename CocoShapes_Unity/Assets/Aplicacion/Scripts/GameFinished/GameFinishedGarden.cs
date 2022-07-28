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
        sequence.gradeMap = "Transition";
        SceneManager.LoadScene("Admin");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
