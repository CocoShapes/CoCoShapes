using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialPlayer : MonoBehaviour
{
    public VideoClip[] clips = new VideoClip[10]; //Col1, Col2, Col3, Sha1, Sha2, Sha3, Con1, Con2, Con3
    public LevelSelector levelSelector;
    public GameObject video;

    private string sceneName;

    void OnEnable()
    {
        if(levelSelector.grade ==  "Transition"){
            if(levelSelector.subject == "Colors"){
                switch(levelSelector.level){
                    case 0:
                        video.GetComponent<VideoPlayer>().clip = clips[0];
                        sceneName = "LevelExp_ColT";
                        break;
                    case 1:
                        video.GetComponent<VideoPlayer>().clip = clips[1];
                        sceneName = "Level1_ColT";
                        break;
                    case 2:
                        video.GetComponent<VideoPlayer>().clip = clips[2];
                        sceneName = "Level2_ColT";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[3];
                        sceneName = "Level3_ColT";
                        break;
                }
            }else if(levelSelector.subject == "Count"){
                switch(levelSelector.level){
                    case 1:
                        video.GetComponent<VideoPlayer>().clip = clips[4];
                        sceneName = "Level1_ConT";
                        break;
                    case 2:
                        video.GetComponent<VideoPlayer>().clip = clips[5];
                        sceneName = "Level2_ConT";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[6];
                        sceneName = "Level3_ConT";
                        break;
                }
            }else if(levelSelector.subject == "Shapes"){
                switch(levelSelector.level){
                    case 1:
                        video.GetComponent<VideoPlayer>().clip = clips[7];
                        sceneName = "Level1_ShaT";
                        break;
                    case 2:
                        video.GetComponent<VideoPlayer>().clip = clips[8];
                        sceneName = "Level2_ShaT";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[9];
                        sceneName = "Level3_ShaT";
                        break;
                }
            }
        }else {
            Debug.Log("Garden");
        }
        video.GetComponent<VideoPlayer>().Play();
    }

    public void StartGameScene(){
        SceneManager.LoadScene(sceneName);
    }
}
