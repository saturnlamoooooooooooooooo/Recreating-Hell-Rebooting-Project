using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Monster2 : MonoBehaviour
{
	public enum AI_State
	{
		Patrol = 0,
		Stay = 1,
		Chase = 2
	}

	private NavMeshAgent AI_Agent;

	public GameObject[] Players;

	public Animator animation;

	public Animator box;

	public Animator boxrun;

	public GameObject boxIntrige;

	public float walkspeed;

	public float runspeed;

	public Animator HideTrigger;

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
		GameObject[] array = GameObject.FindGameObjectsWithTag("Points2");
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
				HideTrigger.SetBool("Run", false);
				boxIntrige.SetActive(false);
				animation.SetBool("stay", false);
				AI_Agent.speed = walkspeed;
				AI_Agent.Resume();
				animation.SetBool("IsOpen", false);
				box.SetBool("IsOpen", false);
				boxrun.SetBool("IsOpen", false);
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
				boxIntrige.SetActive(true);
				animation.SetBool("stay", true);
				i_stay += 1f * Time.deltaTime;
				boxrun.SetBool("IsOpen", false);
				if (i_stay >= 3f)
				{
					AI_Enemy = AI_State.Chase;
					base.gameObject.GetComponent<FieldOfView2>().canSeePlayer = true;
					i_stay = 0f;
				}
			}
			if (AI_Enemy != AI_State.Chase)
			{
				return;
			}
			HideTrigger.SetBool("Run", true);
			boxIntrige.SetActive(false);
			boxrun.SetBool("IsOpen", true);
			animation.SetBool("stay", false);
			AI_Agent.speed = runspeed;
			AI_Agent.Resume();
			animation.SetBool("IsOpen", true);
			box.SetBool("IsOpen", true);
			GameObject gameObject2 = FindClosestTarget(Players, base.transform);
			if (gameObject2 != null)
			{
				if (!base.gameObject.GetComponent<FieldOfView2>().canSeePlayer)
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
			if (num < 7f)
			{
				animation.SetBool("stay", true);
				boxrun.SetBool("IsOpen", false);
			}
			FindClosestTarget(Players, base.transform);
			if (num < 0f || i_stay >= 4f)
			{
				animation.SetBool("stay", false);
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
