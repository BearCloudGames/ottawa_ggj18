using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TextReader : MonoBehaviour {

	// Use this for initialization
	public TextAsset textFile;
	string[] dialogueLines;
	Text textBox;

	public float FadeTime = 2.0f;
	public float OnScreenTime = 2.0f;

	// Use this for initialization
	void Start () {
		textBox = GetComponent<Text> ();

		if (textFile != null) {
			dialogueLines = (textFile.text.Split ('\n'));
		}

		textBox.text = dialogueLines [0];

		InvokeRepeating("ReadNextLine", OnScreenTime + FadeTime, OnScreenTime + (FadeTime * 2));
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	void ReadNextLine() {
		textBox.CrossFadeAlpha (0f, FadeTime, false);
		Invoke("ChangeText", OnScreenTime);
	}

	void ChangeText() {
		foreach (string line in dialogueLines) {
			if (line == textBox.text) {
				int nextLine = System.Array.IndexOf (dialogueLines, line) + 1;
				if (nextLine == dialogueLines.Length) {
					print ("Finished script");
					CancelInvoke ();
					break;
				} else {
					textBox.text = dialogueLines [nextLine];
					break;
				}
			}
		}
		textBox.CrossFadeAlpha (1f, FadeTime, false);
	}
}
