using UnityEngine;

public class SubScript1 : MonoBehaviour
{
	public float scet;

	public GameObject TriggerF;

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
		if ((double)scet >= 1.5)
		{
			Sub1.SetActive(true);
		}
		if ((double)scet >= 3.7)
		{
			Sub1.SetActive(false);
		}
		if (scet >= 5f)
		{
			SubSounds.SetActive(false);
		}
		if (scet >= 6f)
		{
			TriggerF.SetActive(true);
		}
	}
}
