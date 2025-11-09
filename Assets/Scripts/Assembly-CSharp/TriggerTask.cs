using System.Collections;
using UnityEngine;

public class TriggerTask : MonoBehaviour
{
	public GameObject task;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			StartCoroutine(Task());
		}
	}

	private IEnumerator Task()
	{
		yield return new WaitForSeconds(7f);
		task.SetActive(true);
	}
}
