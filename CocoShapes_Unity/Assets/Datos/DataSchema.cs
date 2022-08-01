using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Student
{
    public string _id;
    public string name;
    public string lastName;
    public string grade;
    public List<Result> results;

    public static Student CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Student>(jsonString);
    }
}

[Serializable]
public class Result
{
    public string subject;
    public int level;
    public int hits;
    public int misses;
    public int time;
    public string date;

    public static Result CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Result>(jsonString);
    }
}

[Serializable]
public class Root
{
    public List<Student> documents;

    public static Root CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Root>(jsonString);
    }
}