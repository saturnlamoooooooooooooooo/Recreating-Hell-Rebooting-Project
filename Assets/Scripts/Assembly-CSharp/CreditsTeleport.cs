using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTeleport : MonoBehaviour
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
		yield return new WaitForSeconds(11f);
		SceneManager.LoadScene("Credits");
		yield return new WaitForSeconds(3f);
	}
}
