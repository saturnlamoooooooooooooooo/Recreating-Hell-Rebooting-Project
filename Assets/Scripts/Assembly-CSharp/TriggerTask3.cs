using System.Collections;
using UnityEngine;

public class TriggerTask3 : MonoBehaviour
{
	public GameObject task;

	public GameObject task2;

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
		yield return new WaitForSeconds(3f);
		task.SetActive(false);
		task2.SetActive(true);
		yield return new WaitForSeconds(4f);
		Object.Destroy(base.gameObject);
	}
}
