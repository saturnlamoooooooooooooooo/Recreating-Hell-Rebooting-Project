using UnityEngine;

public class vison0 : MonoBehaviour
{
	public bool isSeeing;

	public GameObject Player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			isSeeing = true;
			Player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		isSeeing = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			isSeeing = true;
			Player = other.gameObject;
		}
	}
}
