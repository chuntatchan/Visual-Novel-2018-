using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setPrefs : MonoBehaviour {

    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject highlighter;
    [SerializeField]
    private GameObject[] pronounSelectionHighlighter;
    [SerializeField]
    private Text _inputField;
	[SerializeField]
	private Image fadeOutScreen;
	[SerializeField]
	private float fadeOutDelta;


    private void Start()
    {
		StartCoroutine (fadeIn());
        continueButton.SetActive(false);
        highlighter.SetActive(false);
    }

	IEnumerator fadeOut(string nextScene) {
		fadeOutScreen.gameObject.SetActive (true);
		float alpha = fadeOutScreen.color.a;
		while (alpha < 0) {
			alpha += fadeOutDelta;
			fadeOutScreen.color = new Color (fadeOutScreen.color.r, fadeOutScreen.color.g, fadeOutScreen.color.b, alpha);
			yield return new WaitForEndOfFrame ();
		}
		SceneManager.LoadScene (nextScene);
	}

	IEnumerator fadeIn() {
		fadeOutScreen.color = new Color (fadeOutScreen.color.r, fadeOutScreen.color.g, fadeOutScreen.color.b, 1f);
		fadeOutScreen.gameObject.SetActive (true);
		float alpha = fadeOutScreen.color.a;
		while (alpha > 0) {
			alpha -= fadeOutDelta;
			fadeOutScreen.color = new Color (fadeOutScreen.color.r, fadeOutScreen.color.g, fadeOutScreen.color.b, alpha);
			yield return new WaitForEndOfFrame ();
		}
		fadeOutScreen.gameObject.SetActive (false);
	}

    public void PronounSelect(string selection)
    {
        PlayerPrefs.SetString("pronoun", selection);
        highlighter.SetActive(true);
    }

    public void PronounHighlight(int i)
    {
        highlighter.transform.position = pronounSelectionHighlighter[i].transform.position;
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", _inputField.text);
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("pronoun") != "" && _inputField.text != "")
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

	public void LoadScene(string nextScene) {
		StartCoroutine (fadeOut(nextScene));
	}

}
