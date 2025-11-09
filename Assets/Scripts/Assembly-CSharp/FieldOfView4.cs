using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView4 : MonoBehaviour
{
	public float radius;

	[Range(0f, 360f)]
	public float angle;

	public GameObject[] playerRef = new GameObject[0];

	public LayerMask targetMask;

	public LayerMask ObstacleMask;

	public bool canSeePlayer;

	public float scet;

	public GameObject[] animat = new GameObject[0];

	public AI_Monster4 Orange;

	private void Start()
	{
		StartCoroutine(FOVRoutine());
	}

	public IEnumerator ChaseRun()
	{
		yield return new WaitForSeconds(0.1f);
		List<Animator> list = new List<Animator>();
		GameObject[] array = animat;
		for (int i = 0; i < array.Length; i++)
		{
			Animator component = array[i].GetComponent<Animator>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		list.ToArray();
		foreach (Animator anim in list)
		{
			if (Orange.AI_Enemy == AI_Monster4.AI_State.Patrol && anim.GetBool("walk") && anim.GetBool("run"))
			{
				yield return new WaitForSeconds(0.8f);
				if (anim.GetBool("walk") && anim.GetBool("run"))
				{
					canSeePlayer = true;
				}
			}
		}
	}

	private IEnumerator FOVRoutine()
	{
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		while (true)
		{
			yield return wait;
		}
	}

	public void FixedUpdate()
	{
		if (canSeePlayer)
		{
			scet += 1f * Time.deltaTime;
		}
		if (scet >= 0.2f)
		{
			scet = 0f;
		}
		animat = GameObject.FindGameObjectsWithTag("ModelFonarik");
		StartCoroutine(ChaseRun());
		playerRef = GameObject.FindGameObjectsWithTag("Player");
		if (canSeePlayer)
		{
			base.gameObject.GetComponent<AI_Monster4>().AI_Enemy = AI_Monster4.AI_State.Chase;
		}
	}

	private void Update()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, radius, targetMask);
		if (array.Length == 0)
		{
			return;
		}
		Transform transform = array[0].transform;
		Vector3 normalized = (transform.position - base.transform.position).normalized;
		if (!(Vector3.Angle(base.transform.forward, normalized) < angle / 2f))
		{
			return;
		}
		float maxDistance = Vector3.Distance(base.transform.position, transform.position);
		if (Physics.Raycast(base.transform.position, normalized, maxDistance, ObstacleMask))
		{
			return;
		}
		GameObject[] array2 = playerRef;
		foreach (GameObject gameObject in array2)
		{
			Vector3 normalized2 = (gameObject.transform.position - base.transform.position).normalized;
			if (Vector3.Angle(base.transform.forward, normalized2) < angle / 2f)
			{
				float maxDistance2 = Vector3.Distance(base.transform.position, gameObject.transform.position);
				if (!Physics.Raycast(base.transform.position, normalized2, maxDistance2, ObstacleMask))
				{
					canSeePlayer = true;
					break;
				}
			}
		}
	}
}
