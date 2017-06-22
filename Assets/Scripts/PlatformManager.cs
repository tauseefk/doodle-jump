using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

	public GameObject _platformPrefab;

	private float _currentHeight;
	private int _blocksInSection = 5;
	private float _blockOffset = 3f;
	private int flag = -1;

	private Transform _playerTransform;

	// Use this for initialization
	void Start () {
		_currentHeight = 0;
		_playerTransform = GameObject.FindGameObjectWithTag (Tags.PLAYER).transform;
		SpawnSection ();
	}

	// Update is called once per frame
	void Update () {
		if (_currentHeight - _playerTransform.position.y < Config.SECTION_SPAWN_DISTANCE) {
			SpawnSection ();
		}
	}

	void SpawnSection() {
		for (int i = 0; i < _blocksInSection; i++) {
			_currentHeight += _blockOffset;
			CreatePlatformBlock (_currentHeight);
		}
	}

	void CreatePlatformBlock(float height) {
		GameObject block = Instantiate (_platformPrefab) as GameObject;
		Vector3 pos = block.transform.position;
		pos.y = height;
		pos.x = flag * Random.Range (0, Config.PLATFORM_RANGE);
		flag *= -1;
		block.transform.position = pos;
		block.transform.parent = this.transform;

		if (Random.value < Config.ENEMY_SPAWN_PROBABILITY) {
//			block.transform.Find ("Enemy").gameObject.SetActive (true);
		}
	}
}
