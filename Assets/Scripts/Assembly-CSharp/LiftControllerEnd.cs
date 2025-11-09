using System.Collections;
using UnityEngine;

public class LiftControllerEnd : MonoBehaviour
{
	public GameObject TextE;

	public GameObject AnimatronicOff;

	public GameObject Task;

	public GameObject Task2;

	public GameObject TriggerTaskOff;

	public GameObject Lift;

	public GameObject LightEnd;

	public GameObject SoundLift;

	public GameObject SoundButton;

	public Animator DoorLift;

	public GameObject SoundDoorLift;

	public GameObject ElectricDoorClose;

	public GameObject ElectricDoorOpen;

	public GameObject WayPoints;

	public GameObject WayPoints2;

	public GameObject WayPointsEnd;

	public GameObject AnimatronicEnd;

	public GameObject AmbianseOff;

	public GameObject StaminaInfinity;

	public SnakeCamera ShakeCamera;

	public GameObject ShakeCube;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				AnimatronicOff.SetActive(true);
				AmbianseOff.SetActive(false);
				TriggerTaskOff.SetActive(false);
				SoundButton.SetActive(true);
				StartCoroutine("door");
				Task.SetActive(false);
				LightEnd.SetActive(true);
				SoundLift.SetActive(true);
				ElectricDoorClose.SetActive(true);
				ElectricDoorOpen.SetActive(false);
				WayPoints.SetActive(false);
				WayPointsEnd.SetActive(true);
				WayPoints2.SetActive(false);
				StaminaInfinity.SetActive(true);
				ShakeCube.SetActive(true);
				ShakeCamera.enabled = true;
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

	private IEnumerator door()
	{
		yield return new WaitForSeconds(2.1f);
		Task2.SetActive(true);
		yield return new WaitForSeconds(4f);
		AnimatronicEnd.SetActive(true);
		AnimatronicOff.SetActive(false);
		Lift.SetActive(true);
		DoorLift.SetBool("On", true);
		TextE.SetActive(false);
		yield return new WaitForSeconds(44f);
		SoundDoorLift.SetActive(true);
	}

	private void Update()
	{
	}
}
