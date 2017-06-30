using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	private Text _text;


	void Awake() {
		_text = GetComponent<Text> ();
	}

	public void OnScoreUpdate (float score) {
		_text.text = Mathf.FloorToInt(score).ToString();
	}
}
