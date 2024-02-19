using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanCharacterController : MonoBehaviour
{
    public float speed = 7;
    public float rotateSpeedRatio = 1;
    public float camYawSpeed, camPitchSpeed;
    public bool invertYaw, invertPitch;
    public CharacterController ctrl;
    public Animator animator;
    public Transform graphicsTransform;
    public Transform camPivotYaw, camPivotPitch;
    public Transform camTransform;

    Vector3 lastKnownLookAtDirection;
    Quaternion idealLookAtDirection;

    void Update()
    {
        // convertir "move" en espace caméra
        Vector3 move = Input.GetAxis("Horizontal") * camTransform.right + Input.GetAxis("Vertical") * camTransform.forward;

        LookTowards(move);

        ctrl.Move(move * speed * Time.deltaTime);
        animator.SetFloat("Speed", move.magnitude);

        // camera rotation
        camPivotYaw.Rotate(Vector3.up * camYawSpeed * Time.deltaTime * Input.GetAxis("Horizontal_Cam") * (invertYaw ? -1 : 1));
        camPivotPitch.Rotate(Vector3.right * camPitchSpeed * Time.deltaTime * Input.GetAxis("Vertical_Cam") * (invertPitch ? -1 : 1));
    }

    void LookTowards(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            lastKnownLookAtDirection = move.normalized;
            idealLookAtDirection = Quaternion.LookRotation(lastKnownLookAtDirection);
        }
        
        graphicsTransform.rotation = Quaternion.Slerp(graphicsTransform.rotation, idealLookAtDirection, rotateSpeedRatio * Time.deltaTime);
    }
}