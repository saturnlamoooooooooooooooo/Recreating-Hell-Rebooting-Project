using System.Collections.Generic;
using UnityEngine;

public class FireNode : MonoBehaviour
{
	[Tooltip("Prefab to be used for the fire.")]
	public GameObject m_fire;

	[Tooltip("GameObjects with a FireNode script, any linked node will be heated up once this node is on fire.")]
	public List<GameObject> m_links;

	[Tooltip("The Hit Points of the cell, the higher the HP to slower the cell is to heat up and ignite.")]
	public float m_HP = 50f;

	[Tooltip("Amount of fuel in the cell.")]
	public float m_fuel = 50f;

	private float m_extinguishThreshold;

	private float m_combustionRateValue;

	private bool m_fireJustStarted;

	private bool m_isAlight;

	private bool m_extingushed;

	private bool m_clean;

	private FireVisualManager m_visualMgr;

	public GameObject flames
	{
		get
		{
			return m_fire;
		}
	}

	public bool isAlight
	{
		get
		{
			return m_isAlight;
		}
	}

	public bool fireJustStarted
	{
		get
		{
			return m_fireJustStarted;
		}
		set
		{
			m_fireJustStarted = value;
		}
	}

	public float HP
	{
		get
		{
			return m_HP;
		}
		set
		{
			m_HP = value;
		}
	}

	public float extinguishThreshold
	{
		get
		{
			return m_extinguishThreshold;
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
					m_extinguishThreshold = m_fuel * component.visualExtinguishThreshold;
					m_combustionRateValue = component.nodeCombustionRate;
				}
				else
				{
					m_extinguishThreshold = m_fuel;
					m_combustionRateValue = 1f;
				}
			}
		}
		catch
		{
			FireManager fireManager = Object.FindObjectOfType<FireManager>();
			if (fireManager != null)
			{
				m_extinguishThreshold = m_fuel * fireManager.visualExtinguishThreshold;
				m_combustionRateValue = fireManager.nodeCombustionRate;
			}
			else
			{
				m_extinguishThreshold = m_fuel;
				m_combustionRateValue = 1f;
			}
		}
	}

	private void KillFlames()
	{
		Object.Destroy(m_fire);
	}

	private void Update()
	{
		if (m_HP <= 0f && !m_isAlight)
		{
			m_fireJustStarted = true;
		}
		Ingition();
		Combustion();
	}

	public bool NodeConsumed()
	{
		if (m_clean)
		{
			return true;
		}
		return false;
	}

	public void ForceUpdate()
	{
		Update();
	}

	public void InstantiateFire(Vector3 position, GameObject Fire)
	{
		if (m_fireJustStarted)
		{
			m_fire = Object.Instantiate(Fire, position, default(Quaternion));
			m_isAlight = true;
		}
	}

	private void Ingition()
	{
		if (m_fireJustStarted && !m_extingushed)
		{
			InstantiateFire(base.transform.position, m_fire);
			m_fireJustStarted = false;
			GetVisualManager();
			if (m_visualMgr != null)
			{
				m_visualMgr.SetIgnitionState();
			}
		}
	}

	private void Combustion()
	{
		if (m_isAlight)
		{
			m_fireJustStarted = false;
			m_fuel -= m_combustionRateValue * Time.deltaTime;
			if (m_fuel < m_extinguishThreshold && m_visualMgr != null)
			{
				m_visualMgr.SetExtingushState();
			}
			if (m_fuel <= 0f)
			{
				m_isAlight = false;
				m_extingushed = true;
				KillFlames();
				m_clean = true;
			}
		}
	}

	private void GetVisualManager()
	{
		if (m_visualMgr == null)
		{
			m_visualMgr = m_fire.GetComponent<FireVisualManager>();
		}
	}
}
