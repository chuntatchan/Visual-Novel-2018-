using System.Collections;
using UnityEngine;
using System.Collections;

public class StoryLines : MonoBehaviour {
	public StoryStrings[] StoryLinesArray;
}

[System.Serializable]
public class StoryStrings {
	public string[] StoryText;
}