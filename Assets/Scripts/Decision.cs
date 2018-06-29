using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decision : MonoBehaviour {

	[SerializeField]
	private string optionString, prompt;
	[SerializeField]
	private int nextStoryStrings;
	[SerializeField]
	private Image nextImage;
	[SerializeField]
	private Decision[] nextDecisions;

	// Animation? Not sure how to set that up yet.

	public int numberOfDecisions() {
		return nextDecisions.Length;
	}

    public int getNextStoryStrings()
    {
        return nextStoryStrings;
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

}
