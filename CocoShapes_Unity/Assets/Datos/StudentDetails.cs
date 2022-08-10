using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentDetails : MonoBehaviour
{
    private Text studentName;
    private Text grade;
    private GameObject contentResults;

    public string studentId;
    private DatabaseController database;
    public GameObject PanelDelete;
    public GameObject PanelUpdate;

    
    void Start()
    {
        database = GameObject.Find("Database").GetComponent<DatabaseController>();

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

    public void DeleteStudentButton()
    {
        PanelDelete.SetActive(true);
    }

    public void ConfirmDeleteStudet()
    {
        StartCoroutine(database.DeleteStudent(studentId));
        PanelDelete.SetActive(false);
        
        GameObject screen = GameObject.Find("Students Panel");
        screen.SetActive(false);
        screen.SetActive(true);

        this.gameObject.SetActive(false);
    }

    public void CancelDeleteStudent()
    {
        PanelDelete.SetActive(false);
    }
}
