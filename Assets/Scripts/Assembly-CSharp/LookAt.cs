using UnityEngine;

public class LookAt : MonoBehaviour
{
	public float speed = 5f;

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
			base.transform.LookAt(target);
		}
	}

	private void FindTarget()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("triggerE");
		if (gameObject != null)
		{
			target = gameObject.transform;
		}
	}
}
