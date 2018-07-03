﻿using System.Collections;
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

    private bool canGetNextLine;

	// Use this for initialization
	void Start () {
        canGetNextLine = true;
        storyStrings = storyLines.StoryLinesArray [0];
		isPaused = false;
		linesCounter = 0;
		tbox.text = storyStrings._storySentence [linesCounter].storySentenceText;
		currentDecision = startingDecision;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			if (Input.GetButtonDown ("nextLine")) {
				linesCounter++;
				if (storyStrings._storySentence.Length == linesCounter) {
					activatePrompt ();
				} else {
					if (canGetNextLine) {
						tbox.text = storyStrings._storySentence [linesCounter].storySentenceText;
						if (storyStrings._storySentence [linesCounter]._audioClipStart != null) {
							AudioSource.PlayClipAtPoint (storyStrings._storySentence [linesCounter]._audioClipStart, gameObject.transform.position);
						}
					}
				}
			}
		}
	}

	private void activatePrompt() {
        canGetNextLine = false;
        tbox.text = currentDecision.promptString();
		for (int i = 0; i < currentDecision.numberOfDecisions(); i++) {
			Decisions [i].gameObject.SetActive (true);
			//Set buttonText
			Text optionTBox = Decisions[i].GetComponentInChildren<Text>();
			optionTBox.text = currentDecision.nextDecisionOptionString (i);
		}
	}

	private void deactivateDecisions() {
		canGetNextLine = true;
		for (int i = 0; i < Decisions.Length; i++) {
			Decisions [i].gameObject.SetActive (false);
		}
	}

    public void buttonClicked(int i)
    {
        deactivateDecisions();
        linesCounter = 0;
        currentDecision = currentDecision.nextDecisionAdvance(i);
        storyStrings = storyLines.StoryLinesArray[currentDecision.getNextStoryStrings()];
        tbox.text = storyStrings._storySentence[linesCounter].storySentenceText;
    }
}
