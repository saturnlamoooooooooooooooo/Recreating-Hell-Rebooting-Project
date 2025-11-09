using UnityEngine;

public class TriggerON : MonoBehaviour
{
	public GameObject Glitch;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Glitch.SetActive(true);
			Object.Destroy(base.gameObject);
		}
	}
}
