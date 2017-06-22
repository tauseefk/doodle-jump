using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private GameObject _player;


	void Start () {
		_player = GameObject.FindGameObjectWithTag (Tags.PLAYER);
	}
	// Update is called once per frame
	void Update () {
		if (_player.transform.position.y - transform.position.y > Config.PLATORM_DESTROY_DISTANCE) {
			Destroy (this.gameObject);
		}
	}
}
