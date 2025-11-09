using System.Collections;
using UnityEngine;

public class TriggerJustGen2 : MonoBehaviour
{
	public GameObject Sub;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			StartCoroutine(Task());
		}
	}

	private IEnumerator Task()
	{
		yield return new WaitForSeconds(4f);
		Sub.SetActive(true);
	}
}
