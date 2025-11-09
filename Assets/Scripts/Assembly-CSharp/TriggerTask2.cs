using UnityEngine;

public class TriggerTask2 : MonoBehaviour
{
	public GameObject task;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			task.SetActive(false);
		}
	}
}
