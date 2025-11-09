using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReEnd : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine("Death");
	}

	private IEnumerator Death()
	{
		yield return new WaitForSeconds(5f);
		SceneManager.LoadScene("ReEnd");
	}
}
