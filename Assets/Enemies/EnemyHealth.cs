using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float current_health;
    public float max_health;
    public bool dead;
    public GameObject life_essence;
    //public HealthBar healthBar;
    //public GameObject healthBarGO;

    private void Start()
    {
        current_health = max_health;
        //healthBar.UpdateHealthBar(max_health, current_health);
        dead = false;
    }

    private void Update()
    {
        if (current_health >= max_health)
        {
            current_health = max_health;
        }

        if (current_health <= 0 && !dead)
        {
            current_health = 0;
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        current_health -= amount;
        //healthBar.UpdateHealthBar(max_health, current_health);
    }

    public void Die()
    {
        Instantiate(life_essence,this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

}
