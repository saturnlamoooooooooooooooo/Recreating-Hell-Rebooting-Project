using UnityEngine;

public class EndControolercutscene : MonoBehaviour
{
	public GameObject TextE;

	public GameObject Animatronic;

	public GameObject Player;

	public GameObject CutScene;

	public GameObject EndDoor;

	public GameObject Ambient1;

	public GameObject PauseScript;

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				Animatronic.SetActive(false);
				Player.SetActive(false);
				CutScene.SetActive(true);
				EndDoor.SetActive(false);
				Ambient1.SetActive(false);
				PauseScript.SetActive(false);
			}
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(false);
		}
	}
}
