using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;
    public Animator animator;
    public SpriteRenderer sprite;
    public const string Debris = "Debris";
    void Start()
    {
        currentHP = maxHP;
        sprite = GetComponent<SpriteRenderer>();
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
        sprite.sortingLayerName = Debris;
        sprite.sortingOrder = sprite.sortingOrder - 1;
        this.enabled = false;
    }
}
