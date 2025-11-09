using UnityEngine;

public class TriggerLightOnendo : MonoBehaviour
{
	public GameObject Light1;

	public GameObject Light2;

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Endo")
		{
			Light1.SetActive(false);
			Light2.SetActive(true);
		}
	}
}
