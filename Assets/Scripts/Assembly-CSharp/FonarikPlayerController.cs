using UnityEngine;

public class FonarikPlayerController : MonoBehaviour
{
	public Animator AnimatorF;

	public bool onLight;

	private void Awake()
	{
		AnimatorF.SetBool("IsOpen", true);
		onLight = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (!onLight)
			{
				AnimatorF.SetBool("IsOpen", false);
				onLight = true;
			}
			else if (onLight)
			{
				AnimatorF.SetBool("IsOpen", true);
				onLight = false;
			}
		}
	}
}
