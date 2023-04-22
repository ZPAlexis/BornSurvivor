using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/FireGCD")]
public class PowerUp_FireGCD : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.fireGCD *= (1 - (amount/100));
    }
}
