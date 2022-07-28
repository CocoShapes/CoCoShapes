using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentDetails : MonoBehaviour
{
    private Text studentName;
    private Text grade;
    private GameObject contentResults;
    
    void Start()
    {
        studentName = GameObject.Find("NameResult").GetComponent<Text>();
        grade = GameObject.Find("GradeResult").GetComponent<Text>();

        contentResults = GameObject.Find("ContentResults");
    }
    
    void OnDisable()
    {
        studentName.text = "";
        grade.text = "";

        foreach (Transform child in contentResults.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
