using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    private Animator _animator;
    private float _armLength = 1f;
    private float _attackDelay = 0.2f;
    private bool _canAttack = true;
    [SerializeField] GameObject bullet;
    private Collider _collider;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        if (!_canAttack) return;
        Projectile projectile = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.ownerType = ownerType;
        projectile.transform.parent = null;
        // _animator.SetTrigger("attack");
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetTrigger("shoot");
        }
        CinemachineShake.Instance.ShakeCamera(2f, 0.2f);
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }

    private void Update()
    {
        Vector2 playerToWeaponDir = PlayerController.Instance.direction;
        transform.position = (Vector2)holder.position
            + (_armLength * playerToWeaponDir.normalized);

        float rotationZ = Mathf.Atan2(PlayerController.Instance.direction.y, PlayerController.Instance.direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        
    }
}
