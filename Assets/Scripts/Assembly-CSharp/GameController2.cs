using UnityEngine;

public class GameController2 : MonoBehaviour
{
	public GameObject fadedr;

	public GameObject trigg;

	public float timeUI;

	private void Awake()
	{
		StartGame();
		fadedr.SetActive(false);
	}

	public void StartGame()
	{
		timeUI = 0f;
	}

	private void Update()
	{
		timeUI += Time.deltaTime * 9f;
		if (Mathf.Floor(timeUI / 1f) == 850f)
		{
			trigg.SetActive(true);
			fadedr.SetActive(true);
		}
	}
}
