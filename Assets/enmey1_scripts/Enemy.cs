using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1_dead : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        //die animaiton
        Debug.Log("enemy died!!");

        animator.SetBool("isDead", true);
        //disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
