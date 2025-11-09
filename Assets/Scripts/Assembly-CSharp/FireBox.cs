using UnityEngine;

public class FireBox
{
	public Vector3 m_radius = new Vector3(1f, 1f, 1f);

	private Vector3 m_position;

	private string m_terrainName = "Terrain";

	private Collider[] m_overlapOjects = new Collider[10];

	public Vector3 radius
	{
		get
		{
			return m_radius;
		}
		set
		{
			m_radius = value;
		}
	}

	public void Init(Vector3 position, string terrianName)
	{
		m_position = position;
		m_terrainName = terrianName;
	}

	public void DetectionTest()
	{
		Physics.OverlapBoxNonAlloc(m_position, m_radius, m_overlapOjects);
		for (int i = 0; i < 10; i++)
		{
			if (m_overlapOjects[i] != null && m_overlapOjects[i].name != m_terrainName)
			{
				ActivePresentFireNodeChains(m_overlapOjects[i]);
			}
		}
	}

	private bool ActivePresentFireNodeChains(Collider gameObject)
	{
		FireNodeChain component = gameObject.GetComponent<FireNodeChain>();
		if (component != null)
		{
			component.StartFire(m_position);
			return true;
		}
		return false;
	}
}
