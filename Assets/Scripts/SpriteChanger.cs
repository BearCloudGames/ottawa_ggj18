using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

	public Sprite OtherSprite;
	Sprite CurrentSprite;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		CurrentSprite = GetComponent<SpriteRenderer> ().sprite;	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwitchSprite() {
		Sprite tempSprite = spriteRenderer.sprite;

		spriteRenderer.sprite = OtherSprite;

		CurrentSprite = spriteRenderer.sprite;
		OtherSprite = tempSprite;
	}
}
