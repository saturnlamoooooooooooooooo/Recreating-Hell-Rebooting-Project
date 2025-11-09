using UnityEngine;

public class SubScript2 : MonoBehaviour
{
	public float scet;

	public GameObject Sub1;

	public GameObject Sub2;

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
		if ((double)scet >= 0.6)
		{
			Sub1.SetActive(true);
		}
		if ((double)scet >= 3.2)
		{
			Sub1.SetActive(false);
		}
		if ((double)scet >= 3.6)
		{
			Sub2.SetActive(true);
		}
		if (scet >= 7f)
		{
			Sub2.SetActive(false);
		}
		if (scet >= 10f)
		{
			SubSounds.SetActive(false);
		}
	}
}
