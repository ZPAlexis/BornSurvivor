using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/AtkGCD")]
public class PowerUp_AtkGCD : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.atkGCD *= (1 - (amount/100));
    }
}
