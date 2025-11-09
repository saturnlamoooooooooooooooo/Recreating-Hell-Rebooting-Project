using UnityEngine;

public class DoorActivateAwake : MonoBehaviour
{
	public GameObject Door;

	private void Awake()
	{
		Door.SetActive(true);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
