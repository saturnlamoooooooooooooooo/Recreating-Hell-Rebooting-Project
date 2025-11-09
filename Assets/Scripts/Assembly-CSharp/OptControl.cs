using UnityEngine;
using UnityEngine.UI;

public class OptControl : MonoBehaviour
{
	public Toggle LowGraph;

	public Toggle MedGraph;

	public Toggle UltGraph;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Graphics()
	{
		if (MedGraph.isOn)
		{
			QualitySettings.currentLevel = QualityLevel.Fast;
		}
		if (UltGraph.isOn)
		{
			QualitySettings.currentLevel = QualityLevel.Fantastic;
		}
	}
}
