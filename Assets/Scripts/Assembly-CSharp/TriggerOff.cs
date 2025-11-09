using UnityEngine;

public class TriggerOff : MonoBehaviour
{
	public GameObject Ambient;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Object.Destroy(Ambient);
		}
	}
}
