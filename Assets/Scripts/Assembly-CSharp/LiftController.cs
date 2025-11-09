using UnityEngine;

public class LiftController : MonoBehaviour
{
	public float scet;

	public GameObject LiftModel;

	public GameObject PauseScript;

	public GameObject CameraLift2;

	public Animator Lift;

	public Animator Camera;

	public GameObject CameraLift;

	public Camera CamPlayer;

	public GameObject AudioListnerPlayer;

	public CameraController CamConPlayer;

	public GameObject Canvas;

	public GameObject Fadedrq;

	public GameObject ScriptObj;

	public LiftController Script1;

	public GameObject soundsub;

	public GameObject soundsub2;

	public GameObject Sub1;

	public GameObject Sub2;

	public GameObject Sub3;

	public GameObject SubStart;

	public GameObject FTutor;

	public GameObject Siren;

	public GameObject Wasd;

	public GameObject ColliderEnterLocation;

	private void Start()
	{
		PauseScript.SetActive(false);
		Canvas.SetActive(false);
		AudioListnerPlayer.SetActive(false);
	}

	private void Awake()
	{
		scet = 0f;
	}

	private void Update()
	{
		scet += 1f * Time.deltaTime;
		if ((double)scet <= 0.5)
		{
			Fadedrq.SetActive(false);
			LiftModel.SetActive(true);
			CameraLift.SetActive(true);
			AudioListnerPlayer.SetActive(false);
			CamPlayer.enabled = false;
			CamConPlayer.enabled = false;
		}
		if ((double)scet >= 4.5)
		{
			soundsub.SetActive(true);
		}
		if (scet >= 5f)
		{
			Sub1.SetActive(true);
		}
		if ((double)scet >= 8.5)
		{
			Sub1.SetActive(false);
		}
		if (scet >= 9f)
		{
			Sub2.SetActive(true);
		}
		if (scet >= 11f)
		{
			Siren.SetActive(true);
		}
		if (scet >= 13f)
		{
			Sub2.SetActive(false);
		}
		if ((double)scet >= 18.3)
		{
			soundsub2.SetActive(true);
		}
		if ((double)scet >= 18.4)
		{
			Sub3.SetActive(true);
		}
		if ((double)scet >= 19.2)
		{
			Sub3.SetActive(false);
		}
		if (scet >= 18f)
		{
			Lift.SetBool("st1", true);
			Camera.SetBool("st1", true);
		}
		if (scet >= 20f)
		{
			Lift.SetBool("st2", true);
			Camera.SetBool("st2", true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if ((double)scet >= 25.5)
		{
			CameraLift.SetActive(false);
			CameraLift2.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if (scet >= 37f)
		{
			ColliderEnterLocation.SetActive(false);
			LiftModel.SetActive(false);
			CamPlayer.enabled = true;
			AudioListnerPlayer.SetActive(true);
			CamConPlayer.enabled = true;
			Fadedrq.SetActive(true);
			Canvas.SetActive(true);
			CameraLift.SetActive(false);
			CameraLift2.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Wasd.SetActive(true);
		}
		if (scet >= 40f)
		{
			ScriptObj.SetActive(false);
			Script1.enabled = false;
			PauseScript.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			SubStart.SetActive(true);
		}
	}
}
