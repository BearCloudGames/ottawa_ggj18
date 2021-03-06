﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class TextReader : MonoBehaviour {

	// Set which dialogue script this script reads from
	public string DialogueFileName;
	List<string> dialogueLines;
	Text textBox;
    public bool IsInitialized = false;

	// Set the time for fadeing in/out each line and how long it stays on the screen for
	public float FadeTime = 2.0f;
	public float OnScreenTime = 2.0f;

	// Use this for initialization
	public void Initialize () {
        IsInitialized = true;
		textBox = GetComponent<Text> ();

        //XMLReader reader = new XMLReader(Application.dataPath + "/Text/" + DialogueFileName);
		XMLReader reader = new XMLReader(Application.dataPath + "/" + DialogueFileName);
        dialogueLines = reader.GetLines();
        string dialogueHint = reader.GetHint();

        if (dialogueHint == null)
        {
            dialogueHint = "You Win!";
        }
        dialogueLines.Add(dialogueHint);

		// Set it to the first line
		textBox.text = dialogueLines [0];

		// Keep showing lines until we've gone through all of them
		InvokeRepeating("ReadNextLine", OnScreenTime + FadeTime, OnScreenTime + (FadeTime * 2));
	}
	
	// Fade out and go to the next line
	void ReadNextLine() {
		textBox.CrossFadeAlpha (0f, FadeTime, false);
		Invoke("ChangeText", OnScreenTime);
	}

	void ChangeText() {
		foreach (string line in dialogueLines) {
			if (line == textBox.text) {
				int nextLine = System.Array.IndexOf (dialogueLines.ToArray(), line) + 1;
				// Stop this coroutine if we've reached the end of the script, otherwise continue
				if (nextLine == dialogueLines.Count) {
					CancelInvoke ();
					GetComponentInParent<Ghost> ().hasReadText = true;
					if (GameManager.instance.ghostsEncountered.Count == 6) {
						SceneManager.LoadScene ("WinScreen");
					}
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
