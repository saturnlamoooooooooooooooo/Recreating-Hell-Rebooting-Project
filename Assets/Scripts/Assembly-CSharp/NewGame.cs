using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
	public Animator animat;

	public GameObject fadedr;

	public GameObject fadedr2;

	public GameObject Menu;

	public GameObject MenuCredits;

	public GameObject Menu2;

	public GameObject loading;

	public GameObject Settin;

	public GameObject easyhard;

	public GameObject Multip;

	public Connection Connectscript;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		loading.SetActive(false);
	}

	public void Normal()
	{
		animat.SetBool("load", true);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Norm");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private IEnumerator Norm()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(false);
		MenuCredits.SetActive(false);
		Menu2.SetActive(false);
		easyhard.SetActive(false);
		loading.SetActive(true);
		yield return new WaitForSeconds(2.8f);
		fadedr2.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		SceneManager.LoadScene("Game");
	}

	public void Hardmode()
	{
		animat.SetBool("load", true);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Hard");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private IEnumerator Hard()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(false);
		MenuCredits.SetActive(false);
		Menu2.SetActive(false);
		easyhard.SetActive(false);
		loading.SetActive(true);
		yield return new WaitForSeconds(2.8f);
		fadedr2.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		SceneManager.LoadScene("GameHARD");
	}

	public void SinglePlayer()
	{
		animat.SetBool("move", true);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Single");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private IEnumerator Single()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(false);
		MenuCredits.SetActive(false);
		Menu2.SetActive(false);
		easyhard.SetActive(true);
		yield return new WaitForSeconds(2.8f);
		fadedr2.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Quit()
	{
		animat.SetBool("move", true);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Quit1");
	}

	private IEnumerator Quit1()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(false);
		MenuCredits.SetActive(false);
		Menu2.SetActive(false);
		loading.SetActive(true);
		Application.Quit();
	}

	public void Settings()
	{
		animat.SetBool("move", true);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Settings1");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private IEnumerator Settings1()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(false);
		MenuCredits.SetActive(false);
		Menu2.SetActive(false);
		Settin.SetActive(true);
		yield return new WaitForSeconds(2.8f);
		fadedr2.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Back()
	{
		animat.SetBool("move", false);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Back1");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Connectscript.disConnect();
	}

	private IEnumerator Back1()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(true);
		MenuCredits.SetActive(true);
		Menu2.SetActive(true);
		Settin.SetActive(false);
		Multip.SetActive(false);
		yield return new WaitForSeconds(2.8f);
		fadedr2.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void BBack()
	{
		animat.SetBool("move", false);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("BBack1");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Connectscript.disConnect();
	}

	private IEnumerator BBack1()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(true);
		MenuCredits.SetActive(true);
		Menu2.SetActive(true);
		easyhard.SetActive(false);
		Multip.SetActive(false);
		yield return new WaitForSeconds(2.8f);
		fadedr2.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Credits()
	{
		animat.SetBool("move", true);
		fadedr.SetActive(false);
		fadedr2.SetActive(true);
		StartCoroutine("Credits1");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private IEnumerator Credits1()
	{
		yield return new WaitForSeconds(2.5f);
		Menu.SetActive(false);
		MenuCredits.SetActive(false);
		Menu2.SetActive(false);
		loading.SetActive(true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Credits");
	}
}
