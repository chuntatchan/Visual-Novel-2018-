using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUI_Text : MonoBehaviour {

	[SerializeField]
	private float translateDelta;

	// Use this for initialization
	void Start () {
		translateDelta = gameObject.transform.GetChild (0).GetComponent<RectTransform> ().rect.height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
