using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerUpCard : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text amount;
    public PowerUp powerupData;
    public GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        title.text = powerupData.title;
        description.text = powerupData.description;
        amount.text = powerupData.amount.ToString();
    }

    public void Confirm()
    {
        powerupData.Apply(player);
        player.GetComponent<PlayerController>().PowerupChosen();
    }
}
