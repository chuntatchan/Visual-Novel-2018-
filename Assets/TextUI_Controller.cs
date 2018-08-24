using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI_Controller : MonoBehaviour {

	[SerializeField]
	private VNMain mainController;
	[SerializeField]
	private GameObject textContainer;
	[SerializeField]
	private TextUI_Text[] texts;

	private int framesToComplete = 10;


	// Use this for initialization
	void OnEnable () {
		mainController.SetCanGetNextLine (false);
		mainController.hideMainTbox ();
		StartCoroutine (lookThroughTexts());
	}

	IEnumerator lookThroughTexts() {
		for (int i = 0; i < texts.Length - 1; i++) {
			for (int j = 0; j <= framesToComplete; j++) {
				textContainer.transform.Translate (0, texts [i].GetTranslateDelta () / framesToComplete, 0);
				yield return new WaitForEndOfFrame ();
			}
			yield return new WaitForSeconds (0.5f);
		}
		mainController.SetCanGetNextLine (true);
		mainController.showMainTbox ();
		mainController.buttonClicked (0);
		gameObject.SetActive (false);
		yield return null;
	}

}
