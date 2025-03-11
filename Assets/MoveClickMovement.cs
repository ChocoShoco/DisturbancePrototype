using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveClickMovement : MonoBehaviour
{
    public LayerMask can_click;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (myRay, out hitInfo, 200, can_click))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
