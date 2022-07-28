using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public string grade;
    public string subject;
    public int level;

    public Button levelOne;
    public Button levelTwo;
    public Button levelThree;
    public Button levelExp;

    public GameObject panelTutorial;

    void OnEnable()
    {
        if(grade == "Garden")
        {
            if(subject == "Count")
            {
                levelExp.gameObject.SetActive(false);
                levelOne.onClick.AddListener(() => {  level = 1; panelTutorial.SetActive(true); });
                levelTwo.onClick.AddListener(() => {  level = 2; panelTutorial.SetActive(true); });
                levelThree.onClick.AddListener(() => {  level = 3; panelTutorial.SetActive(true); });
            }
            if(subject == "Colors")
            {
                levelExp.gameObject.SetActive(false);
                levelOne.onClick.AddListener(() => {  level = 1; panelTutorial.SetActive(true); });
                levelTwo.onClick.AddListener(() => {  level = 2; panelTutorial.SetActive(true); });
                levelThree.onClick.AddListener(() => {  level = 3; panelTutorial.SetActive(true); });
            }
            if(subject == "Shapes")
            {
                levelExp.gameObject.SetActive(false);
                levelOne.onClick.AddListener(() => {  level = 1; panelTutorial.SetActive(true); });
                levelTwo.onClick.AddListener(() => {  level = 2; panelTutorial.SetActive(true); });
                levelThree.onClick.AddListener(() => {  level = 3; panelTutorial.SetActive(true); });
            }
        }

        if(grade == "Transition")
        {
            if(subject == "Count")
            {
                levelExp.gameObject.SetActive(false);
                levelOne.onClick.AddListener(() => {  level = 1; panelTutorial.SetActive(true); });
                levelTwo.onClick.AddListener(() => {  level = 2; panelTutorial.SetActive(true); });
                levelThree.onClick.AddListener(() => {  level = 3; panelTutorial.SetActive(true); });
            }
            if(subject == "Colors")
            {
                levelExp.gameObject.SetActive(true);
                levelExp.onClick.AddListener(() => {  level = 0; panelTutorial.SetActive(true); });
                levelOne.onClick.AddListener(() => {  level = 1; panelTutorial.SetActive(true); });
                levelTwo.onClick.AddListener(() => {  level = 2; panelTutorial.SetActive(true); });
                levelThree.onClick.AddListener(() => {  level = 3; panelTutorial.SetActive(true); });
            }
            if(subject == "Shapes")
            {
                levelExp.gameObject.SetActive(false);
                levelOne.onClick.AddListener(() => {  level = 1; panelTutorial.SetActive(true); });
                levelTwo.onClick.AddListener(() => {  level = 2; panelTutorial.SetActive(true); });
                levelThree.onClick.AddListener(() => {  level = 3; panelTutorial.SetActive(true); });
            }   
        }
    }
}
