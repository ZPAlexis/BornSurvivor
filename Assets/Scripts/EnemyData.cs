using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int maxHP;
    public float speed;
    public int damage;
    public float attackRange;
    public float mobHeight;
    public string mobType;
    public float fireForce;
    public float minDistance;
    public float maxDistance;
    public float gcd;
    public float fireGCD;

    // add monster drops (drop data script? - check YT tutorial)
}