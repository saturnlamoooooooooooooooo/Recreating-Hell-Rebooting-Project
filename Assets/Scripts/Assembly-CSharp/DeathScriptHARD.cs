using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScriptHARD : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine("Death");
	}

	private IEnumerator Death()
	{
		yield return new WaitForSeconds(4.5f);
		SceneManager.LoadScene("DeathScreenHARD");
	}
}
