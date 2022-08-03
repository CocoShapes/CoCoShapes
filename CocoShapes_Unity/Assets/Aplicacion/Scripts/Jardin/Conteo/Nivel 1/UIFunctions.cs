using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    public LevelControllerCont1J levelController;
    private AudioControllerCon1J audioController;

    void Start()
    {
        audioController = GameObject.Find("AudioController").GetComponent<AudioControllerCon1J>();
    }

    public void checkAnswer(Text txt){
        string answer = txt.text;

        AudioClip[] numberAudio = new AudioClip[1];
        switch(answer)
        {
            case "2":
                numberAudio[0] = levelController.audioClips[1];
                break;
            case "3":
                numberAudio[0] = levelController.audioClips[2];
                break;
            case "4":
                numberAudio[0] = levelController.audioClips[3];
                break;
            case "5":
                numberAudio[0] = levelController.audioClips[4];
                break;
            case "6":
                numberAudio[0] = levelController.audioClips[5];
                break;
            case "7":
                numberAudio[0] = levelController.audioClips[6];
                break;
            case "8":
                numberAudio[0] = levelController.audioClips[7];
                break;
            case "9":
                numberAudio[0] = levelController.audioClips[8];
                break;
            case "10":
                numberAudio[0] = levelController.audioClips[9];
                break;
        }

        StartCoroutine(audioController.PlayAudio(numberAudio));

        if(answer == levelController.correctAnswer.ToString()){
            StartCoroutine(levelController.goodAnswer());
        }
        else{
            StartCoroutine(levelController.badAnswer());
        }
    }
}
