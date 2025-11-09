using System;
using UnityEngine;

[Serializable]
public class FireTerrainTexture
{
	[Tooltip("Which Terrain Texture does this data map to, starts at 0.")]
	public int m_textureID;

	[Tooltip("Is this a flammable fuel?")]
	public bool m_flammable;

	[Tooltip("Is this the burnt ground texture to replace terrain textures where a fire has been?")]
	public bool m_isGroundBurnTexture;

	[Tooltip("Lower value, random fuel value generated between lower and higher value.")]
	public float m_fuelLowerValue = 1f;

	[Tooltip("Higher value, random fuel value generated between lower and higher value.")]
	public float m_fuelHigherValue = 2f;

	[Tooltip("Lower value, random hit point (used up in heat up step) value generated between lower and higher value.")]
	public float m_HPLowerValue = 1f;

	[Tooltip("Higher value, random hit point (used up in heat up step) value generated between lower and higher value.")]
	public float m_HPHigherValue = 2f;

	[Tooltip("Amount of ground moisture on this fuel.")]
	public float m_moisture;

	[Tooltip("Higher the value the quicker fire reaches ignition temperature. Fire will propagate faster the higher the value.")]
	public float m_firePropagationSpeed = 20f;

	public float propagationSpeed
	{
		get
		{
			return m_firePropagationSpeed;
		}
	}

	public FireTerrainTexture(int ID, bool flammable, bool scorchTexture)
	{
		m_textureID = ID;
		m_flammable = flammable;
		m_isGroundBurnTexture = scorchTexture;
		if (!m_flammable)
		{
			m_firePropagationSpeed = 0f;
			m_fuelLowerValue = 0f;
			m_fuelHigherValue = 0f;
			m_HPLowerValue = 0f;
			m_HPHigherValue = 0f;
		}
	}

	public float CellHP()
	{
		return UnityEngine.Random.Range(m_HPLowerValue, m_HPHigherValue);
	}

	public float CellFuel()
	{
		return UnityEngine.Random.Range(m_fuelLowerValue, m_fuelHigherValue);
	}

	public float CellMoisture()
	{
		return m_moisture;
	}
}
