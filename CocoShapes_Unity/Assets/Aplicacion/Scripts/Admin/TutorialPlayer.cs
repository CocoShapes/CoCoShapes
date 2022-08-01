using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialPlayer : MonoBehaviour
{
    public VideoClip[] clips = new VideoClip[10]; //ColExpT, Col1T, Col2T, Col3T, Sha1T, Sha2T, Sha3T, Cou1T, Cou2T, Cou3T
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
                        sceneName = "Level2_CouT";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[6];
                        sceneName = "Level3_CouT";
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
            if(levelSelector.subject == "Colors"){
                switch(levelSelector.level){
                    case 1:
                        video.GetComponent<VideoPlayer>().clip = clips[1];
                        sceneName = "Level1_ColJ";
                        break;
                    case 2:
                        video.GetComponent<VideoPlayer>().clip = clips[2];
                        sceneName = "Level2_ColJ";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[3];
                        sceneName = "Level3_ColJ";
                        break;
                }
            }else if(levelSelector.subject == "Count"){
                switch(levelSelector.level){
                    case 1:
                        video.GetComponent<VideoPlayer>().clip = clips[4];
                        sceneName = "Level1_ConJ";
                        break;
                    case 2:
                        video.GetComponent<VideoPlayer>().clip = clips[5];
                        sceneName = "Level2_CouJ";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[6];
                        sceneName = "Level3_CouJ";
                        break;
                }
            }else if(levelSelector.subject == "Shapes"){
                switch(levelSelector.level){
                    case 1:
                        video.GetComponent<VideoPlayer>().clip = clips[7];
                        sceneName = "Level1_ShaJ";
                        break;
                    case 2:
                        video.GetComponent<VideoPlayer>().clip = clips[8];
                        sceneName = "Level2_ShaJ";
                        break;
                    case 3:
                        video.GetComponent<VideoPlayer>().clip = clips[9];
                        sceneName = "Level3_ShaJ";
                        break;
                }
            }
        }
        video.GetComponent<VideoPlayer>().Play();
    }

    public void StartGameScene(){
        SceneManager.LoadScene(sceneName);
    }
}
