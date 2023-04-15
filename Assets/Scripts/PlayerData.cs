using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {
    public int maxHealth = 100;
    public int currentHealth;

    public bool alive;

    public HP hpBar;

    public PlayerData(HP hpBar){
        this.currentHealth = this.maxHealth;
        this.hpBar = hpBar;
        this.hpBar.SetMaxHealth(this.maxHealth);
        this.alive = true;
    }

    public void addHealth(int amount){
        if(!this.alive){
            return;
        }

        if(amount + this.currentHealth > this.maxHealth){
            this.currentHealth = this.maxHealth;
        }else{
            this.currentHealth += amount;
        }
        this.hpBar.SetHealth(this.currentHealth);
    }
        /* 
        Notes:
        - Player Data to contain:
            public int maxHealth = 100;
            public float atkGCD = 0.5f;
            public float fireGCD = 0.5f;
            public float fireForce = 1f;
            public int currentHealth;
            public float moveSpeed = 7f;
            public float attackRange = 0.5f;
            public int attackDMG = 20;
        - PlayerController should pull this data at the start for default player values
        - On level up - update character data or the controller? Call the function to get updated player data
        - On weapon pickup/change - same?
        - Should Lvl (xp req to each lvl up) data be under the Data file as well?

        Data to add:
        + pickup range
        + gems


        */
}
