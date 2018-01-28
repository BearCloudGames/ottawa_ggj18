using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public string GhostName;
    private TextReader _textReader;
    private SpriteRenderer _spriteRenderer;
	public bool hasReadText;

	// Use this for initialization
	void Start () {
        _textReader = GetComponentInChildren<TextReader>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    private Coroutine current_GradualSetColor;
    public void GradualSetColor(Color c)
    {
        if (current_GradualSetColor != null)
        {
            StopCoroutine(current_GradualSetColor);
        }
        current_GradualSetColor = StartCoroutine(GradualSetColor_Coroutine(c));
    }

    public IEnumerator GradualSetColor_Coroutine(Color c)
    {
        while (_spriteRenderer.color != c)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, c, Time.deltaTime);
            yield return null;
        }
    }
}
