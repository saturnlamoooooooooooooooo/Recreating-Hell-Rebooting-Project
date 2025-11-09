using UnityEngine;

public class Finalscript : MonoBehaviour
{
	public GameObject cam2;

	public GameObject sound2;

	public GameObject sound;

	private void Awake()
	{
		cam2.SetActive(false);
		sound2.SetActive(false);
		sound.SetActive(false);
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
		sound.SetActive(true);
	}
}
