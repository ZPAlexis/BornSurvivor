using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

/*
*Videos source - Unity New Control Scheme
* - Brackeys - New input system - https://www.youtube.com/watch?v=Pzd8NhcRzVo
* - BMo - New input system - https://www.youtube.com/watch?v=HmXU4dZbaMw
* - SunnyValleyStudio - mouseposition track - https://www.youtube.com/watch?v=DPqc7qYDtzM
* - SunnyValleyStudio - combat attacks - https://www.youtube.com/watch?v=7vMHTUwtyNs
*/

public class PlayerController : MonoBehaviour
{
    public LogicManager logic;
    private LevelSystem levelSystem;
    public Rigidbody2D rb;
    public Animator animator, weaponAnimator;
    public PlayerInputActions controls;
    Vector2 movement, pointerInput;
    private WeaponParent weaponParent;
    private InputAction move, attack, fire, mousePosition, dash/*, look*/;

    public Transform weaponPoint, firePoint, cameraTarget;
    public LayerMask enemyLayers;
    public HP hpBar;
    public UIXP uiXP;

    public GameObject ui, firePrefab;
    private bool attackOnGCD;
    private bool fireOnGCD;

    public float atkGCD = 0.5f;
    public float fireGCD = 0.5f;
    public float fireForce = 1f;
    private bool alive;
    public int maxHealth = 100;
    public int currentHealth;
    public float moveSpeed = 7f;
    public float attackRange = 0.5f;
    public int attackDMG = 20;

    private void Awake()
    {
        controls = new PlayerInputActions();
        LevelSystem levelSystem = new LevelSystem();
        this.levelSystem = levelSystem;
        uiXP.SetLevelSystem(levelSystem);
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    private void OnEnable()
    {
        //controls.Enable();
        move = controls.Player.Move;
        move.Enable();

        mousePosition = controls.Player.MousePosition;
        mousePosition.Enable();

        attack = controls.Player.PrimaryAttack;
        attack.Enable();
        attack.performed += Attack;

        fire = controls.Player.SecondaryAttack;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        //controls.Disable();
        move.Disable();
        mousePosition.Disable();
        attack.Disable();
        fire.Disable();
    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        currentHealth = maxHealth;
        alive = true;
        hpBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update() //Input
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        pointerInput = GetPointerInput();
        cameraTarget.position = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());

        if(!alive)
            return;
        movement = move.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        weaponParent.pointerPosition = pointerInput;

    }

    // FixedUpdate works like Update but executed on a fixed timer, not stuck to the framerate like Update is. By default FixedUpdate is called 50 times per second
    void FixedUpdate() // Movement
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if(!alive)
            return;
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

    }

    private Vector2 GetPointerInput()
    {
        Vector2 mousePos = mousePosition.ReadValue<Vector2>();
        //mousePos.z = 0; //Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if(!alive)
            return;
        //Brackeys tips
        if(attackOnGCD){
        Debug.Log("Attack on GCD");}
        //    return;
        else{
            
        
        //1) Play animation
        weaponAnimator.SetTrigger("Attack"); 
        
        //2) Detect enemies in range of attack Physics.OverlapSphereAll() if in 3D
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(weaponPoint.position, attackRange, enemyLayers);
        
        //3) Damage enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDMG);
            Debug.Log("We hit " + enemy.name + " for " + attackDMG + " damage.");
        }

        //Enter GCD
        attackOnGCD = true;
        weaponParent.attackOnGCD = true;
        StartCoroutine(DelayAttack());
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if(!alive)
            return;
        if(fireOnGCD){
        Debug.Log("Fire on GCD");}
        else{
        
        GameObject fire = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);

        fireOnGCD = true;
        StartCoroutine(DelayFire());
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(atkGCD);
        attackOnGCD = false;
        weaponParent.ResetGCD();
    }

    private IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(fireGCD);
        fireOnGCD = false;
        //weaponParent.ResetGCD();
    }

    void OnDrawGizmosSelected()
    {
        if(weaponPoint == null)
            return;
        Gizmos.DrawWireSphere(weaponPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        if(!alive)
            return;
        currentHealth -= damage;

        hpBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");
        Debug.Log("Player took " + damage + " damage.");

        ui.GetComponent<DamagePopupSpawner>().SpawnDamagePopup(damage);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        if(!alive || currentHealth == maxHealth)
            return;
        currentHealth += heal;

        hpBar.SetHealth(currentHealth);

        //animator.SetTrigger("Hurt");
        Debug.Log("Player healed for " + heal + " damage.");

        //ui.GetComponent<DamagePopupSpawner>().SpawnDamagePopup(damage);
    }

    void Die()
    {
        // animator.SetBool("IsDead", true);
        Debug.Log("You died!");
        // GetComponent<Collider2D>().enabled = false;
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        alive = false;
        logic.gameOver();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Loot" && collider.gameObject.name == "XP" && alive)
        {
            levelSystem.AddExperience(1);
            Debug.Log("Picked Up" + collider.gameObject.name);
            Destroy(collider.gameObject, 0);
        }
        else if(collider.gameObject.tag == "Loot" && collider.gameObject.name == "Health" && alive)
        {
            Heal(10);
            Destroy(collider.gameObject, 0);
        }
        // else if(collider.gameObject.tag == "Loot" && collider.gameObject.name == "Coin" && alive)
        // {
        //     //adCoins();
        // }
    }

}
