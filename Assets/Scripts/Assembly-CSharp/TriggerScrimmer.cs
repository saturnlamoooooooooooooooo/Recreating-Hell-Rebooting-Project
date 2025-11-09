using UnityEngine;

public class TriggerScrimmer : MonoBehaviour
{
	public GameObject Scream;

	public bool Enter;

	public int Time;

	private void Start()
	{
	}

	private void Update()
	{
		if (Enter)
		{
			Time++;
			if (Time >= 100)
			{
				Object.Destroy(Scream);
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			Scream.GetComponent<Animator>().SetTrigger("Scream");
			Scream.GetComponent<AudioSource>().enabled = true;
			Enter = true;
		}
	}
}
