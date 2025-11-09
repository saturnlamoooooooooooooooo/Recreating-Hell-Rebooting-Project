using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] Spawns;

	public GameObject Player;

	private void Awake()
	{
		Vector3 position = Spawns[Random.Range(0, Spawns.Length)].transform.position;
		PhotonNetwork.Instantiate(Player.name, position, Quaternion.identity, 0);
	}
}
