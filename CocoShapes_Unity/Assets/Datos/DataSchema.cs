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
    public string theme;
    public int level;
    public int numOfCorrectAnswers;
    public int numOfBadAnswers;
    public int requiredTime;

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