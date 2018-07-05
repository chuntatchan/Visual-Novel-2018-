using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionSceneTo : MonoBehaviour {

	public void buttonClick(string nextScene) {
		SceneManager.LoadScene (nextScene);
	}
}
