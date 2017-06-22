using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


	private const string INPUT_AXIS = "Horizontal";

	[SerializeField]
	[Tooltip("Horizontal movement speed of the player.")]
	private float _moveSpeed = 8.0f;

	[SerializeField]
	private float _gravity = -9.81f;

	[SerializeField]
	[Tooltip("Y speed when bouncing off a platform (m/s)")]
	private float _bounceSpeed = 15.0f;

	[SerializeField]
	private GameObject _rightBoundary;

	[SerializeField]
	private GameObject _leftBoundary;

	private Vector3 _velocity;

	private int _maxHeight = 0;
	private int _currentHeight = 0;

	// We'll fire an event every time the player has reached a new maximum height
	// Parameter is the new maximum
	[System.Serializable]
	public class MaxHeightEvent : UnityEvent<float> {}
	[SerializeField]
	private MaxHeightEvent _maxHeightEvent;

	[System.Serializable]
	public class GameOverEvent : UnityEvent {}
	[SerializeField]
	private GameOverEvent _gameOverEvent;

	// Update is called once per frame
	void Update () {
		float input = Input.GetAxis(INPUT_AXIS);
		_currentHeight = Mathf.FloorToInt (transform.position.y);

		Vector3 leftWallPos = _leftBoundary.gameObject.transform.position;
		_leftBoundary.gameObject.transform.position = new Vector3 (leftWallPos.x, _currentHeight, leftWallPos.z);

		Vector3 rightWallPos = _rightBoundary.gameObject.transform.position;
		_rightBoundary.gameObject.transform.position = new Vector3 (rightWallPos.x, _currentHeight, rightWallPos.z);

		_velocity.x = input * _moveSpeed;
		_velocity.y += _gravity * Time.deltaTime;

		transform.position += _velocity * Time.deltaTime;
		if (_currentHeight > _maxHeight) {
			_maxHeight = Mathf.FloorToInt (transform.position.y);
			_maxHeightEvent.Invoke (_maxHeight);
		} else if(_currentHeight < _maxHeight - Config.PLAYER_FALL_THRESHOLD) {
			_gameOverEvent.Invoke ();
			StartCoroutine (LoadGameOverScene());

		}

		if (transform.position.y <= 1.5f) {
			_velocity.y = _bounceSpeed;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Boundary")) {
			if (other.gameObject == _rightBoundary && _velocity.x < 0f) {
				transform.position = new Vector3 (_leftBoundary.transform.position.x + 1f, transform.position.y, transform.position.z);
			} else if (other.gameObject == _leftBoundary && _velocity.x > 0f) {
				transform.position = new Vector3 (_rightBoundary.transform.position.x - 1f, transform.position.y, transform.position.z);
			}
		} else if (other.gameObject.layer == LayerMask.NameToLayer ("Platform")) {
			if (_velocity.y < 0f) {
				_velocity.y = _bounceSpeed;
			}
		}
	}

	IEnumerator LoadGameOverScene () {
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("GameOver");
		yield return null;
	}
}
