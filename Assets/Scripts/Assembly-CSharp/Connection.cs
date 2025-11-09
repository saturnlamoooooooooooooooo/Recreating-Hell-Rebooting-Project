using Photon.Pun;
using UnityEngine;

public class Connection : MonoBehaviourPunCallbacks
{
	public void Connect()
	{
		PhotonNetwork.ConnectUsingSettings();
		Debug.Log("connected");
	}

	public void disConnect()
	{
		PhotonNetwork.Disconnect();
		Debug.Log("disconnected");
	}
}
