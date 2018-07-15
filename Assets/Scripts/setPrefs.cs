using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setPrefs : MonoBehaviour {

    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject highlighter;
    [SerializeField]
    private GameObject[] pronounSelectionHighlighter;
    [SerializeField]
    private Text _inputField;


    private void Start()
    {
        continueButton.SetActive(false);
        highlighter.SetActive(false);
    }

    public void PronounSelect(string selection)
    {
        PlayerPrefs.SetString("pronoun", selection);
        highlighter.SetActive(true);
    }

    public void PronounHighlight(int i)
    {
        highlighter.transform.position = pronounSelectionHighlighter[i].transform.position;
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", _inputField.text);
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("pronoun") != null && _inputField.text != "")
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

}
