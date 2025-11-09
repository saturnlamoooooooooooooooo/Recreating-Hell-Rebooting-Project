using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private GameController game;

	[SerializeField]
	private Text percent;

	[SerializeField]
	private Text Gentext;

	[SerializeField]
	private Text time;

	public GameObject Animatronic1;

	public GameObject Animatronic2;

	public GameObject Animatronic3;

	public GameObject Animatronic4;

	public GameObject Zvuki1;

	public GameObject Zvuki2;

	public GameObject Zvuki3;

	public GameObject Zvuki4;

	public GameObject Zvuki5;

	public GameObject Zvuki6;

	public GameObject Zvuki7;

	public GameObject Playertr1;

	public GameObject Playertr2;

	public GameObject Playertr3;

	public GameObject Camera1;

	public GameObject Camera2;

	public GameObject Camera3;

	public GameObject Camera4;

	public GameObject Camera5;

	public GameObject Camera6;

	public GameObject Camera7;

	public GameObject Camera8;

	public GameObject Canvasik;

	public GameObject Camerafinal2;

	public GameObject CubeFinal;

	public GameObject FadedrFinal;

	public GameObject EndoOn;

	public GameObject Signal1;

	public int deGenbuff;

	public float timeUI;

	public GameObject Znak1;

	public GameObject Znak2;

	public GameObject Znak3;

	public int Genbuff;

	private float Gen;

	private float Energy;

	public int buff;

	public int debuff;

	private bool Genup;

	public int de2buff;

	private void Awake()
	{
		StartGame();
	}

	public void StartGame()
	{
		deGenbuff = 4400;
		Genbuff = 24;
		de2buff = 15;
		debuff = 13;
		buff = 7;
		Energy = 10000f;
		Gen = 1000f;
		Znak1.SetActive(false);
		Znak2.SetActive(false);
		Znak3.SetActive(false);
		timeUI = 0f;
	}

	public void GenerDown()
	{
		if (Gen < 1000f)
		{
			Gen += Time.deltaTime * (float)deGenbuff;
		}
	}

	private void FixedUpdate()
	{
		if (Mathf.Floor(timeUI / 1f) == 1700f)
		{
			deGenbuff = 4550;
			Genbuff = 29;
		}
		if (Mathf.Floor(timeUI / 1f) == 3100f)
		{
			deGenbuff = 4720;
			Genbuff = 32;
		}
		timeUI += Time.deltaTime * 9f;
		if (Mathf.Floor(timeUI / 1f) == 5067f)
		{
			Animatronic1.SetActive(false);
			Animatronic2.SetActive(false);
			Animatronic3.SetActive(false);
			Animatronic4.SetActive(false);
			Playertr1.SetActive(false);
			Playertr2.SetActive(false);
			Playertr3.SetActive(false);
			Zvuki1.SetActive(false);
			Zvuki2.SetActive(false);
			Zvuki3.SetActive(false);
			Zvuki4.SetActive(false);
			Zvuki5.SetActive(false);
			Zvuki6.SetActive(false);
			Zvuki7.SetActive(false);
			Camera1.SetActive(false);
			Camera2.SetActive(false);
			Camera3.SetActive(false);
			Camera4.SetActive(false);
			Camera5.SetActive(false);
			Camera6.SetActive(false);
			Camera7.SetActive(false);
			Camera8.SetActive(false);
			Canvasik.SetActive(false);
			Camerafinal2.SetActive(true);
			CubeFinal.SetActive(true);
			FadedrFinal.SetActive(true);
			Gen = 1000f;
			Energy = 10000f;
		}
		time.text = Mathf.Floor(timeUI / 1000f) + " AM";
		percent.text = Mathf.Round(Energy / 100f) + " % Energy";
		Gentext.text = Mathf.Round(Gen / 10f) + " %";
		if (Mathf.Floor(timeUI / 1f) == 2500f)
		{
			EndoOn.SetActive(true);
		}
		if (Energy > 0f)
		{
			Energy -= Time.deltaTime * (float)buff;
		}
		else
		{
			SceneManager.LoadScene(6);
		}
		if ((Genup = true) && Gen > 1000f)
		{
			Gen = 1000f;
		}
		if (Gen > 0f)
		{
			Gen -= Time.deltaTime * (float)Genbuff;
		}
		else
		{
			SceneManager.LoadScene(14);
		}
		if (Gen < 200f)
		{
			Znak1.SetActive(true);
			Signal1.SetActive(true);
		}
		else
		{
			Signal1.SetActive(false);
			Znak1.SetActive(false);
		}
		if (Gen < 100f)
		{
			Znak2.SetActive(true);
			Znak3.SetActive(true);
			Znak1.SetActive(false);
		}
		else
		{
			Znak2.SetActive(false);
			Znak3.SetActive(false);
		}
	}
}
