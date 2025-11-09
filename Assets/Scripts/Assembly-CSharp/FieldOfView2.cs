using System.Collections;
using UnityEngine;

public class FieldOfView2 : MonoBehaviour
{
	public float radius;

	[Range(0f, 360f)]
	public float angle;

	public GameObject[] playerRef = new GameObject[0];

	public LayerMask targetMask;

	public LayerMask ObstacleMask;

	public bool canSeePlayer;

	public float scet;

	private void Start()
	{
		StartCoroutine(FOVRoutine());
	}

	private IEnumerator FOVRoutine()
	{
		WaitForSeconds wait = new WaitForSeconds(0.2f);
		while (true)
		{
			yield return wait;
			Frr();
		}
	}

	public void FixedUpdate()
	{
		if (scet == 0f)
		{
			Frr();
		}
		if (canSeePlayer)
		{
			scet += 1f * Time.deltaTime;
		}
		if (scet >= 0.2f)
		{
			canSeePlayer = false;
			scet = 0f;
			Frr();
		}
		playerRef = GameObject.FindGameObjectsWithTag("Player");
		if (canSeePlayer)
		{
			base.gameObject.GetComponent<AI_Monster2>().AI_Enemy = AI_Monster2.AI_State.Chase;
		}
	}

	private void Frr()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, radius, targetMask);
		if (array.Length == 0)
		{
			return;
		}
		Transform transform = array[0].transform;
		Vector3 normalized = (transform.position - base.transform.position).normalized;
		if (Vector3.Angle(base.transform.forward, normalized) < angle / 2f)
		{
			float maxDistance = Vector3.Distance(base.transform.position, transform.position);
			if (!Physics.Raycast(base.transform.position, normalized, maxDistance, ObstacleMask))
			{
				GameObject[] array2 = playerRef;
				foreach (GameObject gameObject in array2)
				{
					Vector3 normalized2 = (gameObject.transform.position - base.transform.position).normalized;
					if (Vector3.Angle(base.transform.forward, normalized2) < angle / 2f)
					{
						Vector3.Dot(base.transform.forward, normalized2);
						float maxDistance2 = Vector3.Distance(base.transform.position, gameObject.transform.position);
						if (!Physics.Raycast(base.transform.position, normalized2, maxDistance2, ObstacleMask))
						{
							base.gameObject.GetComponent<AI_Monster2>().AI_Enemy = AI_Monster2.AI_State.Stay;
						}
						else
						{
							canSeePlayer = false;
						}
					}
				}
			}
			else
			{
				canSeePlayer = false;
			}
		}
		else
		{
			canSeePlayer = false;
		}
	}
}
