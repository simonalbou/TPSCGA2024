using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehaviour : StateMachineBehaviour
{
    public string stateName;
    public float recalculationDelay = 3f;
    public float agentSpeed = 3f;
    public bool agentAutoBraking = false;

    [System.NonSerialized] public NavMeshAgent agent;
    [System.NonSerialized] public EnemyAIHandler referenceHolder;

    int indexOfCurrentTarget;
    [System.NonSerialized] public float recalculationTimestamp;

    public void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
        recalculationTimestamp = Time.time;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("entered state: "+stateName);

        agent = animator.GetComponent<NavMeshAgent>();
        referenceHolder = animator.GetComponent<EnemyAIHandler>();

        agent.autoBraking = agentAutoBraking;
        agent.speed = agentSpeed;
        indexOfCurrentTarget = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
