using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeightController : MonoBehaviour {

	private float _currentHeight;

	public AnimationCurve DistanceCurve;

	[SerializeField]
	private float _animationSpeed = 2f;

	[SerializeField]
	private float _cameraOffset = 0.0f;

	private Coroutine _heightChangeCoroutine = null;

	// Use this for initialization
	void Start () {
		_currentHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMaxHeight(float playerHeight) {

		if (playerHeight > _currentHeight) {
			if (_heightChangeCoroutine != null) {
				StopCoroutine (_heightChangeCoroutine);
				_heightChangeCoroutine = null;
			}
			_heightChangeCoroutine = StartCoroutine (ChangeCameraHeight (transform.position.y, playerHeight));
		}
	}

	public void OnPlayerFall() {
		StartCoroutine (LoadGameOverScreen ());
	}

	IEnumerator ChangeCameraHeight(float currentHeight, float newHeight) {
		
		float curveTime = 0.0f;
		float fraction = DistanceCurve.Evaluate (curveTime);
		while (curveTime < 1.0f) {
			curveTime += Time.deltaTime;
			fraction = DistanceCurve.Evaluate (curveTime);
			transform.position = new Vector3 (transform.position.x, currentHeight + (newHeight - currentHeight) * fraction, transform.position.z);
			yield return null;
		}
	}

	IEnumerator LoadGameOverScreen(){
		yield return null;
	}
}
