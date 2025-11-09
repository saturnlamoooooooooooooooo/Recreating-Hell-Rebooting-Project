using UnityEngine;

public class FonarikScript2 : MonoBehaviour
{
	public AudioSource FonarikSound2;

	public AudioSource FonarikSound;

	public int Energy;

	public GameObject Light;

	public bool onLight;

	private float scet;

	private void Update()
	{
		if (onLight)
		{
			scet += 1f * Time.deltaTime;
			if (scet >= 100f)
			{
				Energy--;
				scet = 0f;
			}
		}
		if (Energy >= 100)
		{
			Energy = 100;
		}
		if (Energy <= 0)
		{
			onLight = false;
			Light.SetActive(false);
			Energy = 0;
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (!onLight && Energy > 0)
			{
				Light.SetActive(true);
				onLight = true;
				FonarikSound.Play();
			}
			else
			{
				Light.SetActive(false);
				onLight = false;
			}
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			if (onLight && Energy > 0)
			{
				Light.SetActive(false);
				onLight = false;
				FonarikSound2.Play();
			}
			else
			{
				Light.SetActive(true);
				onLight = true;
			}
		}
	}
}
