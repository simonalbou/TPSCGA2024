using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerTrackType
{
    Enter,
    Exit
}

public class EnemyPlayerDetector : MonoBehaviour
{
    
    public Animator enemyAI;
    public TriggerTrackType trackType;

    void OnTriggerEnter(Collider coll)
    {
        if (trackType == TriggerTrackType.Enter)
            enemyAI.SetBool("SeesPlayer", true);
    }

    void OnTriggerExit(Collider coll)
    {
        if (trackType == TriggerTrackType.Exit)
            enemyAI.SetBool("SeesPlayer", false);
    }
}
