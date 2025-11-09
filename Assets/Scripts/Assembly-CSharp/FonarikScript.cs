using UnityEngine;
using UnityEngine.UI;

public class FonarikScript : MonoBehaviour
{
	public AudioSource FonarikSound2;

	public AudioSource FonarikSound;

	public GameObject FonarikSoundbox;

	public GameObject FonarikSoundbox2;

	public Text txt;

	public int Energy;

	public GameObject Light;

	public bool onLight;

	private float scet;

	public Animator AnimatorF;

	public Animator AnimatorOpenClose;

	public bool onLight2;

	public float psich;

	public float scetPsich;

	public Animator CameraPsich;

	public GameObject SoundPsichS4;

	public GameObject SoundPsichS3;

	public GameObject SoundPsichS2;

	public GameObject PlayerStaminaOn;

	public GameObject SoundPsichS5;

	public GameObject SoundPsichDeath;

	public GameObject triggerdeath;

	public Animator AggreTrigger;

	public Animator BatteryInd;

	public float scetAggre;

	public GameObject Phantom1;

	public GameObject Phantom2;

	public GameObject Phantom3;

	private void Awake()
	{
		scetAggre = 0f;
		scetPsich = 0f;
		onLight2 = false;
	}

	private void Update()
	{
		if (scetAggre <= 0f)
		{
			AggreTrigger.SetBool("IsOpen", false);
		}
		if (scetAggre > 0f)
		{
			AggreTrigger.SetBool("IsOpen", true);
		}
		if (scetAggre <= 0f)
		{
			scetAggre = 0f;
		}
		scetAggre -= 4f * Time.deltaTime;
		psich -= 0.4f * Time.deltaTime;
		if (!onLight && !onLight2)
		{
			psich -= 1.7f * Time.deltaTime;
		}
		if (psich <= 1100f)
		{
			CameraPsich.SetBool("Stage1", true);
		}
		if (psich <= 800f)
		{
			Phantom1.SetActive(true);
			SoundPsichS2.SetActive(true);
			CameraPsich.SetBool("Stage2", true);
		}
		if (psich <= 500f)
		{
			Phantom2.SetActive(true);
			CameraPsich.SetBool("Stage3", true);
			SoundPsichS3.SetActive(true);
		}
		if (psich <= 250f)
		{
			Phantom3.SetActive(true);
			CameraPsich.SetBool("Stage4", true);
			SoundPsichS4.SetActive(true);
			PlayerStaminaOn.SetActive(true);
		}
		if (psich <= 70f)
		{
			SoundPsichS5.SetActive(true);
			CameraPsich.SetBool("Stage5", true);
		}
		if (psich <= 0f)
		{
			scetPsich += 1f * Time.deltaTime;
			SoundPsichDeath.SetActive(true);
			CameraPsich.SetBool("StageDeath", true);
		}
		if ((double)scetPsich >= 0.7)
		{
			triggerdeath.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			scetAggre = 1f;
			if (!onLight2)
			{
				onLight2 = true;
			}
			else if (onLight2)
			{
				onLight2 = false;
			}
		}
		txt.text = Energy + "%";
		if (onLight)
		{
			scet += 1f * Time.deltaTime;
			if (scet >= 4f)
			{
				Energy--;
				scet = 0f;
			}
		}
		if (Energy >= 100)
		{
			Energy = 100;
		}
		if (Energy >= 75)
		{
			BatteryInd.SetBool("75", true);
		}
		if (Energy <= 75)
		{
			BatteryInd.SetBool("75", false);
		}
		if (Energy >= 50)
		{
			BatteryInd.SetBool("50", true);
		}
		if (Energy <= 50)
		{
			BatteryInd.SetBool("50", false);
		}
		if (Energy >= 30)
		{
			BatteryInd.SetBool("30", true);
		}
		if (Energy <= 30)
		{
			BatteryInd.SetBool("30", false);
		}
		if (Energy >= 10)
		{
			BatteryInd.SetBool("10", true);
		}
		if (Energy <= 10)
		{
			BatteryInd.SetBool("10", false);
		}
		if ((double)Energy <= 0.1)
		{
			BatteryInd.SetBool("0", true);
		}
		if ((double)Energy >= 0.1)
		{
			BatteryInd.SetBool("0", false);
		}
		if (Energy <= 0)
		{
			FonarikSoundbox2.SetActive(false);
			FonarikSoundbox.SetActive(false);
			onLight = false;
			onLight2 = false;
			AnimatorOpenClose.SetBool("open", false);
			AnimatorF.SetBool("IsOpen", false);
			Energy = 0;
		}
		else
		{
			FonarikSoundbox.SetActive(true);
			FonarikSoundbox2.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			scetAggre = 1f;
			if (!onLight && Energy > 0)
			{
				AnimatorOpenClose.SetBool("open", true);
				onLight = true;
				AnimatorF.SetBool("IsOpen", true);
				FonarikSound.Play();
				BatteryInd.SetBool("off", false);
			}
			else
			{
				AnimatorOpenClose.SetBool("open", false);
				AnimatorF.SetBool("IsOpen", false);
				onLight = false;
				FonarikSound2.Play();
				BatteryInd.SetBool("off", true);
			}
		}
	}
}
