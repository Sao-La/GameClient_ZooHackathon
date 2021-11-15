using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UserStat
{
    public int id;
    public long money;
    public int x;
    public int y;
    public string username;
    public int user;
    public DateTime createdAt;
    public DateTime updatedAt;

    public UserStat()
    {
        createdAt = DateTime.Now;
        money = 0;
        username = "";
        x = y = 0;
    }
}
