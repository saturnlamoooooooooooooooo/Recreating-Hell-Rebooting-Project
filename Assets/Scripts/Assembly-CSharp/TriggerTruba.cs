using UnityEngine;

public class TriggerTruba : MonoBehaviour
{
	public GameObject TrubaSound;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			TrubaSound.SetActive(true);
		}
	}
}
