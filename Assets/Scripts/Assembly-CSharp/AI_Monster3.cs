using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Monster3 : MonoBehaviour
{
	public enum AI_State
	{
		Patrol = 0,
		Stay = 1,
		Chase = 2
	}

	private NavMeshAgent AI_Agent;

	public GameObject[] Players;

	public float walkspeed;

	public float runspeed;

	public List<Transform> WayPoints = new List<Transform>();

	public int Current_Patch;

	public AI_State AI_Enemy;

	private Transform Last_point;

	public bool Check_LastPoint;

	private float i_stay;

	private void Start()
	{
		AI_Agent = base.gameObject.GetComponent<NavMeshAgent>();
	}

	private void FixedUpdate()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Phantom");
		WayPoints.Clear();
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			WayPoints.Add(gameObject.transform);
		}
		Players = GameObject.FindGameObjectsWithTag("Player");
		if (!Check_LastPoint)
		{
			if (AI_Enemy == AI_State.Patrol)
			{
				AI_Agent.speed = walkspeed;
				AI_Agent.Resume();
				AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
				if (Vector3.Distance(WayPoints[Current_Patch].transform.position, base.gameObject.transform.position) < 2f)
				{
					Current_Patch++;
					Current_Patch %= WayPoints.Count;
				}
			}
			if (AI_Enemy == AI_State.Stay)
			{
				AI_Agent.speed = 0f;
				i_stay += 1f * Time.deltaTime;
				if ((double)i_stay >= 0.1)
				{
					AI_Enemy = AI_State.Chase;
					base.gameObject.GetComponent<FieldOfView3>().canSeePlayer = true;
					i_stay = 0f;
				}
			}
			if (AI_Enemy != AI_State.Chase)
			{
				return;
			}
			AI_Agent.speed = runspeed;
			AI_Agent.Resume();
			GameObject gameObject2 = FindClosestTarget(Players, base.transform);
			if (gameObject2 != null)
			{
				if (!base.gameObject.GetComponent<FieldOfView3>().canSeePlayer)
				{
					Last_point = gameObject2.transform;
					Check_LastPoint = true;
				}
				else
				{
					AI_Agent.SetDestination(gameObject2.transform.position);
				}
			}
		}
		else
		{
			AI_Agent.Resume();
			i_stay += 1f * Time.deltaTime;
			float num = Vector3.Distance(Last_point.transform.position, base.gameObject.transform.position);
			FindClosestTarget(Players, base.transform);
			if (num < 0f || i_stay >= 4f)
			{
				Check_LastPoint = false;
				AI_Enemy = AI_State.Patrol;
				i_stay = 0f;
			}
		}
	}

	public GameObject FindClosestTarget(GameObject[] targets, Transform currentPosition)
	{
		GameObject result = null;
		float num = float.PositiveInfinity;
		foreach (GameObject gameObject in targets)
		{
			float num2 = Vector3.Distance(currentPosition.position, gameObject.transform.position);
			if (num2 < num)
			{
				num = num2;
				result = gameObject;
			}
		}
		return result;
	}
}
