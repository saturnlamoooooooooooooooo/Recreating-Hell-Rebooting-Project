using UnityEngine;

public class VentsScript : MonoBehaviour
{
	[SerializeField]
	private GameController game;

	public Material[] material;

	private Renderer rend;

	public AudioSource audioSOpen;

	public AudioSource audioSOpen2;

	public AudioSource audioSClose;

	public GameObject door;

	private Vector3 startPos;

	private Vector3 endPos;

	public int speed;

	protected bool IsClose;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
		IsClose = false;
		startPos = door.transform.position;
		endPos = new Vector3(startPos.x, startPos.y - 3f, startPos.z);
	}

	private void FixedUpdate()
	{
		if (IsClose)
		{
			door.transform.position = Vector3.MoveTowards(door.transform.position, endPos, (float)speed * Time.deltaTime);
		}
		if (!IsClose)
		{
			door.transform.position = Vector3.MoveTowards(door.transform.position, startPos, (float)speed * Time.deltaTime);
		}
	}

	private void OnMouseDown()
	{
		if (IsClose)
		{
			game.buff -= game.de2buff;
			rend.sharedMaterial = material[0];
			IsClose = false;
			audioSOpen.Play();
			audioSOpen2.Play();
		}
		else
		{
			game.buff += game.de2buff;
			rend.sharedMaterial = material[1];
			IsClose = true;
			audioSClose.Play();
		}
	}
}
