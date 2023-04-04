using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupSpawner : MonoBehaviour
{
    public TMP_Text damagePopup;
    public void SpawnDamagePopup(int damage)
    {
        var popup = Instantiate(damagePopup, transform.position, Quaternion.identity, gameObject.transform);

        popup.text = damage.ToString();
    }
}
