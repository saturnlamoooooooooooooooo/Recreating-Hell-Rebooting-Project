using UnityEngine;

public class Data2 : MonoBehaviour
{
	public GameObject blueCard;

	public GameObject redCard;

	public int RedCard;

	public int BlueCard;

	private void Awake()
	{
	}

	private void Update()
	{
		if (BlueCard > 0)
		{
			blueCard.SetActive(true);
		}
		else
		{
			blueCard.SetActive(false);
		}
		if (RedCard > 0)
		{
			redCard.SetActive(true);
		}
		else
		{
			redCard.SetActive(false);
		}
	}
}
