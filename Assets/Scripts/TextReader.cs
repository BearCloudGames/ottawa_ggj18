using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TextReader : MonoBehaviour {

	// Set which dialogue script this script reads from
	public string DialogueFileName;
	string[] dialogueLines;
	Text textBox;

	// Set the time for fadeing in/out each line and how long it stays on the screen for
	public float FadeTime = 2.0f;
	public float OnScreenTime = 2.0f;

	// Use this for initialization
	void Start () {

		textBox = GetComponent<Text> ();

        XMLReader reader = new XMLReader();
        dialogueLines = reader.ReadFile(Application.dataPath + "/Text/" + DialogueFileName).ToArray();

		// Set it to the first line
		textBox.text = dialogueLines [0];

		// Keep showing lines until we've gone through all of them
		InvokeRepeating("ReadNextLine", OnScreenTime + FadeTime, OnScreenTime + (FadeTime * 2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fade out and go to the next line
	void ReadNextLine() {
		textBox.CrossFadeAlpha (0f, FadeTime, false);
		Invoke("ChangeText", OnScreenTime);
	}

	void ChangeText() {
		foreach (string line in dialogueLines) {
			if (line == textBox.text) {
				int nextLine = System.Array.IndexOf (dialogueLines, line) + 1;
				// Stop this coroutine if we've reached the end of the script, otherwise continue
				if (nextLine == dialogueLines.Length) {
					CancelInvoke ();
					Invoke ("SwitchBack", 1.0f);
					return;
				} else {
					textBox.text = dialogueLines [nextLine];
					break;
				}
			}
		}

		textBox.CrossFadeAlpha (1f, FadeTime, false);
	}

	void SwitchBack() {
		GameManager.instance.SwitchPlanes ();
	}
}
