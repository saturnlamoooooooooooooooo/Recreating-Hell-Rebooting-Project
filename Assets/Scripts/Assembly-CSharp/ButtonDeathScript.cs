using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonDeathScript : MonoBehaviour
{
	public GameObject Image1;

	public GameObject Image2;

	public Animator animat;

	public Animator animat2;

	public Animator animatbox;

	public GameObject box;

	public void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Try()
	{
		StartCoroutine("tryAgain");
		Image2.SetActive(true);
		animat.SetBool("open", true);
		animat2.SetBool("open", true);
		animatbox.SetBool("open", true);
	}

	private IEnumerator tryAgain()
	{
		yield return new WaitForSeconds(0.3f);
		Image1.SetActive(false);
		box.SetActive(true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Game2");
	}

	public void Quitbutton()
	{
		StartCoroutine("Quit");
		Image2.SetActive(true);
		animat.SetBool("open", true);
		animat2.SetBool("open", true);
		animatbox.SetBool("open", true);
	}

	private IEnumerator Quit()
	{
		yield return new WaitForSeconds(0.3f);
		Image1.SetActive(false);
		box.SetActive(true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("menu");
	}

	public void TryHARD()
	{
		StartCoroutine("tryAgainHARD");
		Image2.SetActive(true);
		animat.SetBool("open", true);
		animat2.SetBool("open", true);
		animatbox.SetBool("open", true);
	}

	private IEnumerator tryAgainHARD()
	{
		yield return new WaitForSeconds(0.3f);
		Image1.SetActive(false);
		box.SetActive(true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Game2HARD");
	}
}
