using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerWithAnimations : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // update speed (stick vertical axis)
        // update turn (stick horizontal axis)
        animator.SetFloat("Speed", Input.GetAxis("Vertical") * 2f);
        animator.SetFloat("Turn", Input.GetAxis("Horizontal") * 2f);

        // update crouch (crouch key)
        animator.SetBool("IsCrouching", Input.GetKey(KeyCode.JoystickButton4));

        // update jump (might need further logic)
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) // sauter avec la touche A (manette)
        {
            animator.SetBool("IsGrounded", false);
            animator.SetTrigger("Jump");
        }
        
        // update grounded (collision detection)
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) // fake atterrissage en appuyant sur X (manette)
            animator.SetBool("IsGrounded", true);
    }

    public void PlayFootstepSound(string helloWorldMsg)
    {
        //Debug.Log(helloWorldMsg);
    }
}
