using System.Linq;
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

    public void UpdateStudent()
    {
        PanelUpdate.SetActive(true);

        foreach(Student student in database.root.documents)
        {
            if(student._id == studentId)
            {
                PanelUpdate.transform.Find("InputName").GetComponent<InputField>().text = student.name;
                PanelUpdate.transform.Find("InputLastName").GetComponent<InputField>().text = student.lastName;
                PanelUpdate.transform.Find("Dropdown").GetComponent<Dropdown>().value = student.grade=="Garden" ? 0 : 1;
            }
        }
    }

    public void ConfirmUpdateStudent()
    {
        string name = PanelUpdate.transform.Find("InputName").GetComponent<InputField>().text.Trim();
        string lastName = PanelUpdate.transform.Find("InputLastName").GetComponent<InputField>().text.Trim();
        string grade = PanelUpdate.transform.Find("Dropdown").GetComponent<Dropdown>().value == 0 ? "Garden" : "Transition";
        
        if(name != "" && lastName != "" && !name.Any(char.IsDigit) && !lastName.Any(char.IsDigit))
        {
            StartCoroutine(database.UpdateStudent(studentId, name, lastName, grade));
            PanelUpdate.SetActive(false);
            
            GameObject screen = GameObject.Find("Students Panel");
            screen.SetActive(false);
            screen.SetActive(true);

            this.gameObject.SetActive(false);
        }else{
            PanelUpdate.transform.Find("retroalimentacion").gameObject.SetActive(true);
        }
    }
}
