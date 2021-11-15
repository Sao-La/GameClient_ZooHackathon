using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : Singleton<AnimalManager>
{
    public List<Animal> activeAnimals = new List<Animal>();
    public AnimalPrefab[] animalList;
    private void Awake()
    {
        LoadAnimalFromPref();
        Animal[] animals = GameObject.FindObjectsOfType<Animal>();
        foreach (Animal animal in animals) activeAnimals.Add(animal);
    }

    private void LoadAnimalFromPref()
    {
        for (int i = 0; i < 99; ++i)
        {
            Animal animal = SaveSystem.LoadAnimal($"animal{i.ToString()}.dat");
            if (animal == null) break;
            AddNewAnimal(animal);
        }
    }

    private void SaveAnimalToPref()
    {
        int counter = 0;
        foreach (Animal animal in activeAnimals)
        {
            SaveSystem.SaveAnimal(animal.stat, $"animal{counter.ToString()}.dat");
            counter++;
        }
    }

    public AnimalPrefab GetAnimalById(int id)
    {
        foreach (AnimalPrefab ap in animalList)
        {
            if (ap.AnimalID == id) return ap;
        }
        return null;
    }

    public void AddNewAnimal(Animal animal)
    {
        activeAnimals.Add(animal);
    }

    private void Update()
    {
        activeAnimals.RemoveAll(animal => animal == null);
        foreach (Animal animal in activeAnimals)
        {
            // animal.CheckLifeTime();
        }
    }

    private void OnApplicationQuit()
    {
        SaveAnimalToPref();
    }

}
