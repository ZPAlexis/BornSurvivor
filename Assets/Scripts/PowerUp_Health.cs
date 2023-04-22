using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/MaxHealth")]
public class PowerUp_Health : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.maxHealth += amount;
        target.GetComponent<PlayerController>().data.addHealth(amount);
    }
}
