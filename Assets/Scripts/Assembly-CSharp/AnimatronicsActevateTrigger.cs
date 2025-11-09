using System.Collections;
using UnityEngine;

public class AnimatronicsActevateTrigger : MonoBehaviour
{
	public GameObject triggerStop;

	public GameObject SoundActivate;

	public GameObject AnimatronicOff1;

	public GameObject AnimatronicOff2;

	public GameObject AnimatronicOn;

	public GameObject GameplayTrigger2;

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			AnimatronicOff1.SetActive(false);
			AnimatronicOff2.SetActive(false);
			SoundActivate.SetActive(true);
			triggerStop.SetActive(true);
			StartCoroutine("door");
			GameplayTrigger2.SetActive(true);
		}
	}

	private void Start()
	{
	}

	private IEnumerator door()
	{
		yield return new WaitForSeconds(8.1f);
		AnimatronicOn.SetActive(true);
	}

	private void Update()
	{
	}
}
