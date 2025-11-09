using UnityEngine;

public class Script1Off1On : MonoBehaviour
{
	public GameObject On;

	public GameObject Off;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			On.SetActive(true);
			Off.SetActive(false);
			Object.Destroy(base.gameObject);
		}
	}
}
