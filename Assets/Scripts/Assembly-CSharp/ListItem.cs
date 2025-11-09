using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
	[SerializeField]
	private Text textName;

	[SerializeField]
	private Text textPlayerCount;

	public void SetInfo(RoomInfo info)
	{
		textName.text = info.Name;
		textPlayerCount.text = info.PlayerCount + "/" + info.MaxPlayers;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
