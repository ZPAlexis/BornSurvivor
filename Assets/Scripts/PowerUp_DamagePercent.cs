using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Damage%")]
public class PowerUp_DamagePercent : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.attackDMG *= (1 + (amount/100));
    }
}