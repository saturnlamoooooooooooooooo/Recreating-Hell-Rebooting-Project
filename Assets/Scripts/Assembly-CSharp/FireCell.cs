using UnityEngine;

public class FireCell : MonoBehaviour
{
	private GameObject m_firePrefab;

	private GameObject[] m_fires;

	private FireBox m_fireBox;

	[Tooltip("The Hit Points of the cell, the higher the HP to slower the cell is to heat up and ignite.")]
	public float m_HP = 50f;

	[Tooltip("Amount of fuel in the cell.")]
	public float m_fuel = 60f;

	private float m_extinguishThreshold;

	[Tooltip("The amount of ground moisture in the cell.")]
	public float m_moisture;

	private float m_ignitionTemperature = 1f;

	private float m_fireTemperature;

	private float m_combustionConstant = 1f;

	private float m_pIgnition;

	private bool m_instantiatedInCell;

	private bool m_temperatureModified;

	private bool m_fireProcessHappening;

	private bool m_fireJustStarted;

	private bool m_isAlight;

	private bool m_extinguish;

	private bool m_extingushed;

	private int m_iginitionCounter;

	private Vector2[] m_firesPositions;

	public Vector3 position
	{
		get
		{
			return base.transform.position;
		}
	}

	public float fireTemperature
	{
		get
		{
			return m_fireTemperature;
		}
		set
		{
			m_fireTemperature = value;
		}
	}

	public float ignitionTemperature
	{
		get
		{
			return m_ignitionTemperature;
		}
		set
		{
			m_ignitionTemperature = value;
		}
	}

	public bool temperatureModified
	{
		get
		{
			return m_temperatureModified;
		}
		set
		{
			m_temperatureModified = value;
		}
	}

	public float extinguishThreshold
	{
		get
		{
			return m_extinguishThreshold;
		}
	}

	public bool fireInCell
	{
		get
		{
			return m_instantiatedInCell;
		}
	}

	public bool isAlight
	{
		get
		{
			return m_isAlight;
		}
	}

	public bool fireProcessHappening
	{
		get
		{
			return m_fireProcessHappening;
		}
	}

	private void Update()
	{
		if (!m_fireJustStarted && !m_isAlight && m_fireProcessHappening && m_pIgnition == m_ignitionTemperature)
		{
			m_iginitionCounter++;
		}
		else
		{
			m_iginitionCounter = 0;
		}
		if (m_iginitionCounter > 300)
		{
			m_extinguish = true;
		}
		m_pIgnition = m_ignitionTemperature;
	}

	public void SetupCell(bool alight, GameObject fire, CellData data, string terrainName, Vector2[] firesPositionsInCell)
	{
		m_isAlight = alight;
		m_firePrefab = fire;
		m_fires = new GameObject[firesPositionsInCell.Length];
		m_firesPositions = firesPositionsInCell;
		m_HP = data.HP;
		m_fuel = data.fuel;
		m_extinguishThreshold = data.fuel * data.threshold;
		m_moisture = data.moisture;
		m_fireBox = new FireBox();
		m_fireBox.Init(base.transform.position, terrainName);
		float num = data.cellSize / 2f;
		m_fireBox.radius = new Vector3(num, num, num);
		m_combustionConstant = data.combustionValue;
		SetInitialFireValues(data.airTemperature, data.propagationSpeed);
	}

	public void Delete()
	{
		if (m_instantiatedInCell && m_extingushed)
		{
			base.gameObject.SetActive(false);
			m_fireProcessHappening = false;
		}
		else if (m_instantiatedInCell)
		{
			base.gameObject.SetActive(false);
			m_fireProcessHappening = false;
		}
	}

	public void GridUpdate(FireGrassRemover script)
	{
		Combustion();
		if (m_extinguish && !m_extingushed)
		{
			script.DeleteGrassOnPosition(base.transform.position);
			m_extingushed = true;
			if (m_fireBox != null)
			{
				m_fireBox = null;
			}
			Delete();
		}
	}

	private void InstantiateFire(Vector3 position, GameObject Fire)
	{
		for (int i = 0; i < m_fires.Length; i++)
		{
			m_fires[i] = Object.Instantiate(Fire, position + new Vector3(m_firesPositions[i].x, 0f, m_firesPositions[i].y), default(Quaternion), base.transform);
		}
	}

	public void Ignition(Vector3 position, GameObject Fire)
	{
		if (m_fireJustStarted)
		{
			m_isAlight = true;
			for (int i = 0; i < m_fires.Length; i++)
			{
				m_fires[i].GetComponent<FireVisualManager>().SetIgnitionState();
			}
		}
	}

	public void SetInitialFireValues(float airTemp, float globalFirePropagationSpeed)
	{
		m_ignitionTemperature = m_HP - airTemp + m_moisture;
		if (m_HP > 0f)
		{
			m_fireTemperature += m_fuel / m_HP + globalFirePropagationSpeed;
		}
		else
		{
			m_fireTemperature += globalFirePropagationSpeed;
		}
	}

	public void HeatsUp()
	{
		if (!m_instantiatedInCell)
		{
			InstantiateFire(base.transform.position, m_firePrefab);
			m_instantiatedInCell = true;
			m_fireProcessHappening = true;
			for (int i = 0; i < m_fires.Length; i++)
			{
				m_fires[i].GetComponent<FireVisualManager>().SetHeatState();
			}
		}
		if (m_ignitionTemperature > 0f)
		{
			m_ignitionTemperature -= m_fireTemperature * Time.deltaTime;
		}
		if (m_ignitionTemperature <= 0f && !m_isAlight)
		{
			m_fireJustStarted = true;
			Ignition();
		}
	}

	private void Ignition()
	{
		if (m_fireJustStarted && !m_extingushed)
		{
			Ignition(base.transform.position, m_firePrefab);
		}
	}

	private void Combustion()
	{
		if (!m_isAlight)
		{
			return;
		}
		m_fireJustStarted = false;
		m_fuel -= m_combustionConstant * Time.deltaTime;
		if (m_fuel < m_extinguishThreshold)
		{
			for (int i = 0; i < m_fires.Length; i++)
			{
				m_fires[i].GetComponent<FireVisualManager>().SetExtingushState();
			}
		}
		if (m_fuel <= 0f)
		{
			m_isAlight = false;
			m_extinguish = true;
		}
		m_fireBox.DetectionTest();
	}
}
