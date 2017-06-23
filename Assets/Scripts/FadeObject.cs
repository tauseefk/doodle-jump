using UnityEngine;
using System.Collections;

public class FadeObject : MonoBehaviour {

	private Renderer _renderer;
	private Color _color;

	void Awake () {
		_renderer = GetComponent<Renderer>();
		_color = _renderer.material.color;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut () {
		while(_renderer.material.color.a >= 0) {
			_color.a -= 0.02f;
			_renderer.material.color = _color;
			yield return new WaitForSeconds(.01f);
		}

		if(_renderer.material.color.a <= 0) {
			Destroy(gameObject);
		}
	}
}
