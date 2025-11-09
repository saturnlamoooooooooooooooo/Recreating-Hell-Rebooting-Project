using UnityEngine;

public class ItemG : MonoBehaviour
{
	public float scet;

	public float scetAnimatronicOff;

	public GameObject doors1;

	public GameObject doors2;

	public GameObject FuseBroken;

	public GameObject IndicatorRedOn;

	public GameObject IndicatorRedOff;

	public GameObject IndicatorGreenOn;

	public GameObject IndicatorGreenOff;

	public GameObject indicator1;

	public GameObject indicator2;

	public Animator Strelka1;

	public Animator Strelka2;

	public GameObject SoundOn;

	public GameObject SoundWork;

	public Animator StepOn;

	public GameObject StepOff;

	public AudioSource SoundUp;

	public GameObject Atmosf;

	public GameObject animatronik1;

	public GameObject animatronik;

	public bool trigg;

	public InteractionsG interactionsG;

	public ItemG genOff;

	public DoorTypeG doorType;

	public ItemTypeG itemType;

	private DataG data;

	[Header("Generator")]
	public bool isLock = true;

	private bool IsOpen;

	public Item dooritemoff;

	public GameObject doorIndoff;

	public GameObject doorSoundoff;

	public GameObject TriggerSub;

	private void Start()
	{
		doors1.SetActive(false);
		doors2.SetActive(true);
		trigg = true;
	}

	private void FixedUpdate()
	{
		data = GameObject.FindGameObjectWithTag("Player").GetComponent<DataG>();
		if (scet >= 5f)
		{
			genOff.enabled = false;
		}
		scetAnimatronicOff += 1f * Time.deltaTime;
		if ((scetAnimatronicOff >= 0.2f) & trigg)
		{
			trigg = false;
		}
		if (scetAnimatronicOff <= 0.5f && scetAnimatronicOff >= 0.4f)
		{
			animatronik.SetActive(false);
		}
		if (scet >= 1.5f)
		{
			animatronik1.SetActive(false);
		}
		if (scet >= 4f)
		{
			StepOff.SetActive(false);
		}
		if (!isLock)
		{
			scet += 1f * Time.deltaTime;
			StepOn.SetBool("On", true);
			SoundOn.SetActive(true);
			SoundWork.SetActive(true);
			Atmosf.SetActive(true);
			FuseBroken.SetActive(true);
			IndicatorRedOn.SetActive(false);
			IndicatorRedOff.SetActive(true);
			IndicatorGreenOn.SetActive(true);
			IndicatorGreenOff.SetActive(false);
			indicator1.SetActive(true);
			indicator2.SetActive(true);
			Strelka1.SetBool("On", true);
			Strelka2.SetBool("On", true);
			doors1.SetActive(true);
			doors2.SetActive(false);
			dooritemoff.enabled = true;
			doorIndoff.SetActive(true);
			doorSoundoff.SetActive(true);
		}
	}

	public void ItemInteractionG()
	{
		if (interactionsG == InteractionsG.Generator)
		{
			if (isLock)
			{
				if (doorType == DoorTypeG.Fuse)
				{
					if (data.fuse < 1 && data.fuse != 1)
					{
						return;
					}
					IsOpen = !IsOpen;
					TriggerSub.SetActive(true);
					data.fuse--;
					isLock = false;
				}
				if (doorType == DoorTypeG.Fuse2)
				{
					if (data.fuse2 < 1 && data.fuse2 != 1)
					{
						return;
					}
					IsOpen = !IsOpen;
					TriggerSub.SetActive(true);
					data.fuse2--;
					isLock = false;
				}
			}
			else
			{
				IsOpen = !IsOpen;
			}
		}
		if (interactionsG == InteractionsG.fuse1 && interactionsG == InteractionsG.fuse1)
		{
			if (itemType == ItemTypeG.Keyfuse)
			{
				SoundUp.Play();
				data.fuse++;
				Object.Destroy(base.gameObject);
			}
			if (itemType == ItemTypeG.Keyfuse2)
			{
				SoundUp.Play();
				data.fuse2++;
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void UpdateAnimationState(bool open)
	{
	}

	private void DestroyObject()
	{
		Object.Destroy(base.gameObject);
	}
}
