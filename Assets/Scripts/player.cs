using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {


	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Is_Walking", true);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			anim.SetBool ("Is_Walking", false);
		}



	}
}
