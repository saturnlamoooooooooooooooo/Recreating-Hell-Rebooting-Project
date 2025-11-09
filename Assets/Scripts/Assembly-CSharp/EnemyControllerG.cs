using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerG : MonoBehaviour
{
	public GameObject AnimatrModel;

	private NavMeshAgent agent;

	public Transform checkpoint;

	public Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("PlayerG").transform;
		agent = GetComponent<NavMeshAgent>();
		Invoke("move", 1f / ((float)Settings.hour + Settings.mode));
	}

	private void move()
	{
		CheckPointG component = checkpoint.GetComponent<CheckPointG>();
		if (component is ControlPointsG)
		{
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up), out hitInfo))
			{
				if (hitInfo.collider.tag == "PlayerG")
				{
					AnimatrModel.SetActive(true);
				}
				checkpoint = component.getNext();
				agent.destination = checkpoint.position;
				Invoke("move", 2f / (float)SettingsG.hour);
			}
		}
		else
		{
			checkpoint = component.getNext();
			agent.destination = checkpoint.position;
			Invoke("move", 2f / (float)SettingsG.hour);
		}
	}

	private void Update()
	{
		Debug.DrawRay(base.transform.position + base.transform.up, player.position - (base.transform.position + base.transform.up));
	}
}
