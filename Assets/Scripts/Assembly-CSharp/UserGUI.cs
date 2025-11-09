using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UserGUI : MonoBehaviour
{
	[Tooltip("GameObject to display transform rotation information.")]
	public GameObject m_gameObject;

	[Tooltip("Text to print along with rotation information.")]
	public string m_display = "Text";

	private const string m_displayText = "{0} ";

	[Tooltip("Update the text every frame or only at the start?")]
	public bool m_updateText;

	private Quaternion m_rotation;

	private Text m_Text;

	private void Start()
	{
		m_Text = GetComponent<Text>();
		if (m_gameObject != null)
		{
			WindZone component = m_gameObject.GetComponent<WindZone>();
			if (component != null)
			{
				m_rotation = component.transform.rotation;
			}
		}
	}

	private void Update()
	{
		if (m_updateText)
		{
			Transform component = m_gameObject.GetComponent<Transform>();
			if (component != null)
			{
				m_rotation = component.rotation;
			}
		}
		Vector3 vector = new Vector3(0f, 0f, 1f);
		Vector3 vector2 = m_rotation * vector;
		m_Text.text = string.Format("{0} " + m_display, vector2.normalized);
		m_Text.color = Color.white;
	}
}
