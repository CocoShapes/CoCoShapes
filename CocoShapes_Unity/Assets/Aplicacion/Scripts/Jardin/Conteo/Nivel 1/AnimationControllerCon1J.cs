using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerCon1J : MonoBehaviour
{
    private LevelControllerCont1J levelController;

    void Start(){
        levelController = GameObject.Find("LevelController").GetComponent<LevelControllerCont1J>();
    }
    
    public IEnumerator MoveCar(GameObject car, GameObject rail, float lerpDuration)
    {
        float timeElapsed = 0;
        float dist = Vector2.Distance(car.transform.position, rail.transform.position);
        float step = dist / (lerpDuration * 150);

        while (timeElapsed < lerpDuration)
        {
            car.transform.position = Vector3.Lerp(car.transform.position, rail.transform.position, step);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        levelController.hasReponse = true;
    }
}
