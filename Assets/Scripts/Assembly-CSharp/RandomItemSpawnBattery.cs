using UnityEngine;

public class RandomItemSpawnBattery : MonoBehaviour
{
	public GameObject[] objectPrefab;

	public Transform[] spawnPoints;

	private void Start()
	{
		bool[] array = new bool[spawnPoints.Length];
		GameObject[] array2 = objectPrefab;
		foreach (GameObject original in array2)
		{
			int randomUnusedSpawnIndex = GetRandomUnusedSpawnIndex(array);
			Transform transform = spawnPoints[randomUnusedSpawnIndex];
			Object.Instantiate(original, transform.position, transform.rotation);
			array[randomUnusedSpawnIndex] = true;
		}
	}

	private int GetRandomUnusedSpawnIndex(bool[] usedSpawnPoints)
	{
		int num = Random.Range(0, spawnPoints.Length);
		while (usedSpawnPoints[num])
		{
			num = Random.Range(0, spawnPoints.Length);
		}
		return num;
	}

	private void SpawnObjectRPC(int spawnIndex, string prefabName)
	{
		Transform transform = spawnPoints[spawnIndex];
		GameObject gameObject = GameObject.Find(prefabName + "(Clone)");
		if (gameObject != null)
		{
			Object.Destroy(gameObject);
		}
		GameObject gameObject2 = null;
		GameObject[] array = objectPrefab;
		foreach (GameObject gameObject3 in array)
		{
			if (gameObject3.name == prefabName)
			{
				gameObject2 = gameObject3;
				break;
			}
		}
		if (gameObject2 != null)
		{
			Object.Instantiate(gameObject2, transform.position, transform.rotation);
		}
	}
}
