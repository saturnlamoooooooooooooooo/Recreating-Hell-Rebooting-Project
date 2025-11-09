using UnityEngine;

public class MagnitofonOff : MonoBehaviour
{
	public GameObject Trigger;

	public GameObject Sound;

	public GameObject Sub;

	public Magnitofon1 Script;

	private void Awake()
	{
		Trigger.SetActive(false);
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Script.enabled = false;
			Sub.SetActive(false);
			Sound.SetActive(false);
			Object.Destroy(base.gameObject);
		}
	}
}
