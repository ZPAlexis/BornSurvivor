using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealFull")]
public class PowerUp_HealFull : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.currentHealth = target.GetComponent<PlayerController>().data.maxHealth;
    }
}
