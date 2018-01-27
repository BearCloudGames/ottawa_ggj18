using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	GameObject[] EnvironmentAssets;

	// Use this for initialization
	void Start () {
		EnvironmentAssets = GameObject.FindGameObjectsWithTag ("Environment");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SwitchPlanes () {
		foreach(GameObject environmentAsset in EnvironmentAssets) {
			environmentAsset.GetComponent<SpriteChanger> ().SwitchSprite ();
		}
	}
}
