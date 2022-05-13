using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GlobalVars.enemiesRemaining -= 1;
        Destroy(gameObject, 0.75f);

        // Player is taken to victory screen when all zombies are defeated
        if(GlobalVars.enemiesRemaining == 0){
            SceneManager.LoadScene("WinScreen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
