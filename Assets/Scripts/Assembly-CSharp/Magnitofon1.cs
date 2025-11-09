using UnityEngine;

public class Magnitofon1 : MonoBehaviour
{
	public GameObject TriggerOff;

	public GameObject TextE;

	public GameObject Sub;

	public GameObject Sound;

	public GameObject Button1;

	public GameObject Button2;

	public GameObject Light;

	private void Awake()
	{
		TextE.SetActive(false);
		Light.SetActive(false);
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(true);
			Light.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				TriggerOff.SetActive(true);
				Sub.SetActive(true);
				Sound.SetActive(true);
				Object.Destroy(TextE);
				Button1.SetActive(false);
				Button2.SetActive(true);
				Object.Destroy(Light);
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
}
