using UnityEngine;

public class ScriptSlesh : MonoBehaviour
{
	public Transform target;

	public Transform playerTransform;

	public Vector3 offset;

	public float camPositionSpeed = 10f;

	public float camRotationSpeed = 10f;

	private void FixedUpdate()
	{
		Vector3 b = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, playerTransform.position.z + offset.z);
		new Vector3(playerTransform.rotation.x + offset.x, playerTransform.rotation.y + offset.y, playerTransform.rotation.z + offset.z);
		base.transform.position = Vector3.Lerp(base.transform.position, b, camPositionSpeed * Time.deltaTime);
		base.transform.LookAt(target);
	}
}
