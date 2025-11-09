using UnityEngine;

public class offFinalscript : MonoBehaviour
{
	public GameObject cam2;

	public GameObject sound2;

	public GameObject sound;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			cam2.SetActive(false);
		}
		sound2.SetActive(false);
		sound.SetActive(false);
	}
}
