using System.Collections;
using System.Collections.Generic;
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
    public Rigidbody2D rb;
    public Animator animator, weaponAnimator;
    public PlayerInputActions controls;
    Vector2 movement, pointerInput;
    private WeaponParent weaponParent;
    private InputAction move, fire, mousePosition/*, dash , look*/;

    public Transform attackPoint;
    public float moveSpeed = 5f;
    public float gcd = 0.3f;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private bool attackOnGCD;
    public int attackDMG = 20;

    private void Awake()
    {
        controls = new PlayerInputActions();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    private void OnEnable()
    {
        //controls.Enable();
        move = controls.Player.Move;
        move.Enable();

        mousePosition = controls.Player.MousePosition;
        mousePosition.Enable();

        fire = controls.Player.Fire;
        fire.Enable();
        fire.performed += Attack;
    }

    private void OnDisable()
    {
        //controls.Disable();
        move.Disable();
        mousePosition.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update() //Input
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        movement = move.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        pointerInput = GetPointerInput();
        weaponParent.pointerPosition = pointerInput;

    }

    // FixedUpdate works like Update but executed on a fixed timer, not stuck to the framerate like Update is. By default FixedUpdate is called 50 times per second
    void FixedUpdate() // Movement
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = mousePosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        //Brackeys tips
        if(attackOnGCD)
            return;
        
        //1) Play animation
        weaponAnimator.SetTrigger("Attack"); 
        weaponParent.isAttacking = true;
        attackOnGCD = true;
        
        //2) Detect enemies in range of attack Physics.OverlapSphereAll() if in 3D
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Attack");
        
        //3) Damage enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDMG);
            Debug.Log("We hit " + enemy.name + " for " + attackDMG + " damage.");
        }

        //Enter GCD
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(gcd);
        attackOnGCD = false;
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
