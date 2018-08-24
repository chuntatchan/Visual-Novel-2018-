using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VNMain : MonoBehaviour {

    [Header("Story")]
	public StoryLines storyLines;
	

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
    private float letterPause;

    [Header("UI")]
    [SerializeField]
    private Image mainTbox;
	public Image fadeOutScreen;
	[SerializeField]
	private float fadeOutDelta;
    public Text tbox;
    public GameObject[] Characters;

    [SerializeField]
    private GameObject nameTag;
    [SerializeField]
    private Text nameTagTbox;
    [SerializeField]
    private AudioSource audioSource;

	[SerializeField]
	private GameObject[] TextUI;
	private int textUICounter;


    private bool canGetNextLine;

    private string messageToDisplay;
    private string message;




	// Use this for initialization
	void Start () {
		if (fadeOutScreen != null) {
			fadeOutScreen.gameObject.SetActive (false);
		}
        canGetNextLine = true;
        storyStrings = storyLines.StoryLinesArray [0];
		isPaused = false;
		linesCounter = 0;
		textUICounter = 0;
		messageToDisplay = storyStrings._storySentence [linesCounter].storySentenceText;
        Text();
		currentDecision = startingDecision;
	}

	IEnumerator activateNextScene() {
		fadeOutScreen.gameObject.SetActive (true);
		float alpha = fadeOutScreen.color.a;
		while (alpha > 0) {
			alpha -= fadeOutDelta;
			fadeOutScreen.color = new Color (fadeOutScreen.color.r, fadeOutScreen.color.g, fadeOutScreen.color.b, alpha);
			yield return new WaitForEndOfFrame ();
		}

		//Scene Change
		if (currentDecision.GetNextScene () != null) {
			SceneManager.LoadScene (currentDecision.GetNextScene ());
		} else {
			Debug.Log ("Missing NextScene for currentDecision");
		}

		yield return new WaitForEndOfFrame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused && canGetNextLine) {
			if (Input.GetButtonDown ("nextLine")) {
                if (!checkTyperText())
                {
                    StopAllCoroutines();
                    tbox.text = messageToDisplay;
                }
                else
                {

                    linesCounter++;
					if (storyStrings._storySentence.Length == linesCounter) {
						if (currentDecision.isContinueToNextScene ()) {
							StartCoroutine (activateNextScene());
						} else {
							activatePrompt ();
						}
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

	public void SetCanGetNextLine(bool state) {
		canGetNextLine = state;
	}

	public void hideMainTbox() {
		mainTbox.gameObject.SetActive (false);
		nameTag.gameObject.SetActive (false);
		nameTagTbox.gameObject.SetActive (false);
	}

	public void showMainTbox() {
		mainTbox.gameObject.SetActive (true);
		nameTag.gameObject.SetActive (true);
		nameTagTbox.gameObject.SetActive (true);
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
		if (currentStorySentence.animationToPreform !=  VNAnimation.none)
		{
			if (currentStorySentence.animationToPreform == VNAnimation.appear)
			Characters[currentStorySentence.charaAnimation].SetActive(true);
		}
		if (currentStorySentence.newCharaLookInt != -1)
		{
			Characters[currentStorySentence.newCharaLookInt].GetComponent<Image>().sprite = currentStorySentence.newCharaLook;
		}
	}

	private void activatePrompt() {
		if (currentDecision.GetIsActivateTextUI ()) {
			canGetNextLine = false;
			print ("activating TextUI");
			TextUI[textUICounter].SetActive (true);
			textUICounter++;
		} else {
			if (!currentDecision.isChoice ()) {
				print ("forced into merge");
				canGetNextLine = false;
				buttonClicked (0);
			} else {
				print("Not Merged");
				canGetNextLine = false;
				tbox.text = currentDecision.promptString ();
				for (int i = 0; i < currentDecision.numberOfDecisions (); i++) {
					Decisions [i].gameObject.SetActive (true);
					//Set buttonText
					Text optionTBox = Decisions [i].GetComponentInChildren<Text> ();
					optionTBox.text = currentDecision.nextDecisionOptionString (i);
				}
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
		canGetNextLine = true;
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
        // Change message to replace pronouns and playerName
        message = messageToDisplay;
		message = ReplaceWords (message, "<pname>", PlayerPrefs.GetString("name"));
        tbox.text = "";
        StartCoroutine(TypeText());
    }

    private string ReplaceWords(string message, string key, string newWord)
    {
        string newMessage = message;
        while (newMessage.Contains(key))
        {
            newMessage.Replace(key, newWord);
        }
        return newMessage;
    }

}
