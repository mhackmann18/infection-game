using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;

    public ProgressBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.BarValue = maxHealth;
        
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.BarValue = currentHealth;

        // Player loses if their health drops to 0
        if(currentHealth <= 0){
            Debug.Log("You Lose");
        }
    }
}
