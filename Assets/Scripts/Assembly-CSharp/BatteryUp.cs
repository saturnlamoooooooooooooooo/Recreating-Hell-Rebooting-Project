using UnityEngine;

public class BatteryUp : MonoBehaviour
{
	public GameObject Ebutton;

	public GameObject battery;

	public BatteryUp Script;

	public BoxCollider ScriptCol;

	public GameObject Sound;

	public GameObject E;

	public Animator Active;

	private void Awake()
	{
		Ebutton.SetActive(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "MainCamera")
		{
			Ebutton.SetActive(true);
			Active.SetBool("IsOpen", true);
			if (Input.GetKey(KeyCode.E))
			{
				ScriptCol.enabled = false;
				Sound.SetActive(true);
				col.gameObject.GetComponent<FonarikScript>().Energy += 37;
				battery.SetActive(false);
				Script.enabled = false;
				col.gameObject.GetComponent<FonarikScript>().AnimatorOpenClose.SetBool("open", false);
				col.gameObject.GetComponent<FonarikScript>().AnimatorF.SetBool("IsOpen", false);
				col.gameObject.GetComponent<FonarikScript>().onLight = false;
				col.gameObject.GetComponent<FonarikScript>().onLight2 = false;
				E.SetActive(false);
			}
		}
		if (col.tag == "BatteryDestroy")
		{
			ScriptCol.enabled = false;
			Sound.SetActive(true);
			battery.SetActive(false);
			Script.enabled = false;
			E.SetActive(false);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "MainCamera")
		{
			Ebutton.SetActive(false);
			Active.SetBool("IsOpen", false);
		}
	}
}
