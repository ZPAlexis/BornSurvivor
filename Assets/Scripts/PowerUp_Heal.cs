using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Heal")]
public class PowerUp_Heal : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.addHealth(amount);
    }
}
