using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    float moveSpeed = 2.5f;
    float animationThreshold = 0.01f;

    Animator animator;


    [SyncVar] Vector3 serverPosition;
    Vector3 serverPositionSmoothVelocity;
    Vector3 movement;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isServer)
        {
            // server-specific checking like taking ongoing damage
        }

        if (hasAuthority == true)
        {
            AuthorityUpdate();
        }

        // Do generic updates for ALL clients / server -- like animating movements and such

        // Are we in the correct position?
        if (hasAuthority == false)
        {
            // Lerp towards the "correct" server position
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, serverPosition, ref serverPositionSmoothVelocity, 0.05f);
            movement = (newPosition - transform.position);
            transform.position = newPosition;
        }

        if(Mathf.Abs(movement.x) < animationThreshold && Mathf.Abs(movement.y) < animationThreshold)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
        }


        



    }

    void AuthorityUpdate()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
        transform.Translate(movement * Time.deltaTime * moveSpeed);

        CmdUpdatePosition(transform.position);
    }

    [Command]
    void CmdUpdatePosition(Vector3 newPosition)
    {
        serverPosition = newPosition;
    }
}
