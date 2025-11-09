using UnityEngine;

public class MagnitofonSub3 : MonoBehaviour
{
	public float scet;

	public GameObject Text1;

	public GameObject Text2;

	public GameObject Text3;

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
		if ((double)scet >= 2.8)
		{
			Text1.SetActive(true);
		}
		if ((double)scet >= 16.5)
		{
			Text1.SetActive(false);
			Text2.SetActive(true);
		}
		if (scet >= 32f)
		{
			Text2.SetActive(false);
			Text3.SetActive(true);
		}
		if (scet >= 48f)
		{
			Text3.SetActive(false);
		}
		if (scet >= 53f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
