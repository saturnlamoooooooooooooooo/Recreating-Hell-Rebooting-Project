using UnityEngine;

public class Arms : MonoBehaviour
{
	public Transform cameraTransform;

	public float swayAmount = 0.02f;

	public float maxSwayAmount = 0.1f;

	public float smoothFactor = 4f;

	private Vector3 initialPosition;

	private void Start()
	{
		initialPosition = base.transform.localPosition;
	}

	private void Update()
	{
		float axis = Input.GetAxis("Mouse Y");
		float x = Mathf.Clamp((0f - Input.GetAxis("Mouse X")) * swayAmount, 0f - maxSwayAmount, maxSwayAmount);
		float y = Mathf.Clamp(axis * swayAmount, 0f - maxSwayAmount, maxSwayAmount);
		Vector3 b = initialPosition + new Vector3(x, y, 0f);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b, Time.deltaTime * smoothFactor);
	}
}
