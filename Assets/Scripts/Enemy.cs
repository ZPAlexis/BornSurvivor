using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int maxHP = 60;
    [SerializeField]
    public int damage = 10;
    [SerializeField]
    public float speed = 1;    
    [SerializeField]
    private EnemyData data;
    private bool attackOnGCD = false;
    public float gcd = 1f;
    public bool alive = true;
    int currentHP;
    public HP hpBar;
    Rigidbody2D rb;
    public GameObject ui;
    public Animator animator;
    public AIPath aiPath;
    public SpriteRenderer enemySprite, targetSprite;
    public const string Background = "Background";
    public Transform playerTarget;
    
    void Start()
    {
        SetEnemyValues();
        //enemySprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindWithTag("Player").transform;
        targetSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            enemySprite.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            enemySprite.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(transform.position.y > playerTarget.transform.position.y && alive)
        {
            enemySprite.sortingOrder = targetSprite.sortingOrder - 1;
        }                                                                                                                                                            
        else if(transform.position.y < playerTarget.transform.position.y && alive)
        {
            enemySprite.sortingOrder = targetSprite.sortingOrder +1;
        }
    }

    private void SetEnemyValues()
    {
        currentHP = data.maxHP;
        damage = data.damage;
        speed = data.speed;
        hpBar.SetMaxHealth(data.maxHP);
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
        GetComponent<LootPool>().InstantiateLoot(transform.position);
        aiPath.canMove = false;
        Debug.Log("Enemy died!");
        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        enemySprite.sortingOrder = enemySprite.sortingOrder - 1;
        enemySprite.sortingLayerName = Background;
        alive = false;
        ui.SetActive(false);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(attackOnGCD)
            return;
        else {
        if(collider.gameObject.tag == "Player" && alive)
        {
            //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            collider.GetComponent<PlayerController>().TakeDamage(damage);
        }
        attackOnGCD = true;
        StartCoroutine(DelayAttack());
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(gcd);
        attackOnGCD = false;
    }

    // private void OnTriggerExit2D(Collider2D collider)
    // {
    //     if(collider.gameObject.tag == "Player")
    //     {
    //     rb.constraints = RigidbodyConstraints2D.None;
    //     rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    //     }
    // }

}
