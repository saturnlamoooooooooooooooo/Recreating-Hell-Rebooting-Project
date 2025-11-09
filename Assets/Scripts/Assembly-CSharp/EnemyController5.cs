using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController5 : MonoBehaviour
{
	public Animator Fadedranim;

	public AudioSource EndoSound2;

	public AudioSource EndoSound;

	private NavMeshAgent agent;

	public Transform checkpoint;

	public Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player3").transform;
		agent = GetComponent<NavMeshAgent>();
		Invoke("move", 5f / ((float)Settings5.hour + Settings5.mode));
	}

	private void move()
	{
		CheckPoint5 component = checkpoint.GetComponent<CheckPoint5>();
		EndoSound2.Play();
		EndoSound.Play();
		if (component is ControlPoints5)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up), out hitInfo))
			{
				if (hitInfo.collider.tag == "Player3")
				{
					SceneManager.LoadScene(25);
				}
				Fadedranim.SetBool("State", true);
				checkpoint = component.getNext();
				agent.destination = checkpoint.position;
				Invoke("move", 5f / (float)Settings5.hour);
			}
		}
		else
		{
			Fadedranim.SetBool("State", false);
			checkpoint = component.getNext();
			agent.destination = checkpoint.position;
			Invoke("move", 5f / (float)Settings5.hour);
		}
	}

	private void Update()
	{
		Debug.DrawRay(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up));
		Fadedranim.SetBool("State", false);
	}
}
