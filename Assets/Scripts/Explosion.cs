using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	private ParticleSystem _particleSystemContainer;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha5)) {
			_particleSystemContainer.Play ();
		}
	}

	public void OnHit() {
		_particleSystemContainer = gameObject.GetComponent<ParticleSystem> ();
		_particleSystemContainer.Play ();
	}
}
