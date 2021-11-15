using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer(UserStat data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static UserStat LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                UserStat data = formatter.Deserialize(stream) as UserStat;
                stream.Close();
                return data;
            } catch (Exception e)
            {
                stream.Close();
                DeletePlayerData();
                return null;
            }
            
        } 
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveAnimal(AnimalStat data, string fileName = "animal.dat")
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/{fileName}";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Animal LoadAnimal(string fileName)
    {
        string path = Application.persistentDataPath + $"/{fileName}";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                Animal data = formatter.Deserialize(stream) as Animal;
                stream.Close();
                return data;
            }
            catch (Exception e)
            {
                stream.Close();
                return null;
            }

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void DeletePlayerData()
    {
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
