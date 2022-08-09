using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public Image PlayButton;
    public Image PauseButton; 
    public Image QuitButton;

    public GameObject QuitPanel;

    public void PauseGame()
    {
        PlayButton.gameObject.SetActive(true);
        this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        Time.timeScale = 0;
        PauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        PauseButton.gameObject.SetActive(true);
        this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        Time.timeScale = 1;
        PlayButton.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 0;
        QuitButton.color = new Color(0, 0, 0, 0f);
        PlayButton.color = new Color(0, 0, 0, 0f);
        PauseButton.color = new Color(0, 0, 0, 0f);
        this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        QuitPanel.SetActive(true);
    }

    public void ConfirmQuit()
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
    
    public void CancelQuit()
    {
        Time.timeScale = 1;
        QuitButton.color = new Color(255, 255, 255, 1f);
        PlayButton.color = new Color(255, 255, 255, 1f);
        PauseButton.color = new Color(255, 255, 255, 1f);
        this.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        QuitPanel.SetActive(false);
    }
}
