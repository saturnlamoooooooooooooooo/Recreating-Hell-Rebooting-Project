using UnityEngine;

public class FireIgniter : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Width of the fire grid, fire starts in the center of the grid")]
	private int m_gridWidth = 10;

	[SerializeField]
	[Tooltip("Height of the fire grid, fire starts in the center of the grid")]
	private int m_gridHeight = 10;

	[SerializeField]
	[Tooltip("Prefab of the fire to use")]
	private GameObject m_firePrefab;

	[SerializeField]
	[Tooltip("Delete this GameObject when there is a collision with it and the terrain or another GameObject?")]
	private bool m_destroyOnCollision;

	private bool m_fireIgnited;

	private void Start()
	{
		if (m_firePrefab == null)
		{
			Debug.LogError("No Fire Prefab set on Fire Igniter.");
		}
		if (m_gridWidth < 0)
		{
			m_gridWidth = -m_gridWidth;
		}
		if (m_gridHeight < 0)
		{
			m_gridHeight = -m_gridHeight;
		}
		if (m_gridWidth == 0)
		{
			m_gridWidth = 1;
		}
		if (m_gridHeight == 0)
		{
			m_gridHeight = 1;
		}
	}

	public void OnCollision()
	{
		GameObject obj = new GameObject
		{
			name = "FireGrid"
		};
		FireGrid fireGrid = obj.AddComponent<FireGrid>();
		obj.AddComponent<FireGrassRemover>();
		fireGrid.IgniterUpdate(m_firePrefab, base.gameObject.transform.position, m_gridWidth, m_gridHeight);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!m_fireIgnited)
		{
			OnCollision();
			m_fireIgnited = true;
			if (m_destroyOnCollision)
			{
				Destroy();
			}
		}
	}

	private void Destroy()
	{
		Object.Destroy(base.gameObject);
	}
}
