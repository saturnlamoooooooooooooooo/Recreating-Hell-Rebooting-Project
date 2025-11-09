using UnityEngine;

public class aiEasy : MonoBehaviour
{
	public float fpsTargetDistance;

	public float enemyLookDistance;

	public float attackDistance;

	public float enemyMovementSpeed;

	public float damping;

	public Transform fpsTarget;

	private Rigidbody theRigidbody;

	private Renderer myRender;

	private void Start()
	{
		myRender = GetComponent<Renderer>();
		theRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		fpsTargetDistance = Vector3.Distance(fpsTarget.position, base.transform.position);
		if (fpsTargetDistance < enemyLookDistance)
		{
			myRender.material.color = Color.yellow;
			lookAtPlayer();
			MonoBehaviour.print("Посмотри пожалуйста на игрока");
		}
		if (fpsTargetDistance < attackDistance)
		{
			myRender.material.color = Color.red;
			attackPlease();
			MonoBehaviour.print("АТАКА!");
		}
		else
		{
			myRender.material.color = Color.blue;
		}
	}

	private void lookAtPlayer()
	{
		Quaternion b = Quaternion.LookRotation(fpsTarget.position - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * damping);
	}

	private void attackPlease()
	{
		theRigidbody.AddForce(base.transform.forward * enemyMovementSpeed);
	}
}
