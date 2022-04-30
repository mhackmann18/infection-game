using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public EnemyHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.BarValue = currentHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.BarValue = currentHealth;

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy has died!");
        //Die Animation
        Destroy(gameObject, 0.75f);
        //Disable
        
    }
}
