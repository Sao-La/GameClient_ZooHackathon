using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public int foodAmount = 0;
    public Weapon equippedWeapon;
    public int healthKitAmount = 0;

    public void UseFood()
    {
        if (foodAmount > 0)
        {

        }
    }

    public void UseHealthKit()
    {
        if (healthKitAmount > 0)
        {

        }
    }

    public void UseWeapon()
    {
        if (equippedWeapon != null)
        {

        }
    }

    public void SetWeapon(Weapon weapon = null)
    {
        equippedWeapon = weapon;
    }

    public void AddFood(int amount)
    {
        foodAmount += amount;
        if (foodAmount < 0) foodAmount = 0;
        else if (foodAmount > 99) foodAmount = 99;
    }

    public void AddHealthKit(int amount)
    {
        healthKitAmount += amount;
        if (healthKitAmount < 0) healthKitAmount = 0;
        else if (healthKitAmount > 99) healthKitAmount = 99;
    }
}
