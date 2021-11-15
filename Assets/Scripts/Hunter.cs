using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class Hunter : Entity, IKillable, IDamageable
{
    public Vector2 direction;
    public Vector3 Target;
    public float speed = 200f;
    public float toNextWaypointDistance = 1.5f;
    public bool initialSpriteFacingLeft = true;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    // private Animator anim;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator anim;
    private float currSpeed;
    public float patrolRadius = 10f;
    private float timeOnPath = 0;
    

    private void Start()
    {
        sprite = GetSprite();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetRandomTarget(patrolRadius);
        InvokeRepeating("UpdatePath", 2f, 5f);
        currSpeed = speed;
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

        // direction = ((Vector2)Target - rb.position).normalized;
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currSpeed * Time.deltaTime;

        rb.AddForce(force);
        anim.SetFloat("speed", rb.velocity.magnitude / 2);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < toNextWaypointDistance) currentWaypoint++;
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

    private void UpdatePath()
    {
        if (timeOnPath >= 30f)
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

    private void GetRandomTarget(float radius)
    {
        Target = AnimalManager.Instance.activeAnimals.OrderBy(animal => Vector2.Distance(transform.position, animal.transform.position)).First().transform.position;
        timeOnPath = 0f;
    }

    public bool GetKilled()
    {
        if (!isActive) return false;
        isActive = false;
        anim.SetBool("isDead", true);
        Destroy(gameObject);
        return true;
    }

    public bool TakeDamage(float amount)
    {
        if (!isActive) return false;
        if (isInvincible) return false;
        currentHP -= amount;
        if (currentHP <= 0) GetKilled();
        base.TallyHP();
        base.ActivateInvincibility();
        return true;
    }
}
