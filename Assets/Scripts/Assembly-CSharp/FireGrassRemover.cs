using UnityEngine;

public class FireGrassRemover : MonoBehaviour
{
	private FireManager m_fireManager;

	private float[] m_pixelPoints = new float[4];

	private float m_radius = 1f;

	private int m_scorchmarkTexture;

	private int m_DToAWidth;

	private int m_DToAHeight;

	private bool m_replaceGrass;

	public float radius
	{
		set
		{
			m_radius = value;
		}
	}

	private void Start()
	{
		m_fireManager = Object.FindObjectOfType<FireManager>();
		if (!(m_fireManager != null))
		{
			return;
		}
		m_DToAWidth = m_fireManager.terrainWidth / m_fireManager.alphaWidth;
		m_DToAHeight = m_fireManager.terrainHeight / m_fireManager.alphaHeight;
		FireTerrainTexture[] terrainTextures = m_fireManager.terrainTextures;
		foreach (FireTerrainTexture fireTerrainTexture in terrainTextures)
		{
			if (fireTerrainTexture.m_isGroundBurnTexture)
			{
				m_scorchmarkTexture = fireTerrainTexture.m_textureID;
				break;
			}
		}
		m_replaceGrass = !m_fireManager.removeGrassOnceBurnt;
	}

	public void DeleteGrassOnPosition(Vector3 position)
	{
		RemoveGrass(m_fireManager.terrain, position);
	}

	private void RemoveGrass(Terrain terrian, Vector3 position)
	{
		Vector3 vector = position;
		vector *= m_fireManager.terrainDetailSize;
		m_pixelPoints[0] = vector.z + m_radius;
		m_pixelPoints[1] = vector.z - m_radius;
		m_pixelPoints[2] = vector.x + m_radius;
		m_pixelPoints[3] = vector.x - m_radius;
		for (int i = 0; i < 4; i++)
		{
			if (m_pixelPoints[i] < 0f)
			{
				m_pixelPoints[i] = 0f;
			}
			if (m_pixelPoints[i] > (float)m_fireManager.terrainHeight || m_pixelPoints[i] > (float)m_fireManager.terrainWidth)
			{
				m_pixelPoints[i] = m_fireManager.terrainHeight - 1;
			}
		}
		for (int j = (int)m_pixelPoints[3]; j < (int)m_pixelPoints[2] + 1; j++)
		{
			for (int k = (int)m_pixelPoints[1]; k < (int)m_pixelPoints[0] + 1; k++)
			{
				if (!m_fireManager.maxGrassDetails)
				{
					if (m_replaceGrass && m_fireManager.terrainMap[k, j] != 0)
					{
						m_fireManager.terrainMap[k, j] = 0;
						m_fireManager.terrainReplacementMap[k, j] = 1;
					}
					else
					{
						m_fireManager.terrainMap[k, j] = 0;
					}
				}
				else
				{
					for (int l = 0; l < m_fireManager.terrain.terrainData.detailPrototypes.Length; l++)
					{
						if (m_replaceGrass && m_fireManager.terrainMaps[l][k, j] != 0)
						{
							m_fireManager.terrainMaps[l][k, j] = 0;
							m_fireManager.terrainMaps[m_fireManager.burntGrassDetailIndex][k, j] = 1;
						}
						else if (!m_replaceGrass)
						{
							m_fireManager.terrainMaps[l][k, j] = 0;
						}
					}
				}
				m_fireManager.dirty = true;
			}
		}
		int length = m_fireManager.terrainAlpha.GetLength(2);
		for (int m = (int)m_pixelPoints[3]; m < (int)m_pixelPoints[2] + 1; m++)
		{
			for (int n = (int)m_pixelPoints[1]; n < (int)m_pixelPoints[0] + 1; n++)
			{
				if (m_pixelPoints[0] > (float)n && m_pixelPoints[1] < (float)n && m_pixelPoints[2] > (float)m && m_pixelPoints[3] < (float)m)
				{
					int num = n / m_DToAWidth;
					int num2 = m / m_DToAHeight;
					for (int num3 = 0; num3 < length; num3++)
					{
						m_fireManager.terrainAlpha[num, num2, num3] = 0f;
					}
					m_fireManager.terrainAlpha[num, num2, m_scorchmarkTexture] = 1f;
				}
			}
		}
	}
}
