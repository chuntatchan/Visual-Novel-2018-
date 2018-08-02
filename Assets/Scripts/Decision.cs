using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decision : MonoBehaviour {

	[SerializeField]
	private string optionString, prompt;
	[SerializeField]
	private int storyStrings;
	[SerializeField]
	private Decision[] nextDecisions;
    [SerializeField]
    private bool _isChoice = true;
	[SerializeField]
	private bool _isContinueToNextScene = false;
	[SerializeField]
	private bool activateTextUI = false;
	[SerializeField]
	private string nextScene;
 

	// Animation? Not sure how to set that up yet.

	public int numberOfDecisions() {
		return nextDecisions.Length;
	}

    public int getStoryStrings()
    {
        return storyStrings;
    }

	public string promptString() {
		return prompt;
	}

	public string decisionString() {
		return optionString;
	}

	public string nextDecisionOptionString(int i) {
		return nextDecisions [i].optionString;
	}

    public Decision nextDecisionAdvance(int i)
    {
        return nextDecisions[i];
    }

    public bool isChoice()
    {
        return _isChoice;
    }

	public bool isContinueToNextScene() {
		return _isContinueToNextScene;
	}

	public bool GetIsActivateTextUI() {
		return activateTextUI;
	}

	public string GetNextScene() {
		return nextScene;
	}

}
