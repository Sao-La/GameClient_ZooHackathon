using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float lastingTime = 60;
    public int damage = 2;

    private void Start()
    {
        Destroy(gameObject, lastingTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Animal"))
        {
            collision.GetComponent<Animal>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
