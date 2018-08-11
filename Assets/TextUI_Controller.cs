using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI_Controller : MonoBehaviour {

	[SerializeField]
	private GameObject textContainer;
	[SerializeField]
	private TextUI_Text[] texts;


	// Use this for initialization
	void OnEnable () {
		StartCoroutine (lookThroughTexts());
	}

	IEnumerator lookThroughTexts() {
		for (int i = 0; i < texts.Length; i++) {
			for (int j = 0; j <= 30; j++) {
				textContainer.transform.Translate (0, texts [i].GetTranslateDelta () / 30, 0);
				yield return new WaitForEndOfFrame ();
			}
			yield return new WaitForSeconds (1f);
		}
		yield return null;
	}

}
