using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : BaseEnemyBehaviour
{
    public float distanceThreshold;

    int indexOfCurrentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        indexOfCurrentTarget = 0;
        SetDestination(referenceHolder.patrolWaypoints[indexOfCurrentTarget].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((distanceThreshold > agent.remainingDistance) && (Time.time > recalculationTimestamp + recalculationDelay))
        {
            indexOfCurrentTarget++;
            if (indexOfCurrentTarget == referenceHolder.patrolWaypoints.Length)
                indexOfCurrentTarget = 0;
            SetDestination(referenceHolder.patrolWaypoints[indexOfCurrentTarget].position);
        }
    }
}
