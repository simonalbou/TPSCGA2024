using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackoffBehaviour : BaseEnemyBehaviour
{
    public float distanceThresholdToBase = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);        

        referenceHolder.onBackoff?.Invoke();

        agent.SetDestination(GameManager.instance.enemyBaseLocation.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance < distanceThresholdToBase && Time.time > recalculationDelay + recalculationTimestamp)
        {
            animator.SetTrigger("HasReachedBase");
        }
    }
}
