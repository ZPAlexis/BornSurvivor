using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 pointerPosition { get; set; }
    //public Transform circleOrigin;
    //public float radius;
    public bool attackOnGCD { get; set; }

    public void ResetGCD()
    {
        attackOnGCD = false;
    }
    
    private void Update()
    {
        if(attackOnGCD)
            return;
        Vector2 direction = (pointerPosition-(Vector2)transform.position).normalized;        
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0)
        {
            scale.y = -1;
        }
        else if(direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder -1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder +1;
        }
    }
}
