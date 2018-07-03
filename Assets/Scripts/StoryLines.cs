using System.Collections;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryLines : MonoBehaviour {
	public StoryStrings[] StoryLinesArray;
}

[System.Serializable]
public class StoryStrings {
	public storySentence[] _storySentence;
}

[System.Serializable]
public class storySentence {
	public string storySentenceText;
	public AudioClip _audioClipStart;
	public Sprite newBGImage;
	public string animationToStart;
}