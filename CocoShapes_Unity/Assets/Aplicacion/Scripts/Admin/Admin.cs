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

    //Audio of Buttons
    private AudioSource audioSource;

    //Login UI Elements
    public InputField inputFieldTextValue;
    public Text loginConfirmationText;
    public GameObject connectionPanel;
    public GameObject loginPanel;

    void Start()
    {
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void ActivateScreen(GameObject screen)
    {
        screen.SetActive(true);
        audioSource.Play();
    }

    public void DeactivateScreen(GameObject screen)
    {
        screen.SetActive(false);
        audioSource.Play();
    }

    public void DeactivateRetroalimentationText()
    {
        txtRetroalimentation.text = "";
    }

    public void AddStudent()
    {
        audioSource.Play();
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

    public void Login()
    {
        string pin = PlayerPrefs.GetString("Pin");
        string pass = inputFieldTextValue.text;

        if(pin != "") {
            if(pin == pass){
                ActivateScreen(connectionPanel);
                DeactivateScreen(loginPanel);
            }else{
                loginConfirmationText.text = "Incorrect Pin";
            }
        }else {
            if(pass.Length == 4)
            {
                PlayerPrefs.SetString("Pin", pass);
                loginConfirmationText.text = "Pin Defined Successfully";
                ActivateScreen(connectionPanel);
                DeactivateScreen(loginPanel);
            }else{
                loginConfirmationText.text = "Pin must be 4 digits";
            }
        }
    }
}
