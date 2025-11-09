using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController3 : MonoBehaviour
{
	public AudioSource VentsSound;

	private NavMeshAgent agent;

	public Transform checkpoint;

	public Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player2").transform;
		agent = GetComponent<NavMeshAgent>();
		Invoke("move", 75f / ((float)Settings3.hour + Settings3.mode));
	}

	private void move()
	{
		VentsSound.Play();
		CheckPoint3 component = checkpoint.GetComponent<CheckPoint3>();
		if (component is ControlPoints3)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up), out hitInfo))
			{
				if (hitInfo.collider.tag == "Player2")
				{
					SceneManager.LoadScene(5);
				}
				checkpoint = component.getNext();
				agent.destination = checkpoint.position;
				Invoke("move", 9f / (float)Settings3.hour);
			}
		}
		else
		{
			checkpoint = component.getNext();
			agent.destination = checkpoint.position;
			Invoke("move", 9f / (float)Settings3.hour);
		}
	}

	private void Update()
	{
		Debug.DrawRay(base.transform.position, player.position - base.transform.position);
	}
}
