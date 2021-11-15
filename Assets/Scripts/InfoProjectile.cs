using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoProjectile : Projectile
{

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
        if (collision.CompareTag("Animal"))
        {
            Animal animal = collision.GetComponent<Animal>();
            ModalWindowPanel.Instance.ShowAnimalInfo(collision.GetComponent<Animal>(), "Okay");
            Destroy(gameObject);
        }

    }
}
