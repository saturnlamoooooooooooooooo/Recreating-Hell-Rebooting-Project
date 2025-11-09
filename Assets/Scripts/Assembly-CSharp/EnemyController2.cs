using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController2 : MonoBehaviour
{
	private NavMeshAgent agent;

	public Transform checkpoint;

	public Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		Invoke("move", 120f / ((float)Settings2.hour + Settings2.mode));
	}

	private void move()
	{
		CheckPoint2 component = checkpoint.GetComponent<CheckPoint2>();
		if (component is ControlPoints2)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up), out hitInfo))
			{
				if (hitInfo.collider.tag == "Player")
				{
					SceneManager.LoadScene(4);
				}
				checkpoint = component.getNext();
				agent.destination = checkpoint.position;
				Invoke("move", 16f / (float)Settings2.hour);
			}
		}
		else
		{
			checkpoint = component.getNext();
			agent.destination = checkpoint.position;
			Invoke("move", 16f / (float)Settings2.hour);
		}
	}

	private void Update()
	{
		Debug.DrawRay(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up));
	}
}
