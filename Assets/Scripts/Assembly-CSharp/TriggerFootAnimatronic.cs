using UnityEngine;

public class TriggerFootAnimatronic : MonoBehaviour
{
	public AudioSource audioS;

	public SnakeCamera Steps;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Foot")
		{
			Steps.Step();
			audioS.Play();
		}
	}
}
