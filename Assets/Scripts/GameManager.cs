using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

	GameObject[] SwappableAssets;

    public Tilemap astralTiles;
    public Tilemap corporealTiles;

    public List<string> ghostsEncountered;

    public bool astral = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		SwappableAssets = GameObject.FindGameObjectsWithTag ("Swappable");
        SoundMaster.instance.MortalPlaneMode();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwitchPlanes () {
		astral = !astral;
		if (astral) {
			SoundMaster.instance.AstralPlaneMode ();
		} else {
			SoundMaster.instance.MortalPlaneMode ();
		}
		StopAllCoroutines ();
		;
		StartCoroutine (SwapTiles ());
		foreach (GameObject swappableAsset in SwappableAssets) {
			if (swappableAsset.layer == 9) {
				print (swappableAsset.GetComponent<SpriteRenderer> ().color.ToString ());
				//swappableAsset.GetComponent<Ghost>().GradualSetColor(swappableAsset.GetComponent<SpriteRenderer> ().color.a == 0 ?
				//Color.white : new Color (0, 0, 0, 0));
				swappableAsset.GetComponent<SpriteRenderer> ().color = 
					swappableAsset.GetComponent<SpriteRenderer> ().color.a == 0 ? Color.white : new Color (0, 0, 0, 0);
			} else if (swappableAsset.layer == 8) {
				//Swap player animation
			} else {
				print (swappableAsset.name);
				swappableAsset.GetComponent<SpriteChanger> ().SwitchSprite ();
			}
		}
	}

    IEnumerator SwapTiles()
    {
        Color newCorporealColour;
        Color newAstralColour;
        float corporealAlpha = corporealTiles.color.a;
        float astralAlpha = astralTiles.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1)
        {
            if (astral)
            {
                newCorporealColour = new Color(1, 1, 1, Mathf.Lerp(corporealAlpha, 0, t));
                newAstralColour = new Color(1, 1, 1, Mathf.Lerp(astralAlpha, 1, t));
            }
            else
            {
                newCorporealColour = new Color(1, 1, 1, Mathf.Lerp(corporealAlpha, 1, t));
                newAstralColour = new Color(1, 1, 1, Mathf.Lerp(astralAlpha, 0, t));
            }
            corporealTiles.color = newCorporealColour;
            astralTiles.color = newAstralColour;
            yield return null;
        }
    }
}
