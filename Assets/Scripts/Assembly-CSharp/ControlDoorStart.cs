using System.Collections;
using UnityEngine;

public class ControlDoorStart : MonoBehaviour
{
	public GameObject Soundkey;

	public GameObject TextE;

	public GameObject SoundDoorOpen;

	public Animator Door;

	public GameObject triggerstop;

	public Animator Control;

	private void Awake()
	{
		TextE.SetActive(false);
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
				Soundkey.SetActive(true);
				StartCoroutine("door");
				Control.SetBool("On", true);
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
		yield return new WaitForSeconds(4f);
		SoundDoorOpen.SetActive(true);
		Door.SetBool("On", true);
		yield return new WaitForSeconds(6f);
		triggerstop.SetActive(false);
	}
}
