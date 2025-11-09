using UnityEngine;

public class AnimatronicsOffTrigger : MonoBehaviour
{
	public GameObject Animatronic1;

	public GameObject Animatronic2;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Animatronic1.SetActive(false);
			Animatronic2.SetActive(false);
		}
	}

	private void Update()
	{
	}
}
