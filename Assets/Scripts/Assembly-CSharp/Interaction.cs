using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
	public Text indicator;

	private void Update()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, base.transform.forward, out hitInfo, 3f))
		{
			if (hitInfo.collider.tag == "Card")
			{
				indicator.enabled = true;
				if (Input.GetKeyDown(KeyCode.E))
				{
					hitInfo.collider.GetComponent<Item>().ItemInteraction();
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
