using UnityEngine;

public class TriggerJustFalse : MonoBehaviour
{
	public Animator Anim;

	public Animator Anim2;

	public Animator Anim3;

	public Animator Anim4;

	public Animator Anim5;

	public Animator Anim6;

	public Animator Anim7;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "GamePlay")
		{
			Anim.SetBool("On", true);
			Anim2.SetBool("On", true);
			Anim3.SetBool("On", true);
			Anim4.SetBool("On", true);
			Anim5.SetBool("On", true);
			Anim6.SetBool("On", true);
			Anim7.SetBool("On", true);
		}
	}
}
