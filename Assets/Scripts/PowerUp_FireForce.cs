using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/FireForce")]
public class PowerUp_FireForce : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.fireForce += amount;
    }
}
