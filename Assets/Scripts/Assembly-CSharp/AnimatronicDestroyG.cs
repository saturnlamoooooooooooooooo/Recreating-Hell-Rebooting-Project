using UnityEngine;

public class AnimatronicDestroyG : MonoBehaviour
{
	public GameObject Animatronicmodel;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "AnimatronicG")
		{
			Animatronicmodel.SetActive(false);
		}
	}
}
