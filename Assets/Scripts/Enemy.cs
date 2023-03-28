using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100;
    public bool alive = true;
    int currentHP;
    public Animator animator;
    public AIPath aiPath;
    public SpriteRenderer sprite, player;
    public const string Background = "Background";
    public Transform playerTarget;
    
    void Start()
    {
        currentHP = maxHP;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(transform.position.y > playerTarget.transform.position.y)
        {
            sprite.sortingOrder = player.sortingOrder - 1;
        }                                                                                                                                                            
        else if(transform.position.y < playerTarget.transform.position.y)
        {
            sprite.sortingOrder = player.sortingOrder +1;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        animator.SetTrigger("Hurt");

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
        alive = false;
        sprite.sortingLayerName = Background;
        sprite.sortingOrder = sprite.sortingOrder - 1;
        this.enabled = false;
    }
}
