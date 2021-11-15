using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDistribution : Singleton<RandomDistribution>
{
    [SerializeField] GameObject moneyBag;
    [SerializeField] GameObject trap;
    [SerializeField] GameObject hunter;

    private void Start()
    {
        InvokeRepeating("SpawnMoneyBag", 1, 2);
        InvokeRepeating("SpawnTrap", 1, 5);
        InvokeRepeating("SpawnHunter", 1, 15);
    }

    public void SpawnMoneyBag()
    {
        GameObject bag = Instantiate(moneyBag, transform.position, transform.rotation);
        bag.transform.position = GetRandomPosition();
    }

    public void SpawnTrap()
    {
        GameObject tr = Instantiate(trap, transform.position, transform.rotation);
        tr.transform.position = GetRandomPosition();
    }

    public void SpawnHunter()
    {
        GameObject enemy = Instantiate(hunter, transform.position, transform.rotation);
        enemy.transform.position = GetRandomPosition();
    }

    public Vector2 GetRandomPosition()
    {
        Vector2 pos = new Vector2(Random.Range(Global.WEST_LIMIT, Global.EAST_LIMIT), Random.Range(Global.SOUTH_LIMIT, Global.NORTH_LIMIT));
        while (Physics2D.OverlapCircle(pos, 2f, LayerMask.NameToLayer("Obstacle")))
        {
            pos = new Vector2(Random.Range(Global.WEST_LIMIT, Global.EAST_LIMIT), Random.Range(Global.SOUTH_LIMIT, Global.NORTH_LIMIT));
        }
        return pos;
    }

}
