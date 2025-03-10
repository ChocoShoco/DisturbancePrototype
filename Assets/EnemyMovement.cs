using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private float timer;
    public float wanderTimer;
    public float wanderRadius;
    public NavMeshAgent agent;
    private GameObject player;
    public float run_trigger_distance;
    public Animator animator;

    public State state;

    public enum State
    {
        Idle,
        Wander,
        RunAway,
    }

    private void Start()
    {
        state = State.Wander;
    }

    public void Update()
    {
        player = GameObject.Find("Player");
        switch (state)
        {
            case State.Wander:
                Wander();
                if(agent.velocity == Vector3.zero)
                {
                    animator.SetTrigger("Idle");
                }
                else
                {
                    animator.SetTrigger("Walk");
                }
                break;
            case State.RunAway:
                RunAway();
                if(agent.velocity != Vector3.zero)
                {
                    animator.SetTrigger("Run");
                }
                break;
        }

        float distance_to_player = Vector3.Distance(transform.position, player.transform.position);

        if (distance_to_player < run_trigger_distance)
        {
            state = State.RunAway;
        }
        else
        {
            state = State.Wander;
        }
    }
    public static Vector3 RandomWanderTarget(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    void Wander()
    {
        agent.speed = 2.5f;
        agent.acceleration = 60f;
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomWanderTarget(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void RunAway()
    {
        agent.speed = 10f;
        agent.acceleration = 60f;
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = (transform.position + dirToPlayer);

        agent.SetDestination(newPos);
    }
}
