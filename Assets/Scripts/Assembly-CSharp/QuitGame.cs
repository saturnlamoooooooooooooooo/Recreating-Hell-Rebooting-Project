using UnityEngine;

public class QuitGame : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "trigg")
		{
			Application.Quit();
		}
	}
}
