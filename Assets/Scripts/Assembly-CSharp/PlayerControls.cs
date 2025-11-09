using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	[Tooltip("The Hit Points of the cell, the higher the HP to slower the cell is to heat up and ignite.")]
	public GameObject m_fireIgniter;

	private GameObject m_equipedIgniter;

	private Rigidbody m_rigidBody;

	private FireIgniter m_igniter;

	private Camera m_camera;

	[Tooltip("Higher the value the faster the player moves.")]
	public float m_movementSpeed = 5f;

	[Tooltip("Higher the value the faster the player rotates.")]
	public float m_rotationSpeed = 2f;

	[Tooltip("The height the camera will be above the ground.")]
	public float m_cameraHeightOffset = 2f;

	[Tooltip("How many seconds until the next FireIgniter is spawned.")]
	public float m_respawnDelay = 5f;

	private float m_timer;

	private void Start()
	{
		m_camera = Camera.main;
		updateCameraTransform();
		createNewIgniter();
		m_timer = m_respawnDelay;
	}

	private void Update()
	{
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
		float num = Input.GetAxis("Vertical") * m_movementSpeed;
		float num2 = Input.GetAxis("Horizontal") * m_movementSpeed;
		float yAngle = Input.GetAxis("Mouse X") * m_rotationSpeed;
		float num3 = Input.GetAxis("Mouse Y") * m_rotationSpeed;
		num *= Time.deltaTime;
		num2 *= Time.deltaTime;
		base.transform.Translate(num2, 0f, num);
		base.transform.Rotate(0f - num3, yAngle, 0f);
		base.transform.rotation = Quaternion.Euler(base.transform.rotation.eulerAngles.x, base.transform.rotation.eulerAngles.y, 0f);
		if (m_timer < m_respawnDelay)
		{
			m_timer -= Time.deltaTime;
		}
		if (m_timer == m_respawnDelay && Input.GetButtonDown("Fire1") && m_fireIgniter != null)
		{
			if (m_rigidBody != null)
			{
				m_rigidBody.useGravity = true;
				m_rigidBody.detectCollisions = true;
			}
			if (m_igniter != null)
			{
				m_igniter.enabled = true;
			}
			m_timer -= Time.deltaTime;
		}
		if (m_timer <= 0f)
		{
			m_timer = m_respawnDelay;
			createNewIgniter();
		}
		updateCameraTransform();
	}

	private void updateCameraTransform()
	{
		Vector3 position = base.transform.position;
		position.y = Terrain.activeTerrain.SampleHeight(base.transform.position) + m_cameraHeightOffset;
		m_camera.transform.position = position;
		m_camera.transform.rotation = base.transform.rotation;
		base.transform.position = position;
	}

	private void createNewIgniter()
	{
		Quaternion rotation = m_camera.transform.rotation;
		Vector3 vector = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z);
		vector.y -= 0.75f;
		Vector3 vector2 = new Vector3(0.35f, 0f, 1.5f);
		vector2 = rotation * vector2;
		vector2 += vector;
		m_equipedIgniter = Object.Instantiate(m_fireIgniter, vector2, rotation, base.transform);
		m_equipedIgniter.transform.Rotate(Vector3.up * 15f);
		m_equipedIgniter.transform.Rotate(Vector3.right * 24f);
		m_rigidBody = m_equipedIgniter.GetComponent<Rigidbody>();
		if (m_rigidBody != null)
		{
			m_rigidBody.useGravity = false;
			m_rigidBody.detectCollisions = false;
		}
		else
		{
			Debug.LogWarning("No Rigidbody found as a component on fireIgniter, you sure you don't need one?");
		}
		m_igniter = m_equipedIgniter.GetComponent<FireIgniter>();
		if (m_igniter != null)
		{
			m_igniter.enabled = false;
		}
		else
		{
			Debug.LogWarning("No FireIgniter found as a component on GameObject used to start the fire.");
		}
	}

	private IEnumerator respawnIgniter()
	{
		yield return new WaitForSeconds(m_respawnDelay);
		createNewIgniter();
	}
}
