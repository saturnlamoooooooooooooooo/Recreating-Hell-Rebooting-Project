using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuCreate : MonoBehaviourPunCallbacks
{
	public InputField InputFieldNameRoom;

	[SerializeField]
	private ListItem itemPrefab;

	[SerializeField]
	private Transform content;

	public void CreateRoom()
	{
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 4;
		PhotonNetwork.CreateRoom(InputFieldNameRoom.text, roomOptions);
		PhotonNetwork.LoadLevel("Game");
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(InputFieldNameRoom.text);
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (RoomInfo room in roomList)
		{
			ListItem listItem = Object.Instantiate(itemPrefab, content);
			if (listItem != null)
			{
				listItem.SetInfo(room);
			}
		}
	}
}
