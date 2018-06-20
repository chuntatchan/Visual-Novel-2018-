using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNMain : MonoBehaviour {

	public StoryLines storyLines;
	public Text tbox;
	public GameObject[] Characters;

	private bool isPaused;
	[SerializeField]
	private int linesCounter;

	private bool[] isDecisionActive;
	private StoryStrings storyStrings;

	// Use this for initialization
	void Start () {
		storyStrings = storyLines.StoryLinesArray [0];
		isPaused = false;
		linesCounter = 0;
		tbox.text = storyStrings.StoryText [linesCounter];
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			if (Input.GetButtonDown ("nextLine")) {
				linesCounter++;
				tbox.text = storyStrings.StoryText [linesCounter];
			}
		}
	}
}
