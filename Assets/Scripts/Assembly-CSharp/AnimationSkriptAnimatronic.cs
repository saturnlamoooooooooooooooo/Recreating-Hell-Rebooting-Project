using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnimationSkriptAnimatronic : MonoBehaviour
{
	public float speedThreshold = 10f;

	public Animator animation;

	public Animator box;

	public NavMeshAgent Nav;

	[Header("Patrolling")]
	public int targetpoint;

	public Transform[] walkPoints;

	public float Speed;

	[Header("Chase")]
	public float SpeedChase;

	public vison0 Vision;

	private int currentPont;

	private void Update()
	{
		if (!Vision.isSeeing)
		{
			Patrolling();
		}
		else
		{
			Chase();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Point"))
		{
			currentPont = targetpoint;
			while (currentPont == targetpoint)
			{
				targetpoint = Random.Range(0, walkPoints.Length);
			}
		}
	}

	private IEnumerator waitABit()
	{
		yield return new WaitForSeconds(2f);
		Nav.isStopped = false;
	}

	private void Patrolling()
	{
		Nav.speed = Speed;
		if (targetpoint >= walkPoints.Length)
		{
			targetpoint = 0;
		}
		Nav.SetDestination(walkPoints[targetpoint].position);
	}

	private void Chase()
	{
		Nav.speed = SpeedChase;
		Nav.SetDestination(Vision.Player.transform.position);
	}

	private void Awake()
	{
		animation.SetBool("IsOpen", false);
		box.SetBool("IsOpen", false);
	}

	private void FixedUpdate()
	{
		if (Nav.velocity.magnitude >= speedThreshold)
		{
			animation.SetBool("IsOpen", true);
			box.SetBool("IsOpen", true);
		}
		else if (Nav.velocity.magnitude < speedThreshold)
		{
			animation.SetBool("IsOpen", false);
			box.SetBool("IsOpen", false);
		}
	}
}
