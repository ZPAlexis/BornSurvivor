using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
*Videos source - Unity New Control Scheme
* - Brackeys - https://www.youtube.com/watch?v=Pzd8NhcRzVo
* - BMo - https://www.youtube.com/watch?v=HmXU4dZbaMw
* - SunnyValleyStudio - https://www.youtube.com/watch?v=DPqc7qYDtzM - mouseposition track
*/

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerInputActions controls;
    Vector2 movement, pointerInput;
    private InputAction move, fire, mousePosition/*, dash , look*/;
    private WeaponParent weaponParent;
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
        Debug.Log("We attacked");
    }


}
