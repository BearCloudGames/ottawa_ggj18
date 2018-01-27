using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

	GameObject[] SwappableAssets;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchPlanes () {
		foreach(GameObject swappableAsset in SwappableAssets) {
			swappableAsset.GetComponent<SpriteChanger> ().SwitchSprite ();
		}
	}
}
