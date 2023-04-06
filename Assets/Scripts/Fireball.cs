using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject firehitEffect;
    public LayerMask enemyLayers;
    public float hitRange = 1f;
    public int DMG = 10;
    public Vector3 adjust;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
        {
        GameObject effect = Instantiate(firehitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, hitRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(DMG);
            Debug.Log("We hit " + enemy.name + " for " + DMG + " damage.");
        }

        Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
