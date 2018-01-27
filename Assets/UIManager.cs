using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject lifeText;
    public GameObject lifeBar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void UpdateLife(float newLife)
    {
        lifeText.GetComponent<Text>().text = newLife.ToString("N0");
        lifeBar.transform.localScale = new Vector2(newLife / 100, lifeBar.transform.localScale.y);
    }
}
