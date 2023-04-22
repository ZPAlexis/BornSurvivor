using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/FireballEnable")]
public class PowerUp_FireballEnable : PowerUp
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().data.fireEnabled = true;
    }
}
