using UnityEngine;
using System.Collections;

public class FadeObject : MonoBehaviour {

	[SerializeField]
	private int _delay;

	void Awake () {
		_delay = 1;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut () {
		yield return new WaitForSeconds (_delay);
		Destroy(gameObject);
	}
}
