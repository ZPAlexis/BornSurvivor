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
    public PlayerData data;
    public LogicManager logic;
    public Rigidbody2D rb;
    public Animator animator, weaponAnimator;
    public PlayerInputActions controls;
    Vector2 movement, pointerInput;
    private WeaponParent weaponParent;
    private InputAction move, attack, fire, mousePosition, dash/*, look*/;
    public LevelSystem levelSystem;

    public Transform weaponPoint, firePoint, cameraTarget;
    public LayerMask enemyLayers;
    public HP hpBar;
    public UIXP uiXP;
    private float attackRange = 0.62f;

    public GameObject ui, firePrefab, levelUpScreen;
    private bool attackOnGCD;
    private bool fireOnGCD;
    public Vector3 weaponAdjust;

    private void Awake()
    {
        controls = new PlayerInputActions();
        this.levelSystem = new LevelSystem();
        levelSystem.OnLevelChange += LevelSystem_OnLevelChange;
        weaponParent = GetComponentInChildren<WeaponParent>();
        uiXP.SetLevelSystem(levelSystem);
    }

    private void OnEnable()
    {
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
        attack.Disable();
        mousePosition.Disable();
        fire.Disable();
        move.Disable();
    }

    void Start()
    {
        
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        data = new PlayerData(hpBar);
    }

    // Update is called once per frame
    void Update() //Input
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        pointerInput = GetPointerInput();
        cameraTarget.position = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());

        if(!data.alive)
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
        //rb.MovePosition(rb.position + movement * data.moveSpeed * Time.fixedDeltaTime);
        if(!data.alive)
            return;
        rb.velocity = new Vector2(movement.x * data.moveSpeed, movement.y * data.moveSpeed);

    }

    private Vector2 GetPointerInput()
    {
        Vector2 mousePos = mousePosition.ReadValue<Vector2>();
        //mousePos.z = 0; //Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if(!data.alive)
            return;
        //Brackeys tips
        if(attackOnGCD){
        Debug.Log("Attack on GCD");}
        //    return;
        else{
            
        
        //1) Play animation
        weaponAnimator.SetTrigger("Attack"); 
        
        //2) Detect enemies in range of attack Physics.OverlapSphereAll() if in 3D
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(weaponPoint.position + weaponAdjust, data.attackRange, enemyLayers);
        
        //3) Damage enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(PolygonCollider2D))
            {
            enemy.GetComponent<Enemy>().TakeDamage(data.attackDMG);
            //Debug.Log("We hit " + enemy.name + " for " + data.attackDMG + " damage.");
            }
        }

        //Enter GCD
        attackOnGCD = true;
        weaponParent.attackOnGCD = true;
        StartCoroutine(DelayAttack());
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if(!data.alive)
            return;
        if(fireOnGCD){
        Debug.Log("Fire on GCD");}
        else{
        
        GameObject fire = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * data.fireForce, ForceMode2D.Impulse);

        fireOnGCD = true;
        StartCoroutine(DelayFire());
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(data.atkGCD);
        attackOnGCD = false;
        weaponParent.ResetGCD();
    }

    private IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(data.fireGCD);
        fireOnGCD = false;
        //weaponParent.ResetGCD();
    }

    void OnDrawGizmosSelected()
    {
        if(weaponPoint == null)
            return;
        Gizmos.DrawWireSphere(weaponPoint.position + weaponAdjust, attackRange);
    }

    public void TakeDamage(int damage)
    {
        if(!data.alive)
            return;

        data.addHealth(-damage);
        hpBar.SetHealth(data.currentHealth);

        animator.SetTrigger("Hurt");
        ui.GetComponent<DamagePopupSpawner>().SpawnDamagePopup(damage);

        if(data.currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        // animator.SetBool("IsDead", true);
        Debug.Log("You died!");
        // GetComponent<Collider2D>().enabled = false;
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        data.alive = false;
        logic.gameOver();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Loot" && collider.gameObject.name == "XP" && data.alive)
        {
            levelSystem.AddExperience(5);
            //Debug.Log("Picked Up" + collider.gameObject.name);
            Destroy(collider.gameObject, 0);
        }
        else if(collider.gameObject.tag == "Loot" && collider.gameObject.name == "Health" && data.alive)
        {
            data.addHealth(10);
            Destroy(collider.gameObject, 0);
        }
        // else if(collider.gameObject.tag == "Loot" && collider.gameObject.name == "Coin" && alive)
        // {
        //     //adCoins();
        // }
    }
    private void LevelSystem_OnLevelChange(object sender, System.EventArgs e)
    {
        ChoosePowerup();
    }

    public void ChoosePowerup()
    {
        Time.timeScale = 0;
        levelUpScreen.GetComponent<PowerUpPool>().GenerateOptions();
        levelUpScreen.SetActive(true);
    }
    public void PowerupChosen(string name)
    {
        Time.timeScale = 1;
        levelUpScreen.SetActive(false);
        levelUpScreen.GetComponent<PowerUpPool>().RemoveOptions();
        levelUpScreen.GetComponent<PowerUpPool>().RemoveChoosenOption(name);
    }
}
