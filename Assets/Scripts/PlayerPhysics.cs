using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : Entity, IDamageable, IKillable
{
    private Vector2 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
        sprite = PlayerController.Instance.graphics.GetComponent<SpriteRenderer>();
    }

    protected override SpriteRenderer GetSprite()
    {
        return PlayerController.Instance.graphics.GetComponent<SpriteRenderer>();
    }
    public bool TakeDamage(float amount)
    {
        if (isInvincible) return false;
        currentHP -= amount;
        if (currentHP <= 0) GetKilled();
        base.TallyHP();
        base.ActivateInvincibility();
        return true;
    }

    public bool GetKilled()
    {
        transform.position = spawnPoint;
        currentHP = maxHP;
        TallyHP();
        return true;
    }
}
