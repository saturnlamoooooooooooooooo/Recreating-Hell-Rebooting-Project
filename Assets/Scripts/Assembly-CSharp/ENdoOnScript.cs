using UnityEngine;

public class ENdoOnScript : MonoBehaviour
{
	public GameObject BadStrigg;

	public GameObject BadStory;

	public GameObject EndoAnimatronick;

	public GameObject Soundboxes;

	public GameObject ModelAnimatro;

	public GameObject Warning;

	private void Awake()
	{
		BadStrigg.SetActive(false);
		BadStory.SetActive(false);
		EndoAnimatronick.SetActive(false);
		Soundboxes.SetActive(false);
		ModelAnimatro.SetActive(true);
		Warning.SetActive(false);
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			BadStory.SetActive(true);
		}
		BadStrigg.SetActive(true);
		Soundboxes.SetActive(true);
		ModelAnimatro.SetActive(false);
		Warning.SetActive(true);
		EndoAnimatronick.SetActive(true);
	}
}
