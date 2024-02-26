using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : BaseEnemyBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);        

        referenceHolder.onChase?.Invoke();

        agent.SetDestination(GameManager.instance.playerTransform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > recalculationDelay + recalculationTimestamp)
            SetDestination(GameManager.instance.playerTransform.position);

        if (Input.GetButtonDown("Fire1"))
            animator.SetTrigger("Hurt");
    }
}
