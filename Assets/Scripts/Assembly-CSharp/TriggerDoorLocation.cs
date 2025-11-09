using UnityEngine;

public class TriggerDoorLocation : MonoBehaviour
{
	public GameObject DoorOpen;

	public GameObject DoorClose;

	private void Awake()
	{
		DoorOpen.SetActive(false);
		DoorClose.SetActive(true);
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			DoorOpen.SetActive(true);
			DoorClose.SetActive(false);
			Object.Destroy(base.gameObject);
		}
	}
}
