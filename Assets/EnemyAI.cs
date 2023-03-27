using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

// Brackeys guide - https://youtu.be/jvtFUfJ6CP8?t=924

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rb;
    Seeker seeker;
    public Animator animator;
    public SpriteRenderer targetSprite, enemySprite;
    public Transform target, enemyGFX;
    Path path;
    public HP hpBar;
    public GameObject ui;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public bool alive = true;
    public int maxHP = 100;
    int currentHP;
    public float speed = 100f;
    public float slowdownDistance = 0.6f;
    public float nextWaypointDistance = 3f; // pickNextWaypointDist needs to be higher than slowdownDistance

    void Start()
    {
        currentHP = maxHP;
        hpBar.SetMaxHealth(maxHP);
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        targetSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(rb.velocity.x >=0.01f && force.x > 0f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f && force.x < 0f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(transform.position.y > target.transform.position.y && alive)
        {
            enemySprite.sortingOrder = targetSprite.sortingOrder - 1;
        }                                                                                                                                                            
        else if(transform.position.y < target.transform.position.y && alive)
        {
            enemySprite.sortingOrder = targetSprite.sortingOrder +1;
        }

    }

    public void TakeDamage(int damage)
    {
        if(!alive)
            return;
        currentHP -= damage;

        hpBar.SetHealth(currentHP);

        animator.SetTrigger("Hurt");

        ui.GetComponent<DamagePopupSpawner>().SpawnDamagePopup(damage);

        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        Debug.Log("Enemy died!");
        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        enemySprite.sortingOrder = targetSprite.sortingOrder - 1;
        alive = false;
        ui.SetActive(false);
        Destroy(gameObject, 10);
        //this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
