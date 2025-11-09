using System.Collections;
using UnityEngine;

public class CrowbarScript : MonoBehaviour
{
	public GameObject Light;

	public GameObject TextE;

	public GameObject sound;

	public GameObject Task1;

	public GameObject Task2;

	public GameObject AnimatronicOff1;

	public GameObject AnimatronicOff2;

	public GameObject AnimatronicOff12;

	public GameObject AnimatronicOff22;

	public GameObject CrowbarModel;

	public CrowbarScript script;

	public GameObject unlock;

	public GameObject locked;

	public GameObject doorOpen;

	public GameObject doorClose;

	public GameObject triggerAnimatronicActivated;

	public GameObject TriggerRotateEnemy;

	private void Awake()
	{
		Light.SetActive(false);
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(true);
			Light.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				sound.SetActive(true);
				CrowbarModel.SetActive(false);
				AnimatronicOff1.SetActive(false);
				AnimatronicOff2.SetActive(false);
				AnimatronicOff12.SetActive(true);
				AnimatronicOff22.SetActive(true);
				Task1.SetActive(false);
				locked.SetActive(false);
				unlock.SetActive(true);
				doorOpen.SetActive(true);
				doorClose.SetActive(false);
				TriggerRotateEnemy.SetActive(true);
				StartCoroutine("task");
				triggerAnimatronicActivated.SetActive(true);
			}
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(false);
			Light.SetActive(false);
		}
	}

	private IEnumerator task()
	{
		yield return new WaitForSeconds(2f);
		Task2.SetActive(true);
		Light.SetActive(false);
		Object.Destroy(base.gameObject);
	}

	private void Update()
	{
	}
}
