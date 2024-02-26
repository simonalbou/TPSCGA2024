using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIHandler : MonoBehaviour
{
    public Transform[] patrolWaypoints; // for the patrol behaviour

    public Transform playerTransform; // for chase and fight behaviour
    public Transform baseLocation; // for the backoff behaviour

    public UnityEvent onPatrol, onChase, onBackoff;
}
