using UnityEngine;

public class HideFonarikoff : MonoBehaviour
{
	public FonarikScript script;

	public FieldOfView AI1;

	public FieldOfView2 AI2;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Hide" && script.onLight)
		{
			script.FonarikSound2.Play();
			AI2.canSeePlayer = false;
		}
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Hide")
		{
			script.onLight = false;
			script.onLight2 = false;
			script.AnimatorOpenClose.SetBool("open", false);
			script.AnimatorF.SetBool("IsOpen", false);
		}
	}

	private void Update()
	{
	}
}
