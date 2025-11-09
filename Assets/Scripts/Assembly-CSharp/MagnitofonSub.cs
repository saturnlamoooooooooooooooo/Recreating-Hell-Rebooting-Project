using UnityEngine;

public class MagnitofonSub : MonoBehaviour
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
		if ((double)scet >= 11.8)
		{
			Text1.SetActive(false);
			Text2.SetActive(true);
		}
		if (scet >= 24f)
		{
			Text2.SetActive(false);
			Text3.SetActive(true);
		}
		if (scet >= 34f)
		{
			Text3.SetActive(false);
		}
		if (scet >= 37f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
