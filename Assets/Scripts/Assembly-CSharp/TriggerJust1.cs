using UnityEngine;

public class TriggerJust1 : MonoBehaviour
{
	public GameObject Sub;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Sub.SetActive(true);
		}
	}
}
