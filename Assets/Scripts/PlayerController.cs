using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    float moveSpeed = 2.5f;


    [SyncVar] Vector3 serverPosition;
    Vector3 serverPositionSmoothVelocity;

    // Use this for initialization
    void Start () {
		
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
            transform.position = Vector3.SmoothDamp(transform.position, serverPosition, ref serverPositionSmoothVelocity, 0.25f);
        }


	}

    void AuthorityUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
        transform.Translate(movement * Time.deltaTime * moveSpeed);

        CmdUpdatePosition(transform.position);
    }

    [Command]
    void CmdUpdatePosition(Vector3 newPosition)
    {
        serverPosition = newPosition;
    }
}
