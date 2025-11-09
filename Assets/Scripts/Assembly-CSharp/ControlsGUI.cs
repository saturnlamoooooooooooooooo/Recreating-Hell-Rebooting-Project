using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ControlsGUI : MonoBehaviour
{
	public string m_inputAction = "Action";

	public string m_displayText = "Key";

	private Text m_Text;

	private void Start()
	{
		m_Text = GetComponent<Text>();
	}

	private void Update()
	{
		m_Text.text = string.Format(m_inputAction + ": " + m_displayText);
		m_Text.color = Color.white;
	}
}
