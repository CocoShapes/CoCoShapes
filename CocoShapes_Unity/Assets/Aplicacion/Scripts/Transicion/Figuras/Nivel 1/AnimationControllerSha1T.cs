using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerSha1T : MonoBehaviour
{
    public IEnumerator MoveCoco(GameObject Coco, GameObject Target){
        float durationTime = 0.66f;
        float recorredTime = 0;

        Animator cocoAnimator = Coco.GetComponent<Animator>();
        cocoAnimator.Play("Salto");

        while(recorredTime < durationTime){
            recorredTime += Time.deltaTime;
            Coco.transform.position = Vector3.Lerp(Coco.transform.position, new Vector3(Target.transform.position.x + 2f, Coco.transform.position.y, Coco.transform.position.z), Time.deltaTime);
            yield return null;
        }
    }
}
