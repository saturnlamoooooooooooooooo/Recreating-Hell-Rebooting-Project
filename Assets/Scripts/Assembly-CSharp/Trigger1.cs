using UnityEngine;

public class Trigger1 : MonoBehaviour
{
	public GameObject animatronik;

	public GameObject Object1;

	public GameObject Ambient;

	private void Start()
	{
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
			Object.Destroy(base.gameObject);
		}
	}
}
