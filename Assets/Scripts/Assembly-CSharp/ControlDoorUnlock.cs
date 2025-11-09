using System.Collections;
using UnityEngine;

public class ControlDoorUnlock : MonoBehaviour
{
	public GameObject SoundUnlock;

	public GameObject TextE;

	public GameObject Task;

	public GameObject Task2;

	public GameObject Unlock;

	public GameObject Open;

	public Animator ChainBrake;

	public GameObject Chain;

	public GameObject ControlpanelOff;

	public GameObject Animatr1;

	public GameObject Animatr2;

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
				SoundUnlock.SetActive(true);
				StartCoroutine("door");
				Task2.SetActive(false);
				ChainBrake.SetBool("On", true);
				Chain.SetActive(false);
				Animatr1.SetActive(false);
				Animatr2.SetActive(false);
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
		yield return new WaitForSeconds(2.1f);
		Task.SetActive(true);
		Unlock.SetActive(false);
		Open.SetActive(true);
		ControlpanelOff.SetActive(false);
		TextE.SetActive(false);
		yield return new WaitForSeconds(3f);
		yield return new WaitForSeconds(11f);
	}
}
