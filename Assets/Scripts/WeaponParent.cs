using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 pointerPosition { get; set; }

    private void Update()
    {
        transform.right = (pointerPosition-(Vector2)transform.position).normalized;
    }
}
