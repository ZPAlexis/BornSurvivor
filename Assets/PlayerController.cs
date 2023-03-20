using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerInputActions controls;
    Vector2 movement;
    private InputAction move;
    private InputAction fire;
    private InputAction dash;

    private void Awake()
    {
        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        //controls.Enable();
        move = controls.Player.Move;
        move.Enable();

        fire = controls.Player.Fire;
        fire.Enable();
        fire.performed += Attack;
    }

    private void OnDisable()
    {
        //controls.Disable();
        move.Disable();
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
    }

    // FixedUpdate works like Update but executed on a fixed timer, not stuck to the framerate like Update is. By default FixedUpdate is called 50 times per second
    void FixedUpdate() // Movement
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("We attacked");
    }

}
