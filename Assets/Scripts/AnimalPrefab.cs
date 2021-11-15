using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New animal", menuName = "SO/Animal", order = 1)]
public class AnimalPrefab : ScriptableObject
{
    public int AnimalID;
    public string AnimalName;
    public Sprite AnimalPicture;
    public string Description;
    public GameObject AnimalObject;
}
