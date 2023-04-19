using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform playerTarget;

    void Start()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 direction = ((Vector2)playerTarget.position-(Vector2)transform.position).normalized;        
        transform.right = direction;
    }
}
