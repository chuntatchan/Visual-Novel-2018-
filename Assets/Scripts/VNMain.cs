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
    [SerializeField]
    private GameObject nameTag;
    [SerializeField]
    private Text nameTagTbox;
    [SerializeField]
    private AudioSource audioSource;



    private bool canGetNextLine;

    private string messageToDisplay;
    private string message;
    [SerializeField]
    private float letterPause;



	// Use this for initialization
	void Start () {
        canGetNextLine = true;
        storyStrings = storyLines.StoryLinesArray [0];
		isPaused = false;
		linesCounter = 0;
		messageToDisplay = storyStrings._storySentence [linesCounter].storySentenceText;
        Text();
		currentDecision = startingDecision;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			if (Input.GetButtonDown ("nextLine")) {
                if (!checkTyperText())
                {
                    StopAllCoroutines();
                    tbox.text = messageToDisplay;
                }
                else
                {

                    linesCounter++;
                    if (storyStrings._storySentence.Length == linesCounter)
                    {
                        activatePrompt();
                    }
                    else
                    {
                        if (canGetNextLine)
                        {
							startNextSentence ();
                        }
                    }
                }
			}
		}
	}

    private bool checkTyperText()
    {
        if (tbox.text == messageToDisplay)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	private void startNextSentence() {
		//Add Beginning of Sentence Conditionals here
		storySentence currentStorySentence = storyStrings._storySentence[linesCounter];
		messageToDisplay = currentStorySentence.storySentenceText;
		Text();
		audioSource.Stop();
		if (currentStorySentence._audioClipStart != null)
		{
			audioSource.clip = currentStorySentence._audioClipStart;
			audioSource.volume = currentStorySentence._audioClipVolume;
			audioSource.Play();
		}
		if (currentStorySentence.useName)
		{
			nameTagTbox.text = currentStorySentence.charaName;
			nameTag.SetActive(true);
		}
		else
		{
			nameTagTbox.text = "";
			nameTag.SetActive(false);
		}
		if (currentStorySentence.spawnCharacter != -1)
		{
			Characters[currentStorySentence.spawnCharacter].SetActive(true);
		}
		if (currentStorySentence.newCharaLookInt != -1)
		{
			Characters[currentStorySentence.newCharaLookInt].GetComponent<Image>().sprite = currentStorySentence.newCharaLook;
		}
	}

	private void activatePrompt() {
        if (currentDecision.numberOfDecisions() == 1)
        {
            if (!currentDecision.nextDecisionAdvance(0).isChoice())
            {
                buttonClicked(0);
            }
        }
        else
        {
            canGetNextLine = false;
            tbox.text = currentDecision.promptString();
            for (int i = 0; i < currentDecision.numberOfDecisions(); i++)
            {
                Decisions[i].gameObject.SetActive(true);
                //Set buttonText
                Text optionTBox = Decisions[i].GetComponentInChildren<Text>();
                optionTBox.text = currentDecision.nextDecisionOptionString(i);
            }
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
        storyStrings = storyLines.StoryLinesArray[currentDecision.getStoryStrings()];
		startNextSentence ();
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            tbox.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

    void Text()
    {
        message = messageToDisplay;
        tbox.text = "";
        StartCoroutine(TypeText());
    }

}
