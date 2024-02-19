using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanCharacterController : MonoBehaviour
{
    public float speed = 7;
    public float rotateSpeedRatio = 1;
    public CharacterController ctrl;
    public Animator animator;
    public Transform self;

    Vector3 lastKnownLookAtDirection;
    Quaternion idealLookAtDirection;

    void Update()
    {
        Vector3 move = Vector3.zero;

        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");

        LookTowards(move);

        ctrl.Move(move * speed * Time.deltaTime);
        animator.SetFloat("Speed", move.magnitude);
    }

    void LookTowards(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            lastKnownLookAtDirection = move.normalized;
            idealLookAtDirection = Quaternion.LookRotation(lastKnownLookAtDirection);
        }
        
        self.rotation = Quaternion.Slerp(self.rotation, idealLookAtDirection, rotateSpeedRatio * Time.deltaTime);
    }
}