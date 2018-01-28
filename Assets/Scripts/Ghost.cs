using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public string GhostName;
    private TextReader _textReader;

	// Use this for initialization
	void Start () {
        _textReader = GetComponentInChildren<TextReader>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
        GameManager.instance.ghostsEncountered.Add(GhostName);
        // Turn on the dialogue box
        if (!_textReader.IsInitialized)
        {
            _textReader.Initialize();
        }
	}
}
