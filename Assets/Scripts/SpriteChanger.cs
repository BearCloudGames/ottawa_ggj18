using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

//	public Sprite OtherSprite;
//	Sprite CurrentSprite;
	SpriteRenderer CurrentSpriteRenderer;
	SpriteRenderer OtherSpriteRenderer;

	// Use this for initialization
	void Start () {
		CurrentSpriteRenderer = this.gameObject.transform.GetChild (0).transform.GetComponent<SpriteRenderer> ();
		OtherSpriteRenderer = this.gameObject.transform.GetChild (1).transform.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwitchSprite() {
		SpriteRenderer tempSprite = OtherSpriteRenderer;

		StartCoroutine (FadeBetween (CurrentSpriteRenderer, CurrentSpriteRenderer.color.a, 0.0f, 3.0f));
		StartCoroutine (FadeBetween (OtherSpriteRenderer, OtherSpriteRenderer.color.a, 1.0f, 3.0f));

		OtherSpriteRenderer = CurrentSpriteRenderer;
		CurrentSpriteRenderer = tempSprite;
	}

	private IEnumerator FadeBetween(SpriteRenderer spriteRenderer, float currentAlpha, float alphaFinish, float time)
	{
		float elapsedTime = 0;

		while (elapsedTime < time)
		{
			currentAlpha = Mathf.Lerp(currentAlpha, alphaFinish, (elapsedTime / time));
			spriteRenderer.color = new Color (CurrentSpriteRenderer.color.r, CurrentSpriteRenderer.color.g, CurrentSpriteRenderer.color.b, currentAlpha);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
