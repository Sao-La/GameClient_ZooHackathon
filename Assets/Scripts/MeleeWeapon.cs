using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private Animator _animator;
    private float _armLength = 1f;
    private float _attackDelay = 0.5f;
    private bool _canAttack = true;
    private Collider _collider;
    public Hunter owner;
    private float rotationZ = 0;
    private float newZ = 0;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        // _animator = GetComponent<Animator>();
        // InvokeRepeating("PointKnife", 2, 3);
    }

    public override void Attack()
    {
        if (!_canAttack) return;
        // _animator.SetTrigger("attack");
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        _collider.enabled = true;
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
        _collider.enabled = false;
    }
    private void PointKnife()
    {
        newZ = Mathf.Atan2(owner.direction.y, owner.direction.x) * Mathf.Rad2Deg + Random.Range(-20, 20);
        rotationZ = Mathf.Lerp(rotationZ, newZ, 0.1f);
    }


    private void Update()
    {
        Vector2 playerToWeaponDir = owner.direction;
        transform.position = (Vector2)holder.position
            + (_armLength * playerToWeaponDir.normalized);

        rotationZ = Mathf.Atan2(owner.direction.y, owner.direction.x) * Mathf.Rad2Deg + Random.Range(-10, 10);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ownerType == OwnerType.HUNTER)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerPhysics>().TakeDamage(2);
            }
            else if (collision.CompareTag("Animal"))
            {
                collision.GetComponent<Animal>().TakeDamage(2);
            }
        }
        else if (ownerType == OwnerType.PLAYER)
        {
            if (collision.CompareTag("Hunter"))
            {
                collision.GetComponent<Hunter>().TakeDamage(2);
            }
            else if (collision.CompareTag("Animal"))
            {
                collision.GetComponent<Animal>().TakeDamage(2);
            }
        }
    }

}
