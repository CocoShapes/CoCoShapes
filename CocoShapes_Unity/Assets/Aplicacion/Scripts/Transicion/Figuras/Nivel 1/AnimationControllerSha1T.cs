using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerSha1T : MonoBehaviour
{
    public IEnumerator MoveCoco(GameObject Coco, GameObject Target){
        float durationTime = 1.5f;
        float recorredTime = 0;

        while(recorredTime < durationTime){
            recorredTime += Time.deltaTime;
            Coco.transform.position = Vector3.Lerp(Coco.transform.position, new Vector3(Target.transform.position.x, Coco.transform.position.y, Coco.transform.position.z), Time.deltaTime);
            yield return null;
        }
    }
}
