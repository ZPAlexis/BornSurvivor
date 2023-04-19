using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public GameObject hitEffect;
    public LayerMask playerLayer;
    public float hitRange = 1f;
    public int DMG = 10;
    public Vector3 adjust;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, hitRange, playerLayer);
        
        foreach(Collider2D player in hitEnemies)
        {
            player.GetComponent<PlayerController>().TakeDamage(DMG);
        }

        Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
