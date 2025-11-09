using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
	private NavMeshAgent agent;

	public Transform checkpoint;

	public Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		Invoke("move", 40f / ((float)Settings.hour + Settings.mode));
	}

	private void move()
	{
		CheckPoint component = checkpoint.GetComponent<CheckPoint>();
		if (component is ControlPoints)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up), out hitInfo))
			{
				if (hitInfo.collider.tag == "Player")
				{
					SceneManager.LoadScene(3);
				}
				checkpoint = component.getNext();
				agent.destination = checkpoint.position;
				Invoke("move", 13f / (float)Settings.hour);
			}
		}
		else
		{
			checkpoint = component.getNext();
			agent.destination = checkpoint.position;
			Invoke("move", 13f / (float)Settings.hour);
		}
	}

	private void Update()
	{
		Debug.DrawRay(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up));
	}
}
