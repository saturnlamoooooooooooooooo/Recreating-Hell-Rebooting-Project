using UnityEngine;

public class FireNodeChain : MonoBehaviour
{
	[Tooltip("Higher the value, quick the fire ignites fuel")]
	public float m_firePropagationSpeed = 20f;

	[Tooltip("Nodes within this chain, should have all nodes so fires start correctly")]
	public FireNode[] m_fireNodes;

	[Tooltip("Enable if GameObject should be destroyed once all nodes been set on fire, do not enable for trees!")]
	public bool m_destroyAfterFire;

	[Tooltip("Enable if GameObject should be replaced with another mesh once all nodes have been set on fire")]
	public bool m_replaceAfterFire;

	[Tooltip("The GameObject that this object should be replaced with")]
	public GameObject m_replacementMesh;

	private float m_combustionRateValue = 1f;

	private bool m_destroyedAlready;

	private bool m_replacedAlready;

	private bool m_validChain = true;

	public float propagationSpeed
	{
		get
		{
			return m_firePropagationSpeed;
		}
		set
		{
			m_firePropagationSpeed = value;
		}
	}

	private void Start()
	{
		try
		{
			GameObject gameObject = GameObject.FindWithTag("Fire");
			if (gameObject != null)
			{
				FireManager component = gameObject.GetComponent<FireManager>();
				if (component != null)
				{
					m_combustionRateValue = component.nodeCombustionRate;
				}
			}
		}
		catch
		{
			FireManager fireManager = Object.FindObjectOfType<FireManager>();
			if (fireManager != null)
			{
				m_combustionRateValue = fireManager.nodeCombustionRate;
			}
			Debug.LogWarning("No 'Fire' tag set, looking for Fire Manager.");
		}
		for (int i = 0; i < m_fireNodes.Length; i++)
		{
			if (m_fireNodes[i] == null)
			{
				Debug.LogError("Fire Node Chain on " + base.gameObject.GetComponentInParent<Transform>().name + " has missing Fire Nodes!");
				m_validChain = false;
				break;
			}
		}
	}

	private void Update()
	{
		if (m_validChain)
		{
			PropagateFire();
			if (m_destroyAfterFire && !m_destroyedAlready)
			{
				DestroyAfterFire();
			}
			if (m_replaceAfterFire && !m_replacedAlready)
			{
				ReplaceAfterFire();
			}
		}
	}

	private void PropagateFire()
	{
		for (int i = 0; i < m_fireNodes.Length; i++)
		{
			if (m_fireNodes[i].isAlight)
			{
				for (int j = 0; j < m_fireNodes[i].m_links.Count; j++)
				{
					if (m_fireNodes[i].m_links[j].GetComponent<FireNode>().HP > 0f)
					{
						m_fireNodes[i].m_links[j].GetComponent<FireNode>().HP -= m_firePropagationSpeed * Time.deltaTime;
					}
				}
			}
			m_fireNodes[i].ForceUpdate();
		}
	}

	public void StartFire(Vector3 firePoisition)
	{
		float num = float.MaxValue;
		int num2 = 0;
		for (int i = 0; i < m_fireNodes.Length; i++)
		{
			float num3 = Vector3.Distance(m_fireNodes[i].GetComponent<Transform>().position, firePoisition);
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		m_fireNodes[num2].HP -= m_combustionRateValue * Time.deltaTime;
	}

	private void DestroyAfterFire()
	{
		bool flag = false;
		for (int i = 0; i < m_fireNodes.Length && m_fireNodes[i].m_fuel <= 0f; i++)
		{
			if (i == m_fireNodes.Length - 1)
			{
				flag = true;
			}
		}
		if (flag)
		{
			Object.Destroy(base.gameObject);
			m_destroyedAlready = true;
		}
	}

	private void ReplaceAfterFire()
	{
		bool flag = false;
		for (int i = 0; i < m_fireNodes.Length && m_fireNodes[i].NodeConsumed(); i++)
		{
			if (i == m_fireNodes.Length - 1)
			{
				flag = true;
			}
		}
		if (flag && m_replacementMesh != null)
		{
			if (m_replacementMesh != null)
			{
				Transform transform = base.gameObject.transform;
				Object.Destroy(base.gameObject);
				Object.Instantiate(m_replacementMesh, transform.position, transform.rotation);
			}
			else
			{
				Debug.Log("Failed to replace the gameobject");
			}
			m_replacedAlready = true;
		}
	}
}
