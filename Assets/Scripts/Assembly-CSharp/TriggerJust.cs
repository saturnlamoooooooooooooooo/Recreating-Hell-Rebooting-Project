using UnityEngine;

public class TriggerJust : MonoBehaviour
{
	public GameObject Tutor;

	public Animator anim;

	public Animator anim2;

	public GameObject BatterySub;

	public FonarikScript fonarikscript;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			anim.SetBool("On", true);
			anim2.SetBool("On", true);
			BatterySub.SetActive(true);
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (!(col.tag == "GamePlay"))
		{
			return;
		}
		if (!fonarikscript.onLight)
		{
			Tutor.SetActive(true);
			if (Input.GetKeyDown(KeyCode.F))
			{
				anim.SetBool("On", true);
				anim2.SetBool("On", true);
				BatterySub.SetActive(true);
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			BatterySub.SetActive(true);
			Object.Destroy(base.gameObject);
		}
	}
}
