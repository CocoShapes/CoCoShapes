using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Admin : MonoBehaviour
{
    //Database
    private DatabaseController database;

    //Add Students UI Elements
    public InputField nameInput;
    public InputField lastNameInput;
    public Dropdown gradeDropdown;
    public Text txtRetroalimentation;

    //List Students UI Elements
    public GameObject panelListAllStudents;

    //Level Selector
    public LevelSelector levelSelector;

    void Start()
    {
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
    }
    
    public void ActivateScreen(GameObject screen)
    {
        screen.SetActive(true);
    }

    public void DeactivateScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    public void AddStudent()
    {
        string name = nameInput.text;
        string lastName = lastNameInput.text;
        string grade = gradeDropdown.captionText.text;

        if(name != "" && lastName != "" && grade != "")
        {
            StartCoroutine(database.AddStudent(name, lastName, grade));
            
            txtRetroalimentation.gameObject.SetActive(true);
            txtRetroalimentation.text = "Student Added Successfully";

            nameInput.text = "";
            lastNameInput.text = "";

            GameObject screen = GameObject.Find("Students Panel");
            screen.SetActive(false);
            screen.SetActive(true);
        }else 
        {
            txtRetroalimentation.gameObject.SetActive(true);
            txtRetroalimentation.text = "Please fill all the fields";
        }
    }

    public void ListAllStudents(string grade)
    {
        ActivateScreen(panelListAllStudents);

        StudentList studenlist = panelListAllStudents.GetComponent<StudentList>();
        studenlist.grade = grade;
    }

    public void DefineGrade(string grade)
    {
        levelSelector.grade = grade;
    }

    public void DefineSubject(string subject)
    {
        levelSelector.subject = subject;
    }
}
