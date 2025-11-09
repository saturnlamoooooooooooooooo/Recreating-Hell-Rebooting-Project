using UnityEngine;

public class AnimatronicActivate2 : MonoBehaviour
{
	public GameObject AnimatronicOn;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			AnimatronicOn.SetActive(true);
		}
	}
}
