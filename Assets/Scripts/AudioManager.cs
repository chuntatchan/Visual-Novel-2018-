using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;
	public AudioSource soundSource;
	public AudioClip[] audioClips;
	public int startingClip;

	// Use this for initialization
	void Start () {
		soundSource.clip = audioClips[startingClip];
		soundSource.Play ();

	}

	void Awake () {
		if (instance == null) {
			//if not, set it to this.
			instance = this;
		}
		//If instance already exists:
		else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
