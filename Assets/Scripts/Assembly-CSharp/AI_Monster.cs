using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Monster : MonoBehaviour
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

	public GameObject boxIntrige;

	public float walkspeed;

	public float runspeed;

	public Animator boxrun1;

	public Animator HideTrigger;

	public List<Transform> WayPoints = new List<Transform>();

	public int Current_Patch;

	public AI_State AI_Enemy;

	private Transform Last_point;

	public bool Check_LastPoint;

	private float i_stay;

	public float n_stay;

	public GameObject Shake;

	public GameObject Shake2;

	private void Start()
	{
		AI_Agent = base.gameObject.GetComponent<NavMeshAgent>();
	}

	private void FixedUpdate()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Points");
		WayPoints.Clear();
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			WayPoints.Add(gameObject.transform);
		}
		i_stay -= 1f * Time.deltaTime;
		n_stay -= 1f * Time.deltaTime;
		Players = GameObject.FindGameObjectsWithTag("Player");
		if (!Check_LastPoint)
		{
			if (AI_Enemy == AI_State.Patrol)
			{
				Shake.SetActive(true);
				Shake2.SetActive(true);
				animation.SetBool("stay", false);
				box.SetBool("stay", false);
				HideTrigger.SetBool("Run", false);
				boxrun1.SetBool("IsOpen", false);
				boxIntrige.SetActive(false);
				AI_Agent.speed = walkspeed;
				AI_Agent.Resume();
				animation.SetBool("IsOpen", false);
				box.SetBool("IsOpen", false);
				AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
				if (Vector3.Distance(WayPoints[Current_Patch].transform.position, base.gameObject.transform.position) < 2f)
				{
					Current_Patch++;
					Current_Patch %= WayPoints.Count;
				}
			}
			if (AI_Enemy == AI_State.Stay)
			{
				Shake.SetActive(false);
				Shake2.SetActive(false);
				boxrun1.SetBool("IsOpen", false);
				boxIntrige.SetActive(false);
				AI_Agent.speed = 0f;
				if (i_stay <= 0f)
				{
					AI_Enemy = AI_State.Patrol;
				}
			}
			if (AI_Enemy != AI_State.Chase)
			{
				return;
			}
			Shake.SetActive(true);
			Shake2.SetActive(true);
			n_stay = 5f;
			animation.SetBool("stay", false);
			box.SetBool("stay", false);
			HideTrigger.SetBool("Run", true);
			boxrun1.SetBool("IsOpen", true);
			boxIntrige.SetActive(true);
			AI_Agent.speed = runspeed;
			AI_Agent.Resume();
			animation.SetBool("IsOpen", true);
			box.SetBool("IsOpen", true);
			GameObject gameObject2 = FindClosestTarget(Players, base.transform);
			if (gameObject2 != null)
			{
				if (!base.gameObject.GetComponent<FieldOfView>().canSeePlayer)
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
			float num = Vector3.Distance(Last_point.transform.position, base.gameObject.transform.position);
			if (num < 5f)
			{
				Shake.SetActive(false);
				Shake2.SetActive(false);
				animation.SetBool("stay", true);
				box.SetBool("stay", true);
			}
			if (FindClosestTarget(Players, base.transform) != null && base.gameObject.GetComponent<FieldOfView>().canSeePlayer)
			{
				Shake.SetActive(true);
				Shake2.SetActive(true);
				animation.SetBool("stay", false);
				box.SetBool("stay", false);
				Check_LastPoint = false;
				AI_Enemy = AI_State.Chase;
			}
			if ((num < 2f) | (n_stay < 0f))
			{
				Shake.SetActive(false);
				Shake2.SetActive(false);
				animation.SetBool("stay", true);
				box.SetBool("stay", true);
				Check_LastPoint = false;
				AI_Enemy = AI_State.Stay;
				i_stay = 4f;
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
