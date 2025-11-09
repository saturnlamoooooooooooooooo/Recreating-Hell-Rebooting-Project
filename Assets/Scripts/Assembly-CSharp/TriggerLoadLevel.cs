using UnityEngine;

public class TriggerLoadLevel : MonoBehaviour
{
	public int Level;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			Application.LoadLevel(Level);
		}
	}
}
