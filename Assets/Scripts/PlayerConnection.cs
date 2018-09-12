using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour {

    public GameObject CharacterPrefab;
    GameObject myCharacter;

	// Use this for initialization
	void Start () {

        if (isServer == true)
        {
            SpawnCharacter();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnCharacter()
    {
        if (isServer == false)
        {
            return;
        }

        myCharacter = Instantiate(CharacterPrefab);
        NetworkServer.SpawnWithClientAuthority(myCharacter, connectionToClient);
    }
}
