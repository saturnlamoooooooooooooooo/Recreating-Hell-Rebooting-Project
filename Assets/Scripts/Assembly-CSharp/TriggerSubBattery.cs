using UnityEngine;

public class TriggerSubBattery : MonoBehaviour
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
		if (scet >= 1f)
		{
			Sub1.SetActive(true);
			SubSounds.SetActive(true);
		}
		if (scet >= 6f)
		{
			Sub1.SetActive(false);
		}
	}
}
