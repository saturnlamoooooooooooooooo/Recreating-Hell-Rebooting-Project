using UnityEngine;
using UnityEngine.UI;

public class InteractionG : MonoBehaviour
{
	public Text indicator;

	private void Update()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, base.transform.forward, out hitInfo, 3f))
		{
			if (hitInfo.collider.tag == "Generator")
			{
				indicator.enabled = true;
				if (Input.GetKeyDown(KeyCode.E))
				{
					hitInfo.collider.GetComponent<ItemG>().ItemInteractionG();
				}
			}
			else
			{
				indicator.enabled = false;
			}
		}
		else
		{
			indicator.enabled = false;
		}
	}
}
