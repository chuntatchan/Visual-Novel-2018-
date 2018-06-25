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

	[SerializeField]
	private GameObject[] Decisions;
	[SerializeField]
	private Decision startingDecision;
	[SerializeField]
	private Decision currentDecision;
	private StoryStrings storyStrings;

	// Use this for initialization
	void Start () {
		storyStrings = storyLines.StoryLinesArray [0];
		isPaused = false;
		linesCounter = 0;
		tbox.text = storyStrings.StoryText [linesCounter];
		currentDecision = startingDecision;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			if (Input.GetButtonDown ("nextLine")) {
				linesCounter++;
				if (storyStrings.StoryText.Length == linesCounter) {
					activatePrompt ();
				} else {
					tbox.text = storyStrings.StoryText [linesCounter];
				}
			}
		}
	}

	private void activatePrompt() {
		tbox.text = currentDecision.promptString();
		for (int i = 0; i < currentDecision.numberOfDecisions(); i++) {
			Decisions [i].gameObject.SetActive (true);
			//Set buttonText
			Text optionTBox = Decisions[i].GetComponentInChildren<Text>();
			optionTBox.text = currentDecision.nextDecisionOptionString (i);
		}
	}

	private void deactivateDecisions() {
		for (int i = 0; i < Decisions.Length; i++) {
			Decisions [i].gameObject.SetActive (false);
		}
	}
}
