using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;
using UnityEngine.UI;

public class Animal : Entity, IDamageable, IKillable, IHealable, IFeedable
{
    public AnimalStat stat;
    public bool isFed = false;

    public Vector3 Target;
    public float speed = 200f;
    private float currSpeed;
    public float toNextWaypointDistance = 1.5f;
    public float value = 1f;
    public int scoreValue = 200;
    public float patrolRadius = 10f;

    public bool initialSpriteFacingLeft = true;
    private bool isVisible = false;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    // private Animator anim;

    private float timeOnPath = 0;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator anim;


    protected override SpriteRenderer GetSprite()
    {
        return GetComponent<SpriteRenderer>();
    }

    private void GetRandomTarget(float radius)
    {
        Target = new Vector3(UnityEngine.Random.Range(transform.position.x - radius, transform.position.x + radius),
            UnityEngine.Random.Range(transform.position.y - radius, transform.position.y + radius), 0);
        timeOnPath = 0f;
    }

    private void Start()
    {
        InitializeAnimal();
        sprite = GetSprite();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetRandomTarget(patrolRadius);
        InvokeRepeating("UpdatePath", 2f, 5f);
        currSpeed = speed;
    }

    private void UpdatePath()
    {
        if (timeOnPath >= 10f)
        {
            GetRandomTarget(patrolRadius);
            seeker.StartPath(rb.position, Target, OnPathComplete);
        }
        if (seeker.IsDone() || path.CompleteState == PathCompleteState.Partial)
        {
            seeker.StartPath(rb.position, Target, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void InitializeAnimal()
    {
        // stat = new AnimalStat();
    }

    public void ApplyStat(AnimalStat stat)
    {
        this.stat = stat;
    }

    private void Update()
    {
        if (!isActive) return;

    }

    private void FixedUpdate()
    {
        if (path == null) return;
        timeOnPath += Time.fixedDeltaTime;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            GetRandomTarget(patrolRadius);
            // DetectSurrounding();
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currSpeed * Time.deltaTime;

        rb.AddForce(force);
        anim.SetFloat("speed", rb.velocity.magnitude/2);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < toNextWaypointDistance) currentWaypoint++;
        float faceFactor = initialSpriteFacingLeft ? 1 : -1;
        if (force.x >= 0.1f)
        {
            GetComponent<SpriteRenderer>().flipX = initialSpriteFacingLeft ? true : false;
        }
        else if (force.x <= -0.1f)
        {
            // transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * faceFactor, transform.localScale.y, transform.localScale.z);
            GetComponent<SpriteRenderer>().flipX = initialSpriteFacingLeft ? false : true;
        }
    }

    public void CheckLifeTime()
    {
        if (DateTime.Compare(DateTime.Now, stat.lifeEnd) >= 0)
        {
            // isActive = false;
        }
    }

    public bool Feeding()
    {
        if (isActive && !isFed)
        {
            isFed = true;
            return true;
        }
        return false;
    }

    public bool GetKilled()
    {
        if (isActive)
        {
            isActive = false;
            anim.SetBool("isDead", true);
            
            return true;
        }
        Destroy(gameObject, 3);
        return false;
    }

    public bool Healing()
    {
        if (isActive) return true;
        return false;
    }

    public bool TakeDamage(float amount)
    {
        print("called");
        if (!isActive) return false;
        if (isInvincible) return false;
        print("take dmg");
        currentHP -= amount;
        TallyHP();
        ActivateInvincibility();
        if (currentHP <= 0) GetKilled();
        return true;
    }
}
