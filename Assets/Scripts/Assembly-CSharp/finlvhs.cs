using UnityEngine;

public class finlvhs : MonoBehaviour
{
	public GameObject cam2;

	public GameObject sound2;

	public GameObject sound;

	public GameObject sound3;

	public GameObject sound4;

	private void Awake()
	{
		cam2.SetActive(false);
		sound2.SetActive(false);
		sound.SetActive(false);
		sound3.SetActive(false);
		sound4.SetActive(true);
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
		sound3.SetActive(true);
		sound4.SetActive(true);
	}
}
