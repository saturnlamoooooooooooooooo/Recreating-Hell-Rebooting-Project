using UnityEngine;
using UnityEngine.UI;

public class Textrand : MonoBehaviour
{
	[SerializeField]
	private Text NameText;

	[SerializeField]
	private string[] Names;

	private void Update()
	{
		NameText.text = Names[Random.Range(0, Names.Length)];
	}
}
