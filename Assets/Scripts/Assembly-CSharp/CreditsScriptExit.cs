using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScriptExit : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine("door");
	}

	private void Update()
	{
	}

	private IEnumerator door()
	{
		yield return new WaitForSeconds(80f);
		SceneManager.LoadScene("menu");
		yield return new WaitForSeconds(3f);
	}
}
