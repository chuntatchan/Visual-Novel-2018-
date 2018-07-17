using System.Collections;
using UnityEngine;
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
	[Range(0, 1f)]
	public float _audioClipVolume;
	public Sprite newBGImage;
	public string animationToStart;
    public string charaName;
    public bool useName;
    public int spawnCharacter = -1;
    public Sprite newCharaLook;
    public int newCharaLookInt = -1;
    public Color textBoxColor;
    public FontStyle _fontStyle;
}