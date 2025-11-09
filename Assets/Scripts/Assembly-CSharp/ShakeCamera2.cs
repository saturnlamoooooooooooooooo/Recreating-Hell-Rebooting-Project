using System.Collections;
using UnityEngine;

public class ShakeCamera2 : MonoBehaviour
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
		if (other.tag == "StepOn2")
		{
			_isTrTrue = true;
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
		if (other.tag == "StepOn2")
		{
			_isTrTrue = false;
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}
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
			Random.Range(0.1f, -0.1f);
			float magnitude = _magnitude;
			Random.Range(0.1f, -0.1f);
			float magnitude2 = _magnitude;
			_mainCamera.transform.localPosition = new Vector3(origin.x, origin.y, origin.z);
			time += Time.deltaTime;
			yield return null;
		}
		_mainCamera.transform.localPosition = origin;
	}
}
