using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionSceneTo : MonoBehaviour {

	public void LoadScene(string nextScene) {
		SceneManager.LoadScene (nextScene);
	}
}
