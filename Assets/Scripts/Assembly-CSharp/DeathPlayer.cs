using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathPlayer : MonoBehaviour
{
	public int Level;

	public GameObject Scrimer;

	public GameObject Scrimer2;

	public GameObject Canvas1;

	public Camera Cam1;

	public GameObject listn;

	public GameObject player;

	public GameObject playertagchange;

	public string newTag;

	public GameObject Fonarik;

	public GameObject DeathCube;

	public GameObject PauseScript;

	public GameObject ScrimerPsich;

	public GameObject fpscontroller;

	public FonarikScript scriptFonarik;

	public int newLayer;

	public GameObject Fuse1;

	public GameObject Fuse2;

	public GameObject RedCards;

	public GameObject BlueCards;

	public Transform DropTransform;

	public FirstPersonController scriptPlayer;

	public Data2 data;

	public DataG dataG;

	public GameObject AtmoSounds;

	public GameObject PlayerStamina;

	public GameObject ScrimerOff1;

	public GameObject ScrimerOff2;

	public GameObject ScrimerEnd;

	private void Awake()
	{
	}

	private void Start()
	{
		string tag2 = playertagchange.tag;
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "DeathTrigger")
		{
			Debug.Log("Игрок умер");
			PauseScript.SetActive(false);
			Fonarik.SetActive(false);
			Canvas1.SetActive(false);
			listn.SetActive(false);
			playertagchange.tag = newTag;
			scriptPlayer.enabled = false;
			Cam1.enabled = false;
			Scrimer.SetActive(true);
			player.SetActive(false);
			scriptFonarik.enabled = false;
			AtmoSounds.SetActive(false);
			Object.Destroy(PlayerStamina);
			fpscontroller.layer = newLayer;
			DeathCube.SetActive(false);
			return;
		}
		if (col.tag == "DeathTrigger2")
		{
			Debug.Log("Игрок умер");
			PauseScript.SetActive(false);
			Fonarik.SetActive(false);
			Canvas1.SetActive(false);
			listn.SetActive(false);
			playertagchange.tag = newTag;
			scriptPlayer.enabled = false;
			Cam1.enabled = false;
			Scrimer2.SetActive(true);
			player.SetActive(false);
			scriptFonarik.enabled = false;
			AtmoSounds.SetActive(false);
			Object.Destroy(PlayerStamina);
			fpscontroller.layer = newLayer;
			DeathCube.SetActive(false);
		}
		if (col.tag == "DeathPsich")
		{
			Debug.Log("Игрок умер");
			PauseScript.SetActive(false);
			Fonarik.SetActive(false);
			Canvas1.SetActive(false);
			listn.SetActive(false);
			playertagchange.tag = newTag;
			scriptPlayer.enabled = false;
			Cam1.enabled = false;
			ScrimerPsich.SetActive(true);
			player.SetActive(false);
			scriptFonarik.enabled = false;
			AtmoSounds.SetActive(false);
			Object.Destroy(PlayerStamina);
			fpscontroller.layer = newLayer;
			DeathCube.SetActive(false);
		}
		if (col.tag == "DeathOff1")
		{
			Debug.Log("Игрок умер");
			PauseScript.SetActive(false);
			Fonarik.SetActive(false);
			Canvas1.SetActive(false);
			listn.SetActive(false);
			playertagchange.tag = newTag;
			scriptPlayer.enabled = false;
			Cam1.enabled = false;
			ScrimerOff1.SetActive(true);
			player.SetActive(false);
			scriptFonarik.enabled = false;
			AtmoSounds.SetActive(false);
			Object.Destroy(PlayerStamina);
			fpscontroller.layer = newLayer;
			DeathCube.SetActive(false);
		}
		if (col.tag == "DeathOff2")
		{
			Debug.Log("Игрок умер");
			PauseScript.SetActive(false);
			Fonarik.SetActive(false);
			Canvas1.SetActive(false);
			listn.SetActive(false);
			playertagchange.tag = newTag;
			scriptPlayer.enabled = false;
			Cam1.enabled = false;
			ScrimerOff2.SetActive(true);
			player.SetActive(false);
			scriptFonarik.enabled = false;
			AtmoSounds.SetActive(false);
			Object.Destroy(PlayerStamina);
			fpscontroller.layer = newLayer;
			DeathCube.SetActive(false);
		}
		if (col.tag == "DeathEnd")
		{
			Debug.Log("Игрок умер");
			PauseScript.SetActive(false);
			Fonarik.SetActive(false);
			Canvas1.SetActive(false);
			listn.SetActive(false);
			playertagchange.tag = newTag;
			scriptPlayer.enabled = false;
			Cam1.enabled = false;
			ScrimerEnd.SetActive(true);
			player.SetActive(false);
			scriptFonarik.enabled = false;
			AtmoSounds.SetActive(false);
			Object.Destroy(PlayerStamina);
			fpscontroller.layer = newLayer;
			DeathCube.SetActive(false);
		}
	}
}
