using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TMP_Text text_money;
    public TMP_Text text_animal;

    private void Update()
    {
        text_money.text = GameDirector.Instance.stat.money.ToString();
        text_animal.text = AnimalManager.Instance.activeAnimals.Count.ToString();
    }
}
