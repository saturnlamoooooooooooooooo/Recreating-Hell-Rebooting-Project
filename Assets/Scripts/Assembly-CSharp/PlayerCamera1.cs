using UnityEngine;

public class PlayerCamera1 : MonoBehaviour
{
	public float step;

	private float edgesize = 300f;

	private void Update()
	{
		if (Input.mousePosition.x > (float)Screen.width - edgesize && base.transform.rotation.eulerAngles.y < 1E+19f)
		{
			base.transform.Rotate(0f, step * Time.deltaTime, 0f);
		}
		if (Input.mousePosition.x < edgesize && base.transform.rotation.eulerAngles.y > -1f)
		{
			base.transform.Rotate(0f, (0f - step) * Time.deltaTime, 0f);
		}
	}
}
