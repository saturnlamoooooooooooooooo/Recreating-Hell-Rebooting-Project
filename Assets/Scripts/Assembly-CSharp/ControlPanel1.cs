using System.Collections;
using UnityEngine;

public class ControlPanel1 : MonoBehaviour
{
	public GameObject TextE;

	public GameObject Animatronic1;

	public GameObject Animatronic2;

	public GameObject AnimatronicOff1;

	public GameObject AnimatronicOff2;

	public GameObject sound;

	public GameObject Task1;

	public GameObject Task2;

	public ControlPanel1 script;

	public GameObject Ambient;

	public GameObject Ambient2;

	public GameObject Spawner;

	public SnakeCamera ShakeCamera;

	public GameObject ShakeCube;

	private void Start()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				sound.SetActive(true);
				Animatronic1.SetActive(false);
				Animatronic2.SetActive(false);
				AnimatronicOff1.SetActive(true);
				AnimatronicOff2.SetActive(true);
				Task1.SetActive(false);
				StartCoroutine("task");
				Ambient.SetActive(false);
				Ambient2.SetActive(true);
				ShakeCamera.enabled = false;
				ShakeCube.SetActive(false);
			}
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Control")
		{
			TextE.SetActive(false);
		}
	}

	private IEnumerator task()
	{
		yield return new WaitForSeconds(5f);
		Task2.SetActive(true);
		Spawner.SetActive(true);
		yield return new WaitForSeconds(6f);
		script.enabled = false;
	}

	private void Update()
	{
	}
}
