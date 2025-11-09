using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ScriptStaminaInfinity : MonoBehaviour
{
	public FirstPersonController fpsController;

	private void Start()
	{
	}

	private void Update()
	{
		if (fpsController.Gen <= 330f)
		{
			fpsController.Gen = 330f;
		}
	}
}
