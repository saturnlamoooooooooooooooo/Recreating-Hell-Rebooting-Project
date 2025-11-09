using UnityEngine;

public class HideOpenScript : MonoBehaviour
{
	public HideScript Hide;

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "HideOpenTrigger")
		{
			Hide.AnimatorHide.SetBool("IsOpen", true);
			Hide.IntOpen = 1f;
			Hide.OpenSound.SetActive(true);
			Hide.CloseSound.SetActive(false);
			Hide.scet = 1f;
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "HideOpenTrigger")
		{
			Hide.AnimatorHide.SetBool("IsOpen", false);
			Hide.IntOpen = 0f;
			Hide.OpenSound.SetActive(false);
			Hide.CloseSound.SetActive(true);
			Hide.scet = 1f;
		}
	}
}
