using System.Collections;
using UnityEngine;

public class SnakeCamera : MonoBehaviour
{
	public GameObject _mainCamera;

	public float _distance;

	private Coroutine _coroutine;

	public bool _isTrTrue;

	public float _magnitude;

	[SerializeField]
	private float _maxSnakeMagnitude = 0.2f;

	[SerializeField]
	private float _power = 1f;

	[SerializeField]
	private Transform _animatronicTransform;

	[SerializeField]
	private float _durationStep = 0.3f;

	[SerializeField]
	private float _koef = 10f;

	[SerializeField]
	private float _shakeIntensityMultiplier = 1f;

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "StepOn")
		{
			_isTrTrue = true;
		}
		if (other.tag == "StepOn2")
		{
			_maxSnakeMagnitude = 0.017f;
		}
	}

	public void Step()
	{
		if (_isTrTrue)
		{
			_coroutine = StartCoroutine(Snake());
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "StepOn")
		{
			_isTrTrue = false;
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}
		}
		if (other.tag == "StepOn2")
		{
			_maxSnakeMagnitude = 0.008f;
		}
	}

	private void Update()
	{
		if (_isTrTrue)
		{
			_distance = (_mainCamera.transform.position - _animatronicTransform.position).magnitude;
			float num = Mathf.Lerp(0f, _shakeIntensityMultiplier, _distance / _koef);
			_magnitude = num * _power;
			_magnitude = Mathf.Clamp(_magnitude, 0.01f, _maxSnakeMagnitude);
			Debug.Log(_magnitude);
		}
	}

	private IEnumerator Snake()
	{
		Vector3 origin = _mainCamera.transform.localPosition;
		float time = 0f;
		while (_isTrTrue && time < _durationStep)
		{
			float x = Random.Range(-0.2f, 0.2f) * _magnitude;
			float y = Random.Range(-0.2f, 0.2f) * _magnitude + 0.8f;
			_mainCamera.transform.localPosition = new Vector3(x, y, origin.z);
			time += Time.deltaTime;
			yield return null;
		}
		_mainCamera.transform.localPosition = origin;
	}
}
