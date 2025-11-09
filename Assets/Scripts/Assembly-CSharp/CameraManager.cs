using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[SerializeField]
	private GameController game;

	public GameObject Ramka4;

	public GameObject Ramka3;

	public GameObject Ramka2;

	public GameObject Ramka;

	public GameObject RecImg;

	public GameObject RecText;

	public GameObject MaskButton;

	public GameObject PauseScript1;

	public GameObject MeinCam1;

	public GameObject AudioListener2;

	public GameObject time;

	public GameObject ChargeButton;

	public GameObject buttonCamera;

	public Animator anim;

	public GameObject screen;

	public GameObject image;

	public float timer = 2f;

	public Camera SecurityCamera;

	public Camera Camera1A;

	public Camera Camera1B;

	public Camera Camera2A;

	public Camera Camera2B;

	public Camera Camera2C;

	public Camera Camera3B;

	public Camera Camera1C;

	protected Camera[] cameras;

	protected int currCamera = 1;

	private void Awake()
	{
		RecText.SetActive(false);
		AudioListener2.SetActive(false);
		RecText.SetActive(false);
		RecImg.SetActive(false);
		Ramka.SetActive(false);
		Ramka2.SetActive(false);
		Ramka3.SetActive(false);
		Ramka4.SetActive(false);
		anim = screen.GetComponent<Animator>();
		time.SetActive(false);
		image.SetActive(false);
		SecurityCamera.enabled = true;
		Camera1A.enabled = false;
		Camera2A.enabled = false;
		Camera1B.enabled = false;
		Camera2B.enabled = false;
		Camera2C.enabled = false;
		Camera3B.enabled = false;
		Camera1C.enabled = false;
		cameras = new Camera[8];
		cameras[0] = SecurityCamera;
		cameras[1] = Camera1A;
		cameras[2] = Camera1B;
		cameras[3] = Camera2A;
		cameras[4] = Camera2B;
		cameras[5] = Camera3B;
		cameras[6] = Camera1C;
		cameras[7] = Camera2C;
	}

	private IEnumerator Load()
	{
		image.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		image.SetActive(true);
	}

	private IEnumerator Screen()
	{
		PauseScript1.SetActive(false);
		MaskButton.SetActive(false);
		anim.SetBool("IsOpen", true);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(Load());
		cameras[currCamera].enabled = true;
		time.SetActive(true);
		cameras[0].enabled = false;
		MeinCam1.SetActive(false);
		AudioListener2.SetActive(true);
		RecText.SetActive(true);
		RecImg.SetActive(true);
		Ramka.SetActive(true);
		Ramka2.SetActive(true);
		Ramka3.SetActive(true);
		Ramka4.SetActive(true);
	}

	private IEnumerator Close()
	{
		MeinCam1.SetActive(true);
		AudioListener2.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		MaskButton.SetActive(true);
		anim.SetBool("IsOpen", false);
		image.SetActive(false);
		cameras[currCamera].enabled = false;
		time.SetActive(false);
		RecText.SetActive(false);
		RecImg.SetActive(false);
		Ramka.SetActive(false);
		Ramka2.SetActive(false);
		Ramka3.SetActive(false);
		Ramka4.SetActive(false);
		cameras[0].enabled = true;
		anim.SetBool("IsOpen", false);
		yield return new WaitForSeconds(0.2f);
		PauseScript1.SetActive(true);
	}

	public void Update()
	{
		if (cameras[6].enabled)
		{
			ChargeButton.SetActive(true);
		}
		else
		{
			ChargeButton.SetActive(false);
		}
	}

	public void Camera()
	{
		if (cameras[0].enabled)
		{
			StartCoroutine(Screen());
		}
		else
		{
			StartCoroutine(Close());
		}
	}

	public void Camera_1A()
	{
		cameras[currCamera].enabled = false;
		currCamera = 1;
		cameras[currCamera].enabled = true;
	}

	public void Camera_1B()
	{
		cameras[currCamera].enabled = false;
		currCamera = 2;
		cameras[currCamera].enabled = true;
	}

	public void Camera_2A()
	{
		cameras[currCamera].enabled = false;
		currCamera = 3;
		cameras[currCamera].enabled = true;
	}

	public void Camera_2B()
	{
		cameras[currCamera].enabled = false;
		currCamera = 4;
		cameras[currCamera].enabled = true;
	}

	public void Camera_3B()
	{
		cameras[currCamera].enabled = false;
		currCamera = 5;
		cameras[currCamera].enabled = true;
	}

	public void Camera_1C()
	{
		cameras[currCamera].enabled = false;
		currCamera = 6;
		cameras[currCamera].enabled = true;
	}

	public void Camera_2C()
	{
		cameras[currCamera].enabled = false;
		currCamera = 7;
		cameras[currCamera].enabled = true;
	}
}
