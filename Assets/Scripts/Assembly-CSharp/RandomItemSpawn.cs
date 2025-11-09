using Photon.Pun;
using UnityEngine;

public class RandomItemSpawn : MonoBehaviourPunCallbacks
{
	public GameObject objectPrefab;

	public Transform[] spawnPoints;

	private void Start()
	{
		if (base.photonView.IsMine)
		{
			SpawnObject();
		}
	}

	private void SpawnObject()
	{
		int num = Random.Range(0, spawnPoints.Length);
		Transform transform = spawnPoints[num];
		PhotonNetwork.Instantiate(objectPrefab.name, transform.position, transform.rotation, 0);
		base.photonView.RPC("SpawnObjectRPC", RpcTarget.Others, num);
	}

	[PunRPC]
	private void SpawnObjectRPC(int spawnIndex)
	{
		Transform transform = spawnPoints[spawnIndex];
		Object.Instantiate(objectPrefab, transform.position, transform.rotation);
	}
}
