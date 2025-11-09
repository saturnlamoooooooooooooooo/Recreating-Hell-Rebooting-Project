using UnityEngine;

public class TriggerGen2 : MonoBehaviour
{
	public GameObject animatronik;

	public GameObject Object1;

	public GameObject Ambient;

	public GameObject ThisTrigger;

	public GameObject TaskOn;

	private void Start()
	{
		ThisTrigger.SetActive(false);
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			animatronik.SetActive(true);
			Object1.SetActive(true);
			Ambient.SetActive(true);
			TaskOn.SetActive(true);
			Object.Destroy(base.gameObject);
		}
	}
}
