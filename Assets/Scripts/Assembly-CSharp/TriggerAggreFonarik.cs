using UnityEngine;

public class TriggerAggreFonarik : MonoBehaviour
{
	public FieldOfView Orange;

	public AI_Monster2 Blue;

	private void Update()
	{
		if (Orange == null)
		{
			Orange = Object.FindObjectOfType<FieldOfView>();
		}
		if (Blue == null)
		{
			Blue = Object.FindObjectOfType<AI_Monster2>();
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "AggreOrange")
		{
			Orange.canSeePlayer = true;
		}
		if (col.tag == "AggreBlue")
		{
			Blue.AI_Enemy = AI_Monster2.AI_State.Stay;
		}
	}
}
