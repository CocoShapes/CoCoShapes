using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentList : MonoBehaviour
{
    private DatabaseController database;

    private Root root;
    public string grade;

    public GameObject gardenMap;
    public GameObject transitionMap;

    public Dropdown gradeDropdown;

    void OnEnable()
    {
        StartCoroutine(CreateList());
    }

    private IEnumerator CreateList()
    {
        float time = 0;
        
        database = GameObject.Find("Database").GetComponent<DatabaseController>();

        StartCoroutine(database.GetAllStudents());

        gradeDropdown.ClearOptions();
        
        while(time < 1f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        
        root = database.root;
        
        List<string> options = new List<string>();

        foreach(Student student in root.documents)
        {
            
            if(student.grade == grade)
            {
                options.Add(student.name + " " + student.lastName);
            }
        }
        
        gradeDropdown.AddOptions(options);
    }

    public void StartGame()
    {
        foreach(Student student in root.documents)
        {
            if(student.name + " " + student.lastName == gradeDropdown.captionText.text)
            {
                database.studentId = student._id;
            }
            if(grade == "Garden")
            {
                gardenMap.SetActive(true);
            }
            if(grade == "Transition")
            {
                transitionMap.SetActive(true);
            }
        }
    }
}
