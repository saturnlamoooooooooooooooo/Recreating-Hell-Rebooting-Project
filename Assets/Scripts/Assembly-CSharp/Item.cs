using UnityEngine;

public class Item : MonoBehaviour
{
	public GameObject Trigger;

	public float scet;

	public AudioSource Scrip;

	public AudioSource Locked;

	public AudioSource Unlocked;

	public Interactions interactions;

	public Animator animator;

	public DoorType doorType;

	public ItemType itemType;

	private Data2 data;

	public GameObject triggerActive;

	public GameObject triggerSubDoor;

	[Header("Door")]
	public bool isLock = true;

	private bool IsOpen;

	private void Start()
	{
		Trigger.SetActive(false);
	}

	private void FixedUpdate()
	{
		scet -= 1f;
		if (scet <= 0f)
		{
			scet = 0f;
		}
		data = GameObject.FindGameObjectWithTag("Player").GetComponent<Data2>();
	}

	public void ItemInteraction()
	{
		if (interactions == Interactions.Door)
		{
			if (isLock && scet == 0f)
			{
				Locked.Play();
				if (doorType == DoorType.DoorRedCard)
				{
					if (data.RedCard < 1 && data.RedCard != 1)
					{
						return;
					}
					IsOpen = !IsOpen;
					data.RedCard--;
					isLock = false;
					Locked.Stop();
					Unlocked.Play();
					scet = 1f;
				}
				if (doorType == DoorType.DoorBlueCard)
				{
					if (data.BlueCard < 1 && data.BlueCard != 1)
					{
						return;
					}
					IsOpen = !IsOpen;
					data.BlueCard--;
					isLock = false;
					Locked.Stop();
					Unlocked.Play();
					scet = 1f;
				}
			}
			if (!isLock && scet == 0f)
			{
				animator.SetBool("IsOpen", true);
				animator.SetBool("IsOpen", true);
				Scrip.Play();
			}
			else
			{
				IsOpen = !IsOpen;
			}
		}
		if (interactions == Interactions.Card)
		{
			Locked.Play();
			if (itemType == ItemType.keyCardRed)
			{
				data.RedCard++;
				Object.Destroy(base.gameObject);
			}
			if (itemType == ItemType.keyCardBlue)
			{
				data.BlueCard++;
				Trigger.SetActive(true);
				triggerActive.SetActive(true);
				triggerSubDoor.SetActive(false);
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void DestroyObject()
	{
		Object.Destroy(base.gameObject);
	}
}
