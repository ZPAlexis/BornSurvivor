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
    public float attackRange = 0.6f;
    [SerializeField]
    public float mobHeight = 0.6f; //transfer this to EnemyData after
    [SerializeField]
    private EnemyData data;
    private bool attackOnGCD = false;
    public float gcd = 1f;
    public bool alive = true;
    int currentHP;
    public HP hpBar;
    Rigidbody2D rb;
    public GameObject ui;
    public Transform uiLocation;
    public Animator animator;
    public AIPath aiPath;
    public SpriteRenderer enemySprite, targetSprite;
    public const string Background = "Background";
    public Transform playerTarget;
    public LayerMask playerLayer;

    
    void Start()
    {
        SetEnemyValues();
        //enemySprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindWithTag("Player").transform;
        targetSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();   
        uiLocation = ui.GetComponent<RectTransform>();     
    }

    void Update()
    {
        Vector3 mobHeightVector = new Vector3(0.0f, 0.5f + mobHeight, 0.0f);
        uiLocation.transform.position = transform.position + mobHeightVector;
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
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
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        enemySprite.sortingOrder = enemySprite.sortingOrder - 1;
        enemySprite.sortingLayerName = Background;
        alive = false;
        ui.SetActive(false);
        Destroy(gameObject, 10);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(!alive)
            return;
        if(attackOnGCD)
            return;
        Attack();
    }

    private void Attack()
    {
        //animator.SetTrigger("Attack"); 
        //2) Detect enemies in range of attack Physics.OverlapSphereAll() if in 3D
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach(Collider2D player in hitPlayer)
        {
            if (player.GetType() == typeof(BoxCollider2D))
            {
            player.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }

        //Enter GCD
        attackOnGCD = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(gcd);
        attackOnGCD = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
