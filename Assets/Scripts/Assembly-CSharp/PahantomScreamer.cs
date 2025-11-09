using System.Collections;
using UnityEngine;

public class PahantomScreamer : MonoBehaviour
{
	public GameObject Screamer;

	public GameObject ScreamerSound;

	public GameObject EnemyModel;

	public GameObject PhantomSound;

	public GameObject TriggerDeath;

	public AI_Monster3 script;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Screamer.SetActive(true);
			EnemyModel.SetActive(false);
			StartCoroutine("door");
		}
	}

	private IEnumerator door()
	{
		yield return new WaitForSeconds(0.2f);
		ScreamerSound.SetActive(true);
		EnemyModel.SetActive(false);
		yield return new WaitForSeconds(1f);
		PhantomSound.SetActive(false);
		script.enabled = false;
		TriggerDeath.SetActive(false);
	}
}
