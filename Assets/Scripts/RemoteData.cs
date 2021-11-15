using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    public int id { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public string avt { get; set; }
    public UserStat userStat { get; set; }
    public List<AnimalStat> animalStats { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}

[System.Serializable]
public class Data
{
    public User user { get; set; }
}

[System.Serializable]
public class Root
{
    public int code { get; set; }
    public string msg { get; set; }
    public Data data { get; set; }
}

[System.Serializable]
public class AuthInfo
{
    public string email { get; set; }
    public string password { get; set; }

    public AuthInfo(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}