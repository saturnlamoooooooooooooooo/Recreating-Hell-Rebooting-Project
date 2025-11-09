using UnityEngine;

public class BloodScript : MonoBehaviour
{
	public GameObject PlayerPers;

	public GameObject Blood;

	private void Start()
	{
	}

	private void Update()
	{
		if (PlayerPers.layer == 18)
		{
			Blood.SetActive(true);
		}
	}
}
