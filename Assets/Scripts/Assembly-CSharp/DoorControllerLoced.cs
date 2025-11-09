using System.Collections;
using UnityEngine;

public class DoorControllerLoced : MonoBehaviour
{
	public GameObject SoundLock;

	public GameObject TextE;

	public GameObject Sub;

	public GameObject Sub2;

	public GameObject Task;

	public GameObject Task2;

	public GameObject ControlCenter;

	private void Awake()
	{
		TextE.SetActive(false);
		ControlCenter.SetActive(false);
	}

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				SoundLock.SetActive(true);
				StartCoroutine("door");
				Task2.SetActive(false);
			}
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(false);
		}
	}

	private IEnumerator door()
	{
		yield return new WaitForSeconds(1.1f);
		Sub.SetActive(true);
		ControlCenter.SetActive(true);
		yield return new WaitForSeconds(7f);
		Sub.SetActive(false);
		Sub2.SetActive(true);
		yield return new WaitForSeconds(11f);
		Sub2.SetActive(false);
		Task.SetActive(true);
	}
}
