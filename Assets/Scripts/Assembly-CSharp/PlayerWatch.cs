using UnityEngine;

public class PlayerWatch : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Animatronick")
		{
			col.GetComponent<AiSeeAndMove>().Target = false;
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Animatronick")
		{
			col.GetComponent<AiSeeAndMove>().Target = true;
		}
	}
}
