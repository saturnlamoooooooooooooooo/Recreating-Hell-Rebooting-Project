using UnityEngine;

public class activecam2andsound2 : MonoBehaviour
{
	public GameObject cam2;

	public GameObject sound2;

	public GameObject cam1;

	private void Awake()
	{
		cam2.SetActive(false);
		sound2.SetActive(false);
		cam1.SetActive(false);
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			cam2.SetActive(true);
		}
		sound2.SetActive(true);
		cam1.SetActive(true);
	}
}
