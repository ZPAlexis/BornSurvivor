using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public int amount;
    public string title;
    public string description;
    public string amountText;
    public abstract void Apply(GameObject target);
}
