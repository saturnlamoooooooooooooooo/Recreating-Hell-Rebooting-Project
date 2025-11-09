using UnityEngine;

public class FireVisualManager : MonoBehaviour
{
	private ParticleSystem[] m_particleSystems;

	[Tooltip("Should be the same number as particle systems used in the Fire, which of those particle systems should be active in the simulation's heat up step.")]
	public bool[] m_heatUp;

	[Tooltip("Should be the same number as particle systems used in the Fire, which of those particle systems should be active in the simulation's ignition step.")]
	public bool[] m_ignition;

	[Tooltip("Should be the same number as particle systems used in the Fire, which of those particle systems should be active in the simulation's extingush step.")]
	public bool[] m_extinguish;

	private bool m_heatState;

	private bool m_ignitionState;

	private bool m_extinguishState;

	private bool m_heatStateSet;

	private bool m_ignitionStateSet;

	private bool m_extinguishStateSet;

	private void Start()
	{
		m_particleSystems = GetComponentsInChildren<ParticleSystem>();
		if (m_heatUp.Length > m_particleSystems.Length)
		{
			Debug.LogError(base.gameObject.name + " FireVisualManager::heatUp bigger then the number of children with Particle Systems");
		}
		if (m_ignition.Length > m_particleSystems.Length)
		{
			Debug.LogError(base.gameObject.name + " FireVisualManager::ignition bigger then the number of children with Particle Systems");
		}
		if (m_extinguish.Length > m_particleSystems.Length)
		{
			Debug.LogError(base.gameObject.name + " FireVisualManager::extingush bigger then the number of children with Particle Systems");
		}
		for (int i = 0; i < m_particleSystems.Length; i++)
		{
			m_particleSystems[i].gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (m_heatState && !m_heatStateSet)
		{
			for (int i = 0; i < m_particleSystems.Length; i++)
			{
				m_particleSystems[i].gameObject.SetActive(m_heatUp[i]);
			}
			m_heatStateSet = true;
		}
		else if (m_ignitionState && !m_ignitionStateSet)
		{
			for (int j = 0; j < m_particleSystems.Length; j++)
			{
				m_particleSystems[j].gameObject.SetActive(m_ignition[j]);
			}
			m_ignitionStateSet = true;
		}
		else if (m_extinguishState && !m_extinguishStateSet)
		{
			for (int k = 0; k < m_particleSystems.Length; k++)
			{
				m_particleSystems[k].gameObject.SetActive(m_extinguish[k]);
			}
			m_extinguishStateSet = true;
		}
	}

	public void SetHeatState()
	{
		m_heatState = true;
		m_ignitionState = false;
		m_extinguishState = false;
	}

	public void SetIgnitionState()
	{
		m_heatState = false;
		m_ignitionState = true;
		m_extinguishState = false;
	}

	public void SetExtingushState()
	{
		m_heatState = false;
		m_ignitionState = false;
		m_extinguishState = true;
	}
}
