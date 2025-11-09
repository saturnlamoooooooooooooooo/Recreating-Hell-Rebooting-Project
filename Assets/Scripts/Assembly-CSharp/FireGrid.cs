using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGrid : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Prefab to be used for the fire.")]
	private GameObject m_firePrefab;

	private FireManager m_fireManager;

	private WindZone m_windZone;

	private Terrain m_terrain;

	private GameObject[,] m_cells;

	private SortedList<int, Vector2> m_alightCellIndex;

	private Vector3 m_origin;

	private string m_terrianName;

	private float m_airTemperature;

	private float m_cellSize;

	[SerializeField]
	[Tooltip("Number of cells in the gird (width).")]
	private int m_widthCells = 45;

	[SerializeField]
	[Tooltip("Number of cells in the gird (height).")]
	private int m_heightCells = 45;

	private int m_allocatedListSize;

	private int m_DToAWidth;

	private int m_DToAHeight;

	private Vector3 m_propagationDirectionNorth;

	private Vector3 m_propagationDirectionEast;

	private Vector3 m_propagationDirectionSouth;

	private Vector3 m_propagationDirectionWest;

	private float m_bias;

	private float m_windBias;

	private float m_hillBias;

	private float m_maxHillDistance;

	private float m_visualThreshold;

	private float m_combustionRateValue;

	private bool m_day;

	private int m_width;

	private int m_height;

	private int m_fireCellsLit;

	private bool m_fastSim;

	private bool m_centerCellIgnited;

	[SerializeField]
	[Tooltip("Using a FireIgniter?")]
	private bool m_useIgniter = true;

	private bool m_gridCreated;

	private bool m_gridSlowBuild = true;

	private void Start()
	{
		try
		{
			GameObject gameObject = GameObject.FindWithTag("Fire");
			if (gameObject != null)
			{
				m_fireManager = gameObject.GetComponent<FireManager>();
			}
			else
			{
				m_fireManager = Object.FindObjectOfType<FireManager>();
				Debug.LogWarning("Fire Manager does not have the tag 'Fire'.");
			}
		}
		catch
		{
			m_fireManager = Object.FindObjectOfType<FireManager>();
			Debug.LogWarning("No 'Fire' tag set, looking for Fire Manager.");
		}
		if (m_fireManager != null)
		{
			m_DToAWidth = m_fireManager.terrainWidth / m_fireManager.alphaWidth;
			m_DToAHeight = m_fireManager.terrainHeight / m_fireManager.alphaHeight;
			m_propagationDirectionNorth = new Vector3(0f, 0f, 1f);
			m_propagationDirectionEast = new Vector3(1f, 0f, 0f);
			m_propagationDirectionSouth = new Vector3(0f, 0f, -1f);
			m_propagationDirectionWest = new Vector3(-1f, 0f, 0f);
			if (ValidIgnitionLocation())
			{
				m_windZone = m_fireManager.windzone;
				m_airTemperature = m_fireManager.airTemperature;
				m_cellSize = m_fireManager.cellSize;
				m_bias = m_fireManager.propagationBias;
				m_windBias = m_fireManager.propagationWindBias;
				m_hillBias = m_fireManager.propagationHillBias;
				m_maxHillDistance = m_fireManager.maxHillPropagationDistance;
				m_day = m_fireManager.daytime;
				m_visualThreshold = m_fireManager.visualExtinguishThreshold;
				m_combustionRateValue = m_fireManager.combustionRate;
				m_allocatedListSize = m_fireManager.preAllocatedFireIndexSize;
				m_terrianName = m_fireManager.terrain.name;
				m_fastSim = !m_fireManager.detailedSimulation;
				m_gridSlowBuild = m_fireManager.staggeredGridConstruction;
				m_fireManager.AddActiveFireGrid();
				GetComponentInParent<FireGrassRemover>().radius = m_cellSize;
				if (m_useIgniter)
				{
					m_terrain = m_fireManager.terrain;
				}
				else
				{
					m_terrain = GetComponentInParent<Terrain>();
				}
				if (m_terrain != null)
				{
					Vector3 size = m_terrain.terrainData.size;
					m_origin = m_terrain.transform.position;
					if (m_widthCells > (int)size.z)
					{
						m_widthCells = (int)size.z;
					}
					if (m_heightCells > (int)size.x)
					{
						m_heightCells = (int)size.x;
					}
					m_width = m_widthCells;
					m_height = m_heightCells;
					if (m_gridSlowBuild)
					{
						StartCoroutine(BuildGridStaged());
					}
					else
					{
						BuildGrid();
					}
				}
				else
				{
					Debug.LogError("Not a child of a Terrain GameObject!");
				}
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			Debug.LogWarning("No FireManager found in the scene!");
		}
	}

	private void Update()
	{
		if (!(m_fireManager != null) || !m_gridCreated)
		{
			return;
		}
		if (!m_centerCellIgnited)
		{
			Vector2 vector = new Vector2((float)m_cells.GetLength(0) / 2f, (float)m_cells.GetLength(1) / 2f);
			m_cells[(int)vector.x, (int)vector.y].GetComponent<FireCell>().ignitionTemperature = 0f;
			m_cells[(int)vector.x, (int)vector.y].GetComponent<FireCell>().HeatsUp();
			m_centerCellIgnited = true;
		}
		if (m_fastSim)
		{
			if (m_windZone != null)
			{
				Quaternion rotation = m_windZone.transform.rotation;
				Vector3 vector2 = new Vector3(0f, 0f, 1f);
				FastPropagation((rotation * vector2).normalized);
			}
			else
			{
				Propagation();
			}
		}
		else if (m_windZone != null)
		{
			Quaternion rotation2 = m_windZone.transform.rotation;
			Vector3 vector3 = new Vector3(0f, 0f, 1f);
			vector3 = (rotation2 * vector3).normalized;
			Propagation(vector3);
		}
		else
		{
			Propagation();
		}
		if (m_fireCellsLit == 0)
		{
			Object.Destroy(base.gameObject, 2f);
		}
	}

	private void OnDestroy()
	{
		if (m_fireManager != null)
		{
			m_fireManager.RemoveActiveFireGrid();
		}
	}

	public void IgniterUpdate(GameObject firePrefab, Vector3 position, int gridWidth, int gridHeight)
	{
		base.transform.position = position;
		m_widthCells = gridWidth;
		m_heightCells = gridHeight;
		m_firePrefab = firePrefab;
	}

	private IEnumerator BuildGridStaged()
	{
		m_alightCellIndex = new SortedList<int, Vector2>(m_allocatedListSize);
		float offsetX = 0f;
		float offsetY = 0f;
		if (m_width % 2 == 0)
		{
			offsetX = (float)m_width / 2f * m_cellSize;
		}
		else if (m_width % 2 == 1)
		{
			offsetX = (float)(m_width - 1) / 2f * m_cellSize;
		}
		if (m_height % 2 == 0)
		{
			offsetY = (float)m_height / 2f * m_cellSize;
		}
		else if (m_height % 2 == 1)
		{
			offsetY = (float)(m_height - 1) / 2f * m_cellSize;
		}
		m_cells = new GameObject[m_width, m_height];
		GameObject tmp = new GameObject();
		tmp.AddComponent<FireCell>();
		Quaternion quat = default(Quaternion);
		CellData cellData = default(CellData);
		cellData.airTemperature = m_airTemperature;
		cellData.threshold = m_visualThreshold;
		cellData.combustionValue = m_combustionRateValue;
		cellData.cellSize = m_cellSize;
		yield return null;
		for (int x = 0; x < m_width; x++)
		{
			for (int i = 0; i < m_height; i++)
			{
				Vector2 gridPosition = new Vector2(base.transform.position.x - offsetX + (float)x * m_cellSize, base.transform.position.z - offsetY + (float)i * m_cellSize);
				Vector3 worldPosition = GetWorldPosition(gridPosition);
				worldPosition.y = m_terrain.SampleHeight(worldPosition) + 0.001f;
				m_cells[x, i] = Object.Instantiate(tmp, worldPosition, quat, base.transform);
				m_cells[x, i].name = "FireCell " + (x * m_width + i);
				FireCell component = m_cells[x, i].GetComponent<FireCell>();
				cellData.propagationSpeed = GetValuesFromFuelType((int)worldPosition.x, (int)worldPosition.z, out cellData.HP, out cellData.fuel, out cellData.moisture);
				component.SetupCell(false, m_firePrefab, cellData, m_terrianName, m_fireManager.cellFireSpawnPositions);
				float num = Vector3.Distance(component.position, base.transform.position);
				num /= m_bias;
				component.fireTemperature -= num;
			}
			yield return null;
		}
		Object.DestroyImmediate(tmp);
		m_gridCreated = true;
	}

	private void BuildGrid()
	{
		m_alightCellIndex = new SortedList<int, Vector2>(m_allocatedListSize);
		float num = 0f;
		float num2 = 0f;
		if (m_width % 2 == 0)
		{
			num = (float)m_width / 2f * m_cellSize;
		}
		else if (m_width % 2 == 1)
		{
			num = (float)(m_width - 1) / 2f * m_cellSize;
		}
		if (m_height % 2 == 0)
		{
			num2 = (float)m_height / 2f * m_cellSize;
		}
		else if (m_height % 2 == 1)
		{
			num2 = (float)(m_height - 1) / 2f * m_cellSize;
		}
		m_cells = new GameObject[m_width, m_height];
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<FireCell>();
		Quaternion rotation = default(Quaternion);
		CellData data = default(CellData);
		data.airTemperature = m_airTemperature;
		data.threshold = m_visualThreshold;
		data.combustionValue = m_combustionRateValue;
		data.cellSize = m_cellSize;
		for (int i = 0; i < m_width; i++)
		{
			for (int j = 0; j < m_height; j++)
			{
				Vector2 gridPosition = new Vector2(base.transform.position.x - num + (float)i * m_cellSize, base.transform.position.z - num2 + (float)j * m_cellSize);
				Vector3 worldPosition = GetWorldPosition(gridPosition);
				worldPosition.y = m_terrain.SampleHeight(worldPosition) + 0.001f;
				m_cells[i, j] = Object.Instantiate(gameObject, worldPosition, rotation, base.transform);
				m_cells[i, j].name = "FireCell " + (i * m_width + j);
				FireCell component = m_cells[i, j].GetComponent<FireCell>();
				data.propagationSpeed = GetValuesFromFuelType((int)worldPosition.x, (int)worldPosition.z, out data.HP, out data.fuel, out data.moisture);
				component.SetupCell(false, m_firePrefab, data, m_terrianName, m_fireManager.cellFireSpawnPositions);
				float num3 = Vector3.Distance(component.position, base.transform.position);
				num3 /= m_bias;
				component.fireTemperature -= num3;
			}
		}
		Object.DestroyImmediate(gameObject);
		m_gridCreated = true;
	}

	public Vector3 GetWorldPosition(Vector2 gridPosition)
	{
		return new Vector3(m_origin.z + gridPosition.x, m_origin.y, m_origin.x + gridPosition.y);
	}

	public Vector2 GetGridPosition(Vector3 worldPosition)
	{
		return new Vector2(worldPosition.z / m_cellSize, worldPosition.x / m_cellSize);
	}

	private void FastPropagation(Vector3 windDirection)
	{
		m_fireCellsLit = 0;
		for (int i = 0; i < m_width; i++)
		{
			for (int j = 0; j < m_height; j++)
			{
				Vector2 value = new Vector2(i, j);
				int key = i * m_width + j;
				bool flag = m_alightCellIndex.ContainsKey(key);
				if (m_cells[i, j].GetComponent<FireCell>().fireProcessHappening && !flag)
				{
					m_alightCellIndex.Add(key, value);
				}
				else if (flag)
				{
					m_alightCellIndex.Remove(key);
				}
			}
		}
		FireGrassRemover componentInParent = GetComponentInParent<FireGrassRemover>();
		foreach (Vector2 value2 in m_alightCellIndex.Values)
		{
			int num = (int)value2.x;
			int num2 = (int)value2.y;
			FireCell component = m_cells[num, num2].GetComponent<FireCell>();
			component.GridUpdate(componentInParent);
			if (!component.isAlight)
			{
				continue;
			}
			if (num < m_width - 1)
			{
				if (Vector3.Dot(windDirection, m_propagationDirectionEast.normalized) >= 0f)
				{
					FireCell component2 = m_cells[num + 1, num2].GetComponent<FireCell>();
					if (PropagatingOnSlope(component.position, component2.position) != -1)
					{
						component2.HeatsUp();
					}
				}
				else
				{
					FireCell component3 = m_cells[num + 1, num2].GetComponent<FireCell>();
					if (PropagatingOnSlope(component.position, component3.position) != -1)
					{
						component3.fireTemperature *= ComputeHeadWind();
						component3.temperatureModified = true;
						component3.HeatsUp();
					}
				}
			}
			if (num2 < m_height - 1)
			{
				if (Vector3.Dot(windDirection, m_propagationDirectionNorth.normalized) >= 0f)
				{
					FireCell component4 = m_cells[num, num2 + 1].GetComponent<FireCell>();
					if (PropagatingOnSlope(component.position, component4.position) != -1)
					{
						component4.HeatsUp();
					}
				}
				else
				{
					FireCell component5 = m_cells[num, num2 + 1].GetComponent<FireCell>();
					if (PropagatingOnSlope(component.position, component5.position) != -1)
					{
						component5.fireTemperature *= ComputeHeadWind();
						component5.temperatureModified = true;
						component5.HeatsUp();
					}
				}
			}
			if (num > 0)
			{
				if (Vector3.Dot(windDirection, m_propagationDirectionWest.normalized) >= 0f)
				{
					FireCell component6 = m_cells[num - 1, num2].GetComponent<FireCell>();
					if (PropagatingOnSlope(component.position, component6.position) != -1)
					{
						component6.HeatsUp();
					}
				}
				else
				{
					FireCell component7 = m_cells[num - 1, num2].GetComponent<FireCell>();
					if (PropagatingOnSlope(component.position, component7.position) != -1)
					{
						component7.fireTemperature *= ComputeHeadWind();
						component7.temperatureModified = true;
						component7.HeatsUp();
					}
				}
			}
			if (num2 <= 0)
			{
				continue;
			}
			if (Vector3.Dot(windDirection, m_propagationDirectionSouth.normalized) >= 0f)
			{
				FireCell component8 = m_cells[num, num2 - 1].GetComponent<FireCell>();
				if (PropagatingOnSlope(component.position, component8.position) != -1)
				{
					component8.HeatsUp();
				}
				continue;
			}
			FireCell component9 = m_cells[num, num2 - 1].GetComponent<FireCell>();
			if (PropagatingOnSlope(component.position, component9.position) != -1)
			{
				component9.fireTemperature *= ComputeHeadWind();
				component9.temperatureModified = true;
				component9.HeatsUp();
			}
		}
		m_fireCellsLit = m_alightCellIndex.Count;
	}

	private void Propagation(Vector3 windDirection)
	{
		m_fireCellsLit = 0;
		for (int i = 0; i < m_width; i++)
		{
			for (int j = 0; j < m_height; j++)
			{
				Vector2 value = new Vector2(i, j);
				int key = i * m_width + j;
				bool flag = m_alightCellIndex.ContainsKey(key);
				if (m_cells[i, j].GetComponent<FireCell>().fireProcessHappening && !flag)
				{
					m_alightCellIndex.Add(key, value);
				}
				else if (flag)
				{
					m_alightCellIndex.Remove(key);
				}
			}
		}
		foreach (Vector2 value2 in m_alightCellIndex.Values)
		{
			int num = (int)value2.x;
			int num2 = (int)value2.y;
			FireCell component = m_cells[num, num2].GetComponent<FireCell>();
			component.GridUpdate(GetComponentInParent<FireGrassRemover>());
			if (!component.isAlight)
			{
				continue;
			}
			if (num < m_width - 1)
			{
				if (Vector3.Dot(windDirection, m_propagationDirectionEast.normalized) >= 0f)
				{
					FireCell component2 = m_cells[num + 1, num2].GetComponent<FireCell>();
					int num3 = PropagatingOnSlope(component.position, component2.position);
					switch (num3)
					{
					case 1:
						if (!component2.temperatureModified)
						{
							if (m_day)
							{
								float num6 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
								component2.fireTemperature *= num6;
							}
							else
							{
								float num7 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
								component2.fireTemperature *= num7;
							}
							component2.temperatureModified = true;
						}
						break;
					case 2:
						if (!component2.temperatureModified)
						{
							if (m_day)
							{
								float num4 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
								component2.fireTemperature *= num4;
							}
							else
							{
								float num5 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
								component2.fireTemperature *= num5;
							}
							component2.temperatureModified = true;
						}
						break;
					}
					if (num3 != -1)
					{
						component2.HeatsUp();
					}
				}
				else
				{
					FireCell component3 = m_cells[num + 1, num2].GetComponent<FireCell>();
					int num8 = PropagatingOnSlope(component.position, component3.position);
					switch (num8)
					{
					case 0:
						if (!component3.temperatureModified)
						{
							float num11 = Mathf.Abs(ComputeHeadWind() - m_bias);
							component3.fireTemperature *= num11;
							component3.temperatureModified = true;
						}
						break;
					case 1:
						if (!m_cells[num + 1, num2].GetComponent<FireCell>().temperatureModified)
						{
							if (m_day)
							{
								float num12 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWindBias());
								component3.fireTemperature *= num12;
							}
							else
							{
								float num13 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
								component3.fireTemperature *= num13;
							}
							component3.temperatureModified = true;
						}
						break;
					case 2:
						if (!component3.temperatureModified)
						{
							if (m_day)
							{
								float num9 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWind());
								component3.fireTemperature *= num9;
							}
							else
							{
								float num10 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
								component3.fireTemperature *= num10;
							}
							component3.temperatureModified = true;
						}
						break;
					}
					if (num8 != -1)
					{
						component3.HeatsUp();
					}
				}
			}
			if (num2 < m_height - 1)
			{
				if (Vector3.Dot(windDirection, m_propagationDirectionNorth.normalized) >= 0f)
				{
					FireCell component4 = m_cells[num, num2 + 1].GetComponent<FireCell>();
					int num14 = PropagatingOnSlope(component.position, component4.position);
					switch (num14)
					{
					case 1:
						if (!component4.temperatureModified)
						{
							if (m_day)
							{
								float num17 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
								component4.fireTemperature *= num17;
							}
							else
							{
								float num18 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
								component4.fireTemperature *= num18;
							}
							component4.temperatureModified = true;
						}
						break;
					case 2:
						if (!component4.temperatureModified)
						{
							if (m_day)
							{
								float num15 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
								component4.fireTemperature *= num15;
							}
							else
							{
								float num16 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
								component4.fireTemperature *= num16;
							}
							component4.temperatureModified = true;
						}
						break;
					}
					if (num14 != -1)
					{
						component4.HeatsUp();
					}
				}
				else
				{
					FireCell component5 = m_cells[num, num2 + 1].GetComponent<FireCell>();
					int num19 = PropagatingOnSlope(component.position, component5.position);
					switch (num19)
					{
					case 0:
						if (!m_cells[num, num2 + 1].GetComponent<FireCell>().temperatureModified)
						{
							float num22 = Mathf.Abs(ComputeHeadWind() - m_bias);
							component5.fireTemperature *= num22;
							component5.temperatureModified = true;
						}
						break;
					case 1:
						if (!component5.temperatureModified)
						{
							if (m_day)
							{
								float num23 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWindBias());
								component5.fireTemperature *= num23;
							}
							else
							{
								float num24 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
								component5.fireTemperature *= num24;
							}
							component5.temperatureModified = true;
						}
						break;
					case 2:
						if (!component5.temperatureModified)
						{
							if (m_day)
							{
								float num20 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWind());
								component5.fireTemperature *= num20;
							}
							else
							{
								float num21 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
								component5.fireTemperature *= num21;
							}
							component5.temperatureModified = true;
						}
						break;
					}
					if (num19 != -1)
					{
						component5.HeatsUp();
					}
				}
			}
			if (num > 0)
			{
				if (Vector3.Dot(windDirection, m_propagationDirectionWest.normalized) >= 0f)
				{
					FireCell component6 = m_cells[num - 1, num2].GetComponent<FireCell>();
					int num25 = PropagatingOnSlope(component.position, component6.position);
					switch (num25)
					{
					case 1:
						if (!component6.temperatureModified)
						{
							if (m_day)
							{
								float num28 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
								component6.fireTemperature *= num28;
							}
							else
							{
								float num29 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
								component6.fireTemperature *= num29;
							}
							component6.temperatureModified = true;
						}
						break;
					case 2:
						if (!component6.temperatureModified)
						{
							if (m_day)
							{
								float num26 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
								component6.fireTemperature *= num26;
							}
							else
							{
								float num27 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
								component6.fireTemperature *= num27;
							}
							component6.temperatureModified = true;
						}
						break;
					}
					if (num25 != -1)
					{
						component6.HeatsUp();
					}
				}
				else
				{
					FireCell component7 = m_cells[num - 1, num2].GetComponent<FireCell>();
					int num30 = PropagatingOnSlope(component.position, component7.position);
					switch (num30)
					{
					case 0:
						if (!component7.temperatureModified)
						{
							float num33 = Mathf.Abs(ComputeHeadWind() - m_bias);
							component7.fireTemperature *= num33;
							component7.temperatureModified = true;
						}
						break;
					case 1:
						if (!component7.temperatureModified)
						{
							if (m_day)
							{
								float num34 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWindBias());
								component7.fireTemperature *= num34;
							}
							else
							{
								float num35 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
								component7.fireTemperature *= num35;
							}
							component7.temperatureModified = true;
						}
						break;
					case 2:
						if (!component7.temperatureModified)
						{
							if (m_day)
							{
								float num31 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWind());
								component7.fireTemperature *= num31;
							}
							else
							{
								float num32 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
								component7.fireTemperature *= num32;
							}
							component7.temperatureModified = true;
						}
						break;
					}
					if (num30 != -1)
					{
						component7.HeatsUp();
					}
				}
			}
			if (num2 <= 0)
			{
				continue;
			}
			if (Vector3.Dot(windDirection, m_propagationDirectionSouth.normalized) >= 0f)
			{
				FireCell component8 = m_cells[num, num2 - 1].GetComponent<FireCell>();
				int num36 = PropagatingOnSlope(component.position, component8.position);
				switch (num36)
				{
				case 1:
					if (!component8.temperatureModified)
					{
						if (m_day)
						{
							float num39 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
							component8.fireTemperature *= num39;
						}
						else
						{
							float num40 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
							component8.fireTemperature *= num40;
						}
						component8.temperatureModified = true;
					}
					break;
				case 2:
					if (!component8.temperatureModified)
					{
						if (m_day)
						{
							float num37 = Mathf.Abs(ComputeDownSlopeBias() + ComputeHeadWind());
							component8.fireTemperature *= num37;
						}
						else
						{
							float num38 = Mathf.Abs(ComputeUpSlopeBias() + ComputeHeadWind());
							component8.fireTemperature *= num38;
						}
						component8.temperatureModified = true;
					}
					break;
				}
				if (num36 != -1)
				{
					component8.HeatsUp();
				}
				continue;
			}
			FireCell component9 = m_cells[num, num2 - 1].GetComponent<FireCell>();
			int num41 = PropagatingOnSlope(component.position, component9.position);
			switch (num41)
			{
			case 0:
				if (!component9.temperatureModified)
				{
					float num44 = Mathf.Abs(ComputeHeadWind() - m_bias);
					component9.fireTemperature *= num44;
					component9.temperatureModified = true;
				}
				break;
			case 1:
				if (!component9.temperatureModified)
				{
					if (m_day)
					{
						float num45 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWindBias());
						component9.fireTemperature *= num45;
					}
					else
					{
						float num46 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
						component9.fireTemperature *= num46;
					}
					component9.temperatureModified = true;
				}
				break;
			case 2:
				if (!component9.temperatureModified)
				{
					if (m_day)
					{
						float num42 = Mathf.Abs(ComputeUpSlopeBias() - ComputeHeadWind());
						component9.fireTemperature *= num42;
					}
					else
					{
						float num43 = Mathf.Abs(ComputeDownSlopeBias() - ComputeHeadWind());
						component9.fireTemperature *= num43;
					}
					component9.temperatureModified = true;
				}
				break;
			}
			if (num41 != -1)
			{
				component9.HeatsUp();
			}
		}
		m_fireCellsLit = m_alightCellIndex.Count;
	}

	private void Propagation()
	{
		m_fireCellsLit = 0;
		for (int i = 0; i < m_width; i++)
		{
			for (int j = 0; j < m_height; j++)
			{
				Vector2 value = new Vector2(i, j);
				int key = i * m_width + j;
				bool flag = m_alightCellIndex.ContainsKey(key);
				if (m_cells[i, j].GetComponent<FireCell>().fireProcessHappening && !flag)
				{
					m_alightCellIndex.Add(key, value);
				}
				else if (flag)
				{
					m_alightCellIndex.Remove(key);
				}
			}
		}
		foreach (Vector2 value2 in m_alightCellIndex.Values)
		{
			int num = (int)value2.x;
			int num2 = (int)value2.y;
			FireCell component = m_cells[num, num2].GetComponent<FireCell>();
			component.GridUpdate(GetComponentInParent<FireGrassRemover>());
			if (!component.isAlight)
			{
				continue;
			}
			if (num < m_width - 1)
			{
				FireCell component2 = m_cells[num + 1, num2].GetComponent<FireCell>();
				if (PropagatingOnSlope(component.position, component2.position) != -1)
				{
					component2.HeatsUp();
				}
			}
			if (num2 < m_height - 1)
			{
				FireCell component3 = m_cells[num, num2 + 1].GetComponent<FireCell>();
				if (PropagatingOnSlope(component.position, component3.position) != -1)
				{
					component3.HeatsUp();
				}
			}
			if (num > 0)
			{
				FireCell component4 = m_cells[num - 1, num2].GetComponent<FireCell>();
				if (PropagatingOnSlope(component.position, component4.position) != -1)
				{
					component4.HeatsUp();
				}
			}
			if (num2 > 0)
			{
				FireCell component5 = m_cells[num, num2 - 1].GetComponent<FireCell>();
				if (PropagatingOnSlope(component.position, component5.position) != -1)
				{
					component5.HeatsUp();
				}
			}
		}
		m_fireCellsLit = m_alightCellIndex.Count;
	}

	private float GetValuesFromFuelType(int x, int y, out float hp, out float fuel, out float mositure)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		if (x < 0 || x >= m_fireManager.alphaWidth)
		{
			x = 0;
			hp = 0f;
			fuel = 0f;
			mositure = 0f;
			return num;
		}
		if (y < 0 || y >= m_fireManager.alphaHeight)
		{
			x = 0;
			hp = 0f;
			fuel = 0f;
			mositure = 0f;
			return num;
		}
		Vector3 vector = new Vector3(x, 0f, y);
		vector *= m_fireManager.terrainDetailSize;
		float[] array = new float[4]
		{
			vector.z + 1f,
			vector.z - 1f,
			vector.x + 1f,
			vector.x - 1f
		};
		int length = m_fireManager.terrainAlpha.GetLength(2);
		for (int i = (int)array[3]; i < (int)array[2]; i++)
		{
			for (int j = (int)array[1]; j < (int)array[0]; j++)
			{
				num6 = j / m_DToAWidth;
				num7 = i / m_DToAHeight;
				if (num6 < 0)
				{
					num6 = 0;
				}
				else if (num6 >= m_fireManager.alphaWidth)
				{
					num6 = m_fireManager.alphaWidth - 1;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				else if (num7 >= m_fireManager.alphaHeight)
				{
					num7 = m_fireManager.alphaHeight - 1;
				}
				for (int k = 0; k < length; k++)
				{
					int num8 = (int)m_fireManager.terrainAlpha[num6, num7, k];
					if (!m_fireManager.terrainTextures[k].m_flammable && num8 > 0)
					{
						num = 0f;
						num2 = 0f;
						num3 = 0f;
						num4 = 0f;
					}
					else if (m_fireManager.terrainAlpha[x, y, k] > 0f)
					{
						num += m_fireManager.terrainTextures[k].propagationSpeed;
						num2 = m_fireManager.terrainTextures[k].CellHP();
						num3 = m_fireManager.terrainTextures[k].CellFuel();
						num4 = m_fireManager.groundMoisture + m_fireManager.terrainTextures[k].CellMoisture();
						num5++;
					}
				}
			}
		}
		if (num5 != 0)
		{
			num /= (float)num5;
		}
		hp = num2;
		fuel = num3;
		mositure = num4;
		return num;
	}

	private bool ValidIgnitionLocation()
	{
		bool result = false;
		int num = 0;
		int num2 = 0;
		Vector3 vector = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		vector *= m_fireManager.terrainDetailSize;
		float[] array = new float[4]
		{
			vector.z + 1f,
			vector.z - 1f,
			vector.x + 1f,
			vector.x - 1f
		};
		int length = m_fireManager.terrainAlpha.GetLength(2);
		for (int i = 0; i < 4; i++)
		{
			if (array[i] < 0f)
			{
				array[i] = 0f;
			}
			if (array[i] > (float)m_fireManager.alphaHeight || array[i] > (float)m_fireManager.alphaWidth)
			{
				array[i] = m_fireManager.alphaHeight - 1;
			}
		}
		for (int j = (int)array[3]; j < (int)array[2] + 1; j++)
		{
			for (int k = (int)array[1]; k < (int)array[0] + 1; k++)
			{
				num = k / m_DToAWidth;
				num2 = j / m_DToAHeight;
				for (int l = 0; l < length; l++)
				{
					int num3 = (int)m_fireManager.terrainAlpha[num, num2, l];
					if (!m_fireManager.terrainTextures[l].m_flammable && num3 > 0)
					{
						result = false;
					}
					else if (m_fireManager.terrainAlpha[(int)base.transform.position.x, (int)base.transform.position.z, l] > 0f)
					{
						result = true;
					}
				}
			}
		}
		return result;
	}

	private float ComputeHeadWindBias()
	{
		return Mathf.Clamp(m_windZone.windMain + m_windBias, 1f, float.MaxValue);
	}

	private float ComputeHeadWind()
	{
		return m_windZone.windMain * m_windBias;
	}

	private float ComputeUpSlopeBias()
	{
		return m_hillBias + m_bias;
	}

	private float ComputeDownSlopeBias()
	{
		return m_hillBias - m_bias;
	}

	private int PropagatingOnSlope(Vector3 fireOrigin, Vector3 fireTarget)
	{
		if (Vector3.Distance(fireOrigin, fireTarget) <= m_maxHillDistance)
		{
			if (fireTarget.y > fireOrigin.y)
			{
				return 1;
			}
			if (fireTarget.y < fireOrigin.y)
			{
				return 2;
			}
			return 0;
		}
		if (Mathf.Abs(fireOrigin.y - fireTarget.y) < m_maxHillDistance)
		{
			return 0;
		}
		return -1;
	}
}
