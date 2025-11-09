using UnityEngine;

public class DisableOnPaly : MonoBehaviour
{
	public GameObject ObjectOff;

	private void Start()
	{
		ObjectOff.SetActive(false);
	}

	private void Update()
	{
	}
}
