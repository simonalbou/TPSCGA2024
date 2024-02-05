using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ViewpointSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera viewpointA, viewpointB;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            (viewpointA.Priority, viewpointB.Priority) = (viewpointB.Priority, viewpointA.Priority);
        }
    }
}
