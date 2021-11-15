using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : Singleton<GameDirector>
{
    public UserStat stat;

    private void Awake()
    {
        LoadPlayerFromPref();
    }

    private void Update()
    {
        stat.x = (int)PlayerController.Instance.transform.position.x;
        stat.y = (int)PlayerController.Instance.transform.position.y;
    }

    public void AddMoney(int amount)
    {
        stat.money += amount;
    }

    private void LoadPlayerFromPref()
    {
        stat = SaveSystem.LoadPlayer();
        if (stat == null) stat = new UserStat();
    }

    private void SavePlayerToPref()
    {
        SaveSystem.SavePlayer(stat);
    }

    private void OnApplicationQuit()
    {
        SavePlayerToPref();
    }
}
