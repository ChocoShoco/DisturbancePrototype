using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit!");
            EnemyHealth enemy_health = other.gameObject.GetComponent<EnemyHealth>();
            if (enemy_health != null)
            {
                //Debug.Log("Enemy hit!");
                enemy_health.TakeDamage(50f);
            }
        }
    }
}
