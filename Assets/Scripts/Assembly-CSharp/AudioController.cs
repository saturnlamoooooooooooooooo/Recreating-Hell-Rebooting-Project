using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
	public Slider slider;

	private void Update()
	{
		AudioListener.volume = slider.value;
	}
}
