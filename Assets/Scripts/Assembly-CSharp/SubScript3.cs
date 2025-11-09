using UnityEngine;

public class SubScript3 : MonoBehaviour
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
		if ((double)scet >= 0.1)
		{
			SubSounds.SetActive(true);
		}
		if ((double)scet >= 0.5)
		{
			Sub1.SetActive(true);
		}
		if ((double)scet >= 3.5)
		{
			Sub1.SetActive(false);
		}
		if (scet >= 8f)
		{
			SubSounds.SetActive(false);
		}
	}
}
