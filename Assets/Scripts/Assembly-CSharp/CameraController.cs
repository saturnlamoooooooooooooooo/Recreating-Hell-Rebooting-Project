using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float amount = 1f;

	public float speed = 1f;

	private Vector3 startPos;

	private float distation;

	private Vector3 rotation = Vector3.zero;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		distation += (base.transform.position - startPos).magnitude;
		startPos = base.transform.position;
		rotation.z = Mathf.Sin(distation * speed) * amount;
		base.transform.localEulerAngles += rotation;
	}
}
