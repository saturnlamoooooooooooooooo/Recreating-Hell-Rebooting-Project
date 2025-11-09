using UnityEngine;

public class SubScript5 : MonoBehaviour
{
	public float scet;

	public GameObject Sub1;

	public GameObject SubSounds;

	private void Start()
	{
	}

	private void Awake()
	{
		scet = 0f;
	}

	private void Update()
	{
		scet += 1f * Time.deltaTime;
		if (scet >= 3f)
		{
			SubSounds.SetActive(true);
		}
		if ((double)scet >= 3.6)
		{
			Sub1.SetActive(true);
		}
		if ((double)scet >= 8.5)
		{
			Sub1.SetActive(false);
		}
		if (scet >= 12f)
		{
			SubSounds.SetActive(false);
		}
	}
}
