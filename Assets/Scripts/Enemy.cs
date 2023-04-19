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
    public float fireForce = 1f;
    [SerializeField]
    public float mobHeight = 0.6f; //transfer this to EnemyData after
    [SerializeField]
    public string mobType; //transfer this to EnemyData after
    [SerializeField]
    public float minDistance = 1f;
    [SerializeField]
    public float maxDistance = 5f;
    [SerializeField]
    private EnemyData data;
    private bool attackOnGCD = false;
    private bool fireOnGCD = false;
    public float gcd = 1f;
    public float fireGCD = 5f;
    public bool alive = true;
    int currentHP;
    public HP hpBar;
    Rigidbody2D rb;
    public GameObject ui, firePrefab;
    public Transform playerTarget, uiLocation, aim;
    public Animator animator;
    public AIPath aiPath;
    public SpriteRenderer enemySprite, targetSprite;
    public const string Background = "Background";
    public LayerMask playerLayer;
    public float distance;

    
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
        distance = Vector3.Distance (playerTarget.transform.position, transform.position);
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

        if(mobType == "ranged" && distance >= minDistance && distance <= maxDistance)
        {
            if(!alive)
                return;
            if(fireOnGCD)
                return;
            RangedAttack();
        }

    }

    private void SetEnemyValues()
    {
        currentHP = data.maxHP;
        damage = data.damage;
        speed = data.speed;
        hpBar.SetMaxHealth(data.maxHP);
        attackRange = data.attackRange;
        fireForce = data.fireForce;
        mobHeight = data.mobHeight;
        mobType = data.mobType;
        minDistance = data.minDistance;
        maxDistance = data.maxDistance;
        gcd = data.gcd;
        fireGCD = data.fireGCD;
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
        //Debug.Log("Enemy died!");
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
        MeleeAttack();
    }

    private void MeleeAttack()
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

    private void RangedAttack()
    {
        //animator.SetTrigger("Attack"); 
        //2) Detect enemies in range of attack Physics.OverlapSphereAll() if in 3D
        GameObject fire = Instantiate(firePrefab, aim.position, aim.rotation);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        rb.AddForce(aim.right * fireForce, ForceMode2D.Impulse);
        enemyProjectile projectile = fire.GetComponent<enemyProjectile>();
        projectile.DMG = damage;
        
        //Enter GCD
        fireOnGCD = true;
        StartCoroutine(DelayFire());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(gcd);
        attackOnGCD = false;
    }

    private IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(fireGCD);
        fireOnGCD = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
