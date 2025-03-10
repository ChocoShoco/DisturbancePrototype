using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Animator controller;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Run();
        }
    }

    void Run()
    {
        controller.SetTrigger("Run");
    }
}
