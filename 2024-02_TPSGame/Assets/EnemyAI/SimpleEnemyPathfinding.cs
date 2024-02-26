using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Patrol,
    Chase,
    Backoff,
    Rest
}

public class SimpleEnemyPathfinding : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform destination;
    [Header("Stats")]
    public float recalculationDelay;
    public float recalculationDistance; 

    float lastRecalculationTimestamp;
    AIState state;

    void Update()
    {
        //UpdateFSM();

        if (agent.enabled)
        {
            if (Time.time - lastRecalculationTimestamp > recalculationDelay)
                Recalculate();

            if (agent.remainingDistance < recalculationDistance)
                Recalculate();
        }
        

        if (Input.GetKeyDown(KeyCode.G))
            agent.enabled = !agent.enabled;
    }

    void UpdateFSM()
    {
        if (state == AIState.Patrol)
        {
            // comportement de base

            // conditions de transition vers X ou Y état
        }

        if (state == AIState.Chase)
        {

        }
    }

    void Recalculate()
    {
        agent.SetDestination(destination.position);
        lastRecalculationTimestamp = Time.deltaTime;
    }
}
