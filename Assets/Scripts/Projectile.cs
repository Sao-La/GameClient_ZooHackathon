using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float range = 20f;
    public float damage = 2;
    public Action<Collider2D> OnCollider;
    public OwnerType ownerType;
    protected Vector2 startPos;
    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        Destroy(gameObject, 15);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, startPos) > range) Destroy(gameObject);
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ownerType == OwnerType.HUNTER)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerPhysics>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Animal"))
            {
                collision.GetComponent<Animal>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (ownerType == OwnerType.PLAYER)
        {
            if (collision.CompareTag("Hunter"))
            {
                collision.GetComponent<Hunter>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Animal"))
            {
                collision.GetComponent<Animal>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        OnCollider?.Invoke(collision);
    }
}
