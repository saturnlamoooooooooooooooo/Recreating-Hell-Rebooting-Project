using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EasyFps : MonoBehaviour
{
	private bool can10;

	private bool can30;

	private bool can60;

	private bool can120;

	private bool canmax;

	public UnityEvent OnFpsLessThan10;

	public UnityEvent OnFpsLessThan30;

	public UnityEvent OnFpsLessThan60;

	public UnityEvent OnFpsLessThan120;

	public UnityEvent OnFpsMoreThanMax;

	public bool ncm;

	public int maxFR;

	public float refresht = 0.5f;

	private int frameCounter;

	private float timeCounter;

	private float lastFramerate;

	private bool acttxt = true;

	private Text txt;

	private int mx = 60;

	[SerializeField]
	public float FPS
	{
		get
		{
			return lastFramerate;
		}
	}

	[SerializeField]
	public float RefreshTime
	{
		get
		{
			return refresht;
		}
		set
		{
			refresht = value;
		}
	}

	public int MaxFrameRate
	{
		get
		{
			return mx;
		}
		set
		{
			mx = value;
			Application.targetFrameRate = value;
		}
	}

	private void Start()
	{
		txt = base.transform.Find("Text").GetComponent<Text>();
		if (ncm)
		{
			mx = maxFR;
			RefreshTime = refresht;
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = maxFR;
		}
		EasyFpsCounter.EasyFps = this;
	}

	private void Update()
	{
		if (timeCounter < refresht)
		{
			timeCounter += Time.deltaTime;
			frameCounter++;
			return;
		}
		lastFramerate = (float)frameCounter / timeCounter;
		int num = (int)lastFramerate;
		if (!can10 && lastFramerate >= 10f)
		{
			can10 = true;
		}
		else if (can10 && lastFramerate < 10f)
		{
			can10 = false;
			OnFpsLessThan10.Invoke();
		}
		if (!can30 && lastFramerate >= 30f)
		{
			can30 = true;
		}
		else if (can30 && lastFramerate < 30f)
		{
			can30 = false;
			OnFpsLessThan30.Invoke();
		}
		if (!can60 && lastFramerate >= 60f)
		{
			can60 = true;
		}
		else if (can60 && lastFramerate < 60f)
		{
			can60 = false;
			OnFpsLessThan60.Invoke();
		}
		if (!can120 && lastFramerate >= 120f)
		{
			can120 = true;
		}
		else if (can120 && lastFramerate < 120f)
		{
			can120 = false;
			OnFpsLessThan120.Invoke();
		}
		if (!canmax && lastFramerate <= (float)MaxFrameRate)
		{
			canmax = true;
		}
		else if (canmax && lastFramerate > (float)MaxFrameRate)
		{
			canmax = false;
			OnFpsMoreThanMax.Invoke();
		}
		if (acttxt)
		{
			if (lastFramerate <= (float)MaxFrameRate)
			{
				txt.text = num.ToString();
			}
			else
			{
				txt.text = MaxFrameRate + "+";
			}
		}
		frameCounter = 0;
		timeCounter = 0f;
	}

	public void HideFps()
	{
		acttxt = false;
		txt.gameObject.SetActive(false);
	}

	public void ShowFps()
	{
		acttxt = true;
		txt.gameObject.SetActive(true);
	}
}
