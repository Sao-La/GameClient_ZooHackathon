using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public float lastingTime = 60;
    public int value = 5;

    private void Start()
    {
        Destroy(gameObject, lastingTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameDirector.Instance.AddMoney(value);
            Destroy(gameObject);
        }
    }
}
