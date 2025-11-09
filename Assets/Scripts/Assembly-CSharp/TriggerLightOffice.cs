using UnityEngine;

public class TriggerLightOffice : MonoBehaviour
{
	public GameObject Light1;

	public GameObject Light2;

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Endo")
		{
			Light1.SetActive(true);
			Light2.SetActive(false);
		}
	}
}
