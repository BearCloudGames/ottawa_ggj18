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
        float elapsedTime = 0;
        float time = 1;
        Color currentColor = _spriteRenderer.color;
        Color colorFinish = c;
        while (elapsedTime != time)
        {
            currentColor = Color.Lerp(currentColor, colorFinish, (elapsedTime / time));
            _spriteRenderer.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _spriteRenderer.color = c;
    }
}
