using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMovement2 : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public TrashManager2 tm;

    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    public bool jump = false;
    public bool crouch = false;

    // Update is called once per frame
    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            tm.trashCount++;
        }
    }

}
