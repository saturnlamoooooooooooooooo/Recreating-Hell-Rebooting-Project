using UnityEngine;

public class ModelPlayerAnim : MonoBehaviour
{
	public float speedThreshold = 10f;

	public float speedThreshold2 = 10f;

	public Animator animation;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (GetComponent<Rigidbody>().velocity.magnitude >= speedThreshold)
		{
			animation.SetBool("IsOpen", true);
		}
		else if (GetComponent<Rigidbody>().velocity.magnitude < speedThreshold)
		{
			animation.SetBool("IsOpen", false);
		}
		if (GetComponent<Rigidbody>().velocity.magnitude >= speedThreshold2)
		{
			animation.SetBool("IsOpen2", true);
		}
		else if (GetComponent<Rigidbody>().velocity.magnitude < speedThreshold2)
		{
			animation.SetBool("IsOpen2", false);
		}
	}
}
