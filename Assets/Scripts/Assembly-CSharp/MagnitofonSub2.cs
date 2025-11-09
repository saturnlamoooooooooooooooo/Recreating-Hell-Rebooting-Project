using UnityEngine;

public class MagnitofonSub2 : MonoBehaviour
{
	public float scet;

	public GameObject Text1;

	public GameObject Text2;

	public GameObject Text3;

	public GameObject Text4;

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
		if ((double)scet >= 14.5)
		{
			Text1.SetActive(false);
			Text2.SetActive(true);
		}
		if (scet >= 29f)
		{
			Text2.SetActive(false);
			Text3.SetActive(true);
		}
		if (scet >= 41f)
		{
			Text3.SetActive(false);
			Text4.SetActive(true);
		}
		if (scet >= 48f)
		{
			Text4.SetActive(true);
		}
		if ((double)scet >= 51.5)
		{
			Text4.SetActive(false);
			Object.Destroy(base.gameObject);
		}
	}
}
