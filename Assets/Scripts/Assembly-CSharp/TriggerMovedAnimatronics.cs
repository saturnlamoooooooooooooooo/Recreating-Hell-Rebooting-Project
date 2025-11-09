using UnityEngine;

public class TriggerMovedAnimatronics : MonoBehaviour
{
	public GameObject animatronikoff1;

	public GameObject animatronikoff2;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			animatronikoff1.SetActive(false);
			animatronikoff2.SetActive(true);
			Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
	}
}
