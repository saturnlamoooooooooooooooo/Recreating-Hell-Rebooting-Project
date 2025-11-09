using UnityEngine;

public class Trigger1Off : MonoBehaviour
{
	public GameObject Ambient;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Ambient.SetActive(false);
			Object.Destroy(base.gameObject);
		}
	}
}
