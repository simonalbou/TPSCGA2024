using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanCharacterController : MonoBehaviour
{
    public float speed = 7;
    public float gravity = -9.81f;
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
        Vector3 camRight = camTransform.right;
        camRight.y = 0;
        camRight = camRight.normalized;
        Vector3 camForward = camTransform.forward;
        camForward.y = 0;
        camForward = camForward.normalized;
        Vector3 move = Input.GetAxis("Horizontal") * camRight + Input.GetAxis("Vertical") * camForward;

        // exploiter "move" pour les données graphiques (animation, orientation)
        LookTowards(move);
        animator.SetFloat("Speed", move.magnitude);

        // on ajoute la gravité au move
        move.y = gravity;
        ctrl.Move(move * speed * Time.deltaTime);

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