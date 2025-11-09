using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseEsc : MonoBehaviour
{
	private bool paused;

	public GameObject ListnerPlayer;

	public GameObject ListnerPause;

	public GameObject Setting;

	public GameObject panel;

	public GameObject QuitPanel;

	public AudioSource Sound;

	public CameraController CamController;

	public GameObject ShakeCam;

	public GameObject PlayerSamina;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!paused)
			{
				ShakeCam.SetActive(false);
				Time.timeScale = 0f;
				Sound.Play();
				paused = true;
				PlayerSamina.SetActive(false);
				ListnerPlayer.SetActive(false);
				ListnerPause.SetActive(true);
				panel.SetActive(true);
				CamController.enabled = false;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				Time.timeScale = 1f;
				Sound.Play();
				ShakeCam.SetActive(true);
				ListnerPlayer.SetActive(true);
				ListnerPause.SetActive(false);
				paused = false;
				panel.SetActive(false);
				CamController.enabled = true;
				QuitPanel.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				Setting.SetActive(false);
			}
		}
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		Sound.Play();
		ShakeCam.SetActive(true);
		ListnerPlayer.SetActive(true);
		ListnerPause.SetActive(false);
		paused = false;
		panel.SetActive(false);
		CamController.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void Quit()
	{
		Sound.Play();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		SceneManager.LoadScene("menu");
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void back()
	{
		Sound.Play();
	}

	public void settings()
	{
		Sound.Play();
	}
}
