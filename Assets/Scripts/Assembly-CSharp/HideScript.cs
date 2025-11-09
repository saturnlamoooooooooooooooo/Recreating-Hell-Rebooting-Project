using UnityEngine;

public class HideScript : MonoBehaviour
{
	public GameObject Ebutton;

	public GameObject OpenSound;

	public GameObject CloseSound;

	public float IntOpen;

	public Animator AnimatorHide;

	public GameObject Ldoor;

	public GameObject Rdoor;

	public float scet;

	private void Awake()
	{
		Ldoor.SetActive(true);
		Rdoor.SetActive(true);
		scet = 0f;
		IntOpen = 0f;
		Ebutton.SetActive(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
		scet -= 1f * Time.deltaTime;
		if (scet <= 0f)
		{
			scet = 0f;
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (scet == 0f && col.tag == "HidePlayerTrigger")
		{
			if (IntOpen == 0f)
			{
				AnimatorHide.SetBool("IsOpen", true);
				IntOpen = 1f;
				OpenSound.SetActive(true);
				CloseSound.SetActive(false);
				scet = 1f;
			}
			else
			{
				AnimatorHide.SetBool("IsOpen", false);
				OpenSound.SetActive(false);
				CloseSound.SetActive(true);
				scet = 1f;
				IntOpen = 0f;
			}
		}
		if (col.tag == "MainCamera")
		{
			Ebutton.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "MainCamera")
		{
			Ebutton.SetActive(false);
		}
	}
}
