using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public string GhostName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Turn on the dialogue box
		transform.GetChild (0).gameObject.SetActive (true);
        GameManager.instance.ghostsEncountered.Add(GhostName);
	}
}
