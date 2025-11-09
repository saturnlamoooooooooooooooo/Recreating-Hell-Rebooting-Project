using UnityEngine;
using UnityEngine.AI;

public class robot : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField]
	private Transform target;

	private void Start()
	{
	}

	private void Update()
	{
		agent.SetDestination(target.position);
	}
}
