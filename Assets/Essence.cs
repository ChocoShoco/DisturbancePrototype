using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EvolutionManager evolutionManager = other.GetComponent<EvolutionManager>();
            evolutionManager.add_essence();
            Destroy(this.gameObject);
        }
    }
}
