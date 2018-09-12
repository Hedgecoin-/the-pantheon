using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    [SyncVar] public float TimeOfDay = 1200;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
        {
            return;
        }

        TimeOfDay += Time.deltaTime;
        if(TimeOfDay >= 2400)
        {
            TimeOfDay = 0;
        }

	}
}
