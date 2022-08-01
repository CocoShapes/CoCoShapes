using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentPanel : MonoBehaviour
{
    private DatabaseController database;
    private Root root = new Root();

    public GameObject PrefabStudent;
    public GameObject PrefabResult;
    private GameObject content;

    public GameObject panelDetails;

    private int nullRoot = 0;

    void OnEnable()
    {
        StartCoroutine(callDatabase());
    }

    void OnDisable()
    {
        nullRoot = 0;

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator callDatabase()
    {
        float time = 0;
        
        database = GameObject.Find("Database").GetComponent<DatabaseController>();
        content = GameObject.Find("Content");

        StartCoroutine(database.GetAllStudents());

        while(time < 1f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if(database.root.documents.Count > 0 && nullRoot == 0)
        {
            root = database.root;   
            ShowStudents();
            nullRoot = 1;
        }
    }

    private void ShowStudents()
    {
        foreach(Student student in root.documents)
        {
            GameObject studentObject = Instantiate(PrefabStudent, transform);
            studentObject.transform.SetParent(content.transform);

            Button btn = studentObject.AddComponent(typeof(Button)) as Button;

            btn.onClick.AddListener(() => {
                panelDetails.SetActive(true);

                panelDetails.transform.Find("NameResult").GetComponent<Text>().text = student.name + " " + student.lastName;
                panelDetails.transform.Find("GradeResult").GetComponent<Text>().text = student.grade;

                foreach(Result result in student.results)
                {
                    GameObject resultObject = Instantiate(PrefabResult, transform);

                    GameObject contentResults = GameObject.Find("ContentResults");
                    resultObject.transform.SetParent(contentResults.transform);

                    resultObject.transform.Find("Subject").GetComponent<Text>().text = result.subject;
                    resultObject.transform.Find("Level").GetComponent<Text>().text = result.level.ToString();
                    resultObject.transform.Find("Hits").GetComponent<Text>().text = result.hits.ToString();
                    resultObject.transform.Find("Misses").GetComponent<Text>().text = result.misses.ToString();
                    resultObject.transform.Find("Time").GetComponent<Text>().text = result.time.ToString();
                    resultObject.transform.Find("Date").GetComponent<Text>().text = result.date;
                }
            });
            
            studentObject.transform.Find("Name").GetComponent<Text>().text = student.name;
            studentObject.transform.Find("LastName").GetComponent<Text>().text = student.lastName;
            studentObject.transform.Find("Grade").GetComponent<Text>().text = student.grade;

            if(student.results.Count > 0)
            {
                int count = student.results.Count;
                Result result = student.results[count - 1];
                
                string results = result.hits +  "/" + result.misses;
                
                studentObject.transform.Find("Last Result").GetComponent<Text>().text = results;
                
            }
            else
            {
                studentObject.transform.Find("Last Result").GetComponent<Text>().text = "No results";
            }
        }
    }
}
