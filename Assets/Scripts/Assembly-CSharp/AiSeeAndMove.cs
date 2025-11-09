using UnityEngine;

public class AiSeeAndMove : MonoBehaviour
{
	public float seeDistance = 5f;

	public float speed;

	private Transform target;

	public int healthMonster;

	public bool Target;

	private void Start()
	{
		target = GameObject.FindWithTag("Player").transform;
		Target = true;
	}

	private void Update()
	{
		if (healthMonster <= 0)
		{
			Object.Destroy(base.gameObject);
		}
		if (Vector3.Distance(base.transform.position, target.transform.position) < seeDistance)
		{
			if (Target)
			{
				base.transform.LookAt(target.transform);
				base.transform.Translate(new Vector3(0f, 0f, speed * Time.deltaTime));
			}
		}
		else
		{
			Target = false;
		}
	}
}
