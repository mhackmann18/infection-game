using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

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
