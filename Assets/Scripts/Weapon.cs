using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OwnerType
{
    PLAYER,
    HUNTER
}

public abstract class Weapon : MonoBehaviour
{
    public int attackPower;
    public bool isFriendly;
    public Transform holder;
    public OwnerType ownerType;

    public abstract void Attack();

}
