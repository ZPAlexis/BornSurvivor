using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public int maxHP;
    public float speed;
    public int damage;

    // add monster drops (drop data script? - check YT tutorial)
}