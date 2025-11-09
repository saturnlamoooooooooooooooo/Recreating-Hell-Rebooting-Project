using System.Collections;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
	public GameObject PauseScript1;

	public GameObject CameraButton;

	public GameObject buttonCamera;

	public Animator anim;

	public GameObject Mask;

	public Camera SecurityCamera;

	public float timer = 2f;

	protected Camera[] cameras;

	private void Awake()
	{
		SecurityCamera.enabled = true;
		cameras[0] = SecurityCamera;
	}

	private IEnumerator Load()
	{
		yield return new WaitForSeconds(0f);
	}

	private IEnumerator Screen()
	{
		PauseScript1.SetActive(false);
		CameraButton.SetActive(false);
		SecurityCamera.enabled = false;
		anim.SetBool("IsOpen", true);
		yield return new WaitForSeconds(0f);
		StartCoroutine(Load());
	}

	private IEnumerator Close()
	{
		yield return new WaitForSeconds(0f);
		anim.SetBool("IsOpen", false);
		yield return new WaitForSeconds(0f);
		PauseScript1.SetActive(true);
		CameraButton.SetActive(true);
		SecurityCamera.enabled = true;
	}

	public void Camera()
	{
		if (SecurityCamera.enabled)
		{
			StartCoroutine(Screen());
		}
		else
		{
			StartCoroutine(Close());
		}
	}
}
