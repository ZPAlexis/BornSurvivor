using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Damage")]
public class PowerUp_Damage : PowerUp
{
    public override void Apply(GameObject target)
    {
        //target.GetComponent<PlayerData>().maxHealth += amount;
        target.GetComponent<PlayerController>().data.attackDMG += amount;
    }
}
