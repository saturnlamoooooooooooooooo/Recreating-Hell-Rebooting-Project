using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController4 : MonoBehaviour
{
	public Animator Fadedranim;

	public AudioSource EndoSound;

	private NavMeshAgent agent;

	public Transform checkpoint;

	public Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		Invoke("move", 10f / ((float)Settings4.hour + Settings4.mode));
	}

	private void move()
	{
		CheckPoint4 component = checkpoint.GetComponent<CheckPoint4>();
		EndoSound.Play();
		if (component is ControlPoints4)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up), out hitInfo))
			{
				if (hitInfo.collider.tag == "Player")
				{
					SceneManager.LoadScene(16);
				}
				Fadedranim.SetBool("State", true);
				checkpoint = component.getNext();
				agent.destination = checkpoint.position;
				Invoke("move", 6f / (float)Settings4.hour);
			}
		}
		else
		{
			Fadedranim.SetBool("State", false);
			checkpoint = component.getNext();
			agent.destination = checkpoint.position;
			Invoke("move", 6f / (float)Settings4.hour);
		}
	}

	private void Update()
	{
		Debug.DrawRay(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up));
		Fadedranim.SetBool("State", false);
	}
}
