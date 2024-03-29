using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseController : MonoBehaviour
{
    public static DatabaseController InstanceDatabase;
    
    public Root root;
    public string studentId;

    private string apikey = "v4CVz0hPeX5F5xr4wxR1IhcL0IB8RZoyd8LSFIJBSOCq00mcQDLrjyRve1Dg5ozm";
    
    void Awake()
    {
        if(InstanceDatabase)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            InstanceDatabase = this;
        }
    }
    
    public IEnumerator GetAllStudents()
    {
        string url = "https://data.mongodb-api.com/app/data-takdq/endpoint/data/v1/action/find";
        string json = "{\"collection\":\"students\",\"database\":\"lafontaine\",\"dataSource\":\"maincluster\"}";

        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", apikey);

        yield return request.SendWebRequest();

        root = Root.CreateFromJSON(request.downloadHandler.text);
    }

    public IEnumerator AddStudent(string Name, string LastName, string Grade)
    {
        string url = "https://data.mongodb-api.com/app/data-takdq/endpoint/data/v1/action/insertOne";
        string json = "{\"collection\":\"students\",\"database\":\"lafontaine\",\"dataSource\":\"maincluster\",\"document\":{\"name\":\"" + Name + "\",\"lastName\":\"" + LastName + "\",\"grade\":\"" + Grade + "\", \"results\": []}}";   

        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", apikey);

        yield return request.SendWebRequest();

    }

    private IEnumerator FindStudent(string url, string id)
    {
       string json = "{\"collection\":\"students\",\"database\":\"lafontaine\",\"dataSource\":\"maincluster\",\"filter\":{\"_id\":{\"$oid\":\"" + id + "\"}}}";
        
        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", apikey);

        yield return request.SendWebRequest();
    }

    public IEnumerator PushResult(string subject, int level, int hits, int misses, int requiredTime)
    {
        string url = "https://data.mongodb-api.com/app/data-takdq/endpoint/data/v1/action/updateOne";
        string json = "{\"collection\":\"students\",\"database\":\"lafontaine\",\"dataSource\":\"maincluster\",\"filter\":{\"_id\":{\"$oid\":\"" + studentId + "\"}},\"update\":{\"$push\":{\"results\":{\"subject\":\"" + subject + "\",\"level\":" + level + ",\"hits\":" + hits + ",\"misses\":" + misses + ",\"time\":" + requiredTime + ", \"date\":\"" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\"}}}}";
        
        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", apikey);
        
        yield return request.SendWebRequest();
    }

    public IEnumerator DeleteStudent(string _id)
    {
        string url = "https://data.mongodb-api.com/app/data-takdq/endpoint/data/v1/action/deleteOne";
        string json = "{\"collection\":\"students\",\"database\":\"lafontaine\",\"dataSource\":\"maincluster\",\"filter\":{\"_id\":{\"$oid\":\"" + _id + "\"}}}";

        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", apikey);

        yield return request.SendWebRequest();
    }

    public IEnumerator UpdateStudent(string _id, string name, string lastName, string grade)
    {
        string url = "https://data.mongodb-api.com/app/data-takdq/endpoint/data/v1/action/updateOne";
        string json = "{\"collection\":\"students\",\"database\":\"lafontaine\",\"dataSource\":\"maincluster\",\"filter\":{\"_id\":{\"$oid\":\"" + _id + "\"}},\"update\":{\"$set\":{\"name\":\"" + name + "\",\"lastName\":\"" + lastName + "\",\"grade\":\"" + grade + "\"}}}";
        
        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", apikey);
        
        yield return request.SendWebRequest();
    }
}
