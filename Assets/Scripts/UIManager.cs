using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject lifeText;
    public Image lifeBar;

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
        lifeBar.fillAmount = newLife / 100;
        if(lifeBar.fillAmount < 0.3)
        {
            StartCoroutine(HealthbarPulse());
        }
        else
        {
            StopAllCoroutines();
            lifeBar.color = Color.white;
        }
    }

    IEnumerator HealthbarPulse()
    {
        lifeBar.GetComponent<Image>().color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
        yield return new WaitForEndOfFrame();
    }
}
