using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings1 : MonoBehaviour
{
	public Dropdown resolutionDropdown;

	public Dropdown qualityDropdown;

	public Slider slider;

	private float currentVolume;

	private Resolution[] resolutions;

	private void Start()
	{
		resolutionDropdown.ClearOptions();
		List<string> list = new List<string>();
		resolutions = Screen.resolutions;
		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string item = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
			list.Add(item);
			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(list);
		resolutionDropdown.RefreshShownValue();
		LoadSettings(currentResolutionIndex);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void ExitSettings()
	{
		SceneManager.LoadScene("Level");
	}

	public void SaveSettings()
	{
		PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
		PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
		PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
		PlayerPrefs.SetFloat("VolumePreference", currentVolume);
	}

	public void LoadSettings(int currentResolutionIndex)
	{
		if (PlayerPrefs.HasKey("QualitySettingPreference"))
		{
			qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
		}
		else
		{
			qualityDropdown.value = 0;
		}
		if (PlayerPrefs.HasKey("ResolutionPreference"))
		{
			resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
		}
		else
		{
			resolutionDropdown.value = currentResolutionIndex;
		}
		if (PlayerPrefs.HasKey("FullscreenPreference"))
		{
			Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
		}
		else
		{
			Screen.fullScreen = true;
		}
	}

	private void Update()
	{
		AudioListener.volume = slider.value;
	}
}
