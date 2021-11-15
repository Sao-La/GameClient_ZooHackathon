using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalStat
{
    public int earnRate;
    public int animalId;
    public DateTime lifeEnd;
    public int user;
    public DateTime createdAt;
    public DateTime updatedAt;

    public AnimalStat()
    {
        animalId = 0;
        createdAt = DateTime.Now;
        earnRate = 1;
        lifeEnd = DateTime.Now.AddSeconds(Global.GAME_INTERVAL);
    }
}
