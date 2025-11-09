using UnityEngine;

public class CheckPoint3 : MonoBehaviour
{
	public int toHero;

	public Transform[] toPoints;

	private GameObject player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public Transform getNext()
	{
		if (Random.Range(1, 100) <= toHero)
		{
			return getPointNearHero();
		}
		return toPoints[Random.Range(0, toPoints.Length)];
	}

	private Transform getPointNearHero()
	{
		Transform transform = toPoints[0];
		if (toPoints.Length > 1)
		{
			float num = Vector3.Distance(transform.position, player.transform.position);
			for (int i = 1; i < toPoints.Length; i++)
			{
				if (Vector3.Distance(toPoints[i].position, player.transform.position) < num)
				{
					transform = toPoints[i];
				}
			}
		}
		return transform;
	}
}
