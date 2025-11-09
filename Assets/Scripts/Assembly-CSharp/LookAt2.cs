using UnityEngine;

public class LookAt2 : MonoBehaviour
{
	public float speed;

	private Transform target;

	private void Start()
	{
		FindTarget();
	}

	private void Update()
	{
		if (target == null)
		{
			FindTarget();
		}
		if (target != null)
		{
			Vector3 vector = target.position - base.transform.position;
			float maxRadiansDelta = speed * Time.deltaTime;
			Vector3 forward = Vector3.RotateTowards(base.transform.forward, vector, maxRadiansDelta, 0f);
			base.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	private void FindTarget()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
		if (gameObject != null)
		{
			target = gameObject.transform;
		}
	}
}
