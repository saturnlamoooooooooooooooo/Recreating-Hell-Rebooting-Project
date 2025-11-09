using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(AudioSource))]
	public class FirstPersonController : MonoBehaviour
	{
		[SerializeField]
		private bool m_IsWalking;

		[SerializeField]
		private float m_WalkSpeed;

		[SerializeField]
		private float m_RunSpeed;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_RunstepLenghten;

		[SerializeField]
		private float m_JumpSpeed;

		[SerializeField]
		private float m_StickToGroundForce;

		[SerializeField]
		private float m_GravityMultiplier;

		[SerializeField]
		private MouseLook m_MouseLook;

		[SerializeField]
		private bool m_UseFovKick;

		[SerializeField]
		private FOVKick m_FovKick = new FOVKick();

		[SerializeField]
		private bool m_UseHeadBob;

		[SerializeField]
		private CurveControlledBob m_HeadBob = new CurveControlledBob();

		[SerializeField]
		private LerpControlledBob m_JumpBob = new LerpControlledBob();

		[SerializeField]
		private float m_StepInterval;

		[SerializeField]
		private AudioClip[] m_FootstepSounds;

		[SerializeField]
		private AudioClip m_JumpSound;

		[SerializeField]
		private AudioClip m_LandSound;

		private Camera m_Camera;

		private bool m_Jump;

		private float m_YRotation;

		private Vector2 m_Input;

		private Vector3 m_MoveDir = Vector3.zero;

		private CharacterController m_CharacterController;

		private CollisionFlags m_CollisionFlags;

		private bool m_PreviouslyGrounded;

		private Vector3 m_OriginalCameraPosition;

		private float m_StepCycle;

		private float m_NextStep;

		private bool m_Jumping;

		private AudioSource m_AudioSource;

		public GameObject PlayerModelOff;

		public GameObject Camerapers;

		public GameObject Fonarik;

		public GameObject Fonarikoff;

		public GameObject[] massive;

		public GameObject Players;

		public GameObject PlayersIsMine;

		public GameObject AudiolistnerMe;

		public GameObject HideTrigger;

		public Animator HidePlayerTrigger;

		public FirstPersonController scriptPlayer;

		public AI_Monster scriptAnimatronik;

		public FieldOfView scriptAnimatronik2;

		public AI_Monster2 script2Animatronik;

		public FieldOfView2 script2Animatronik2;

		public bool Sprint;

		public GameObject DeathBox;

		public GameObject StaminaSound;

		public GameObject Layertrigger;

		public Animator FonarikWalkRun;

		public float Gen;

		public GameObject PlayerStaminaSound;

		private float staminaregen;

		private float staminasound;

		public GameObject BatteryDestroyTrigger;

		public GameObject triggerE;

		public GameObject lift;

		public GameObject liftScript;

		public Animator TargetBlue;

		public CameraController CamController;

		private void Awake()
		{
			staminasound = 0f;
			Gen = 1000f;
			staminaregen = 1f;
			Players = GameObject.FindGameObjectWithTag("PlayerConnect");
			scriptAnimatronik = GameObject.FindGameObjectWithTag("Orange").GetComponent<AI_Monster>();
			scriptAnimatronik2 = GameObject.FindGameObjectWithTag("Orange").GetComponent<FieldOfView>();
			script2Animatronik = GameObject.FindGameObjectWithTag("Blue").GetComponent<AI_Monster2>();
			script2Animatronik2 = GameObject.FindGameObjectWithTag("Blue").GetComponent<FieldOfView2>();
		}

		private void Start()
		{
			Players = GameObject.FindGameObjectWithTag("PlayerConnect");
			if ((bool)Players)
			{
				scriptAnimatronik.enabled = false;
				script2Animatronik.enabled = false;
				Debug.Log('f');
			}
			m_CharacterController = GetComponent<CharacterController>();
			m_Camera = Camera.main;
			m_OriginalCameraPosition = m_Camera.transform.localPosition;
			m_FovKick.Setup(m_Camera);
			m_HeadBob.Setup(m_Camera, m_StepInterval);
			m_StepCycle = 0f;
			m_NextStep = m_StepCycle / 2f;
			m_Jumping = false;
			m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(base.transform, m_Camera.transform);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				HidePlayerTrigger.SetBool("On", true);
			}
			if (Input.GetKeyUp(KeyCode.E))
			{
				HidePlayerTrigger.SetBool("On", false);
			}
			if (Gen < 1000f)
			{
				Gen += 83f * Time.deltaTime;
			}
			if (Gen >= 1000f)
			{
				Gen = 1000f;
			}
			if (Gen <= 0f)
			{
				staminaregen = 0f;
				m_RunSpeed = 3f;
				m_IsWalking = true;
				Gen = 0f;
			}
			if (Gen > 450f)
			{
				m_RunSpeed = 8f;
				staminaregen = 1f;
			}
			if (Gen < 400f)
			{
				staminasound = 1f;
			}
			if (Gen > 500f)
			{
				staminasound = 0f;
			}
			if (staminasound == 1f)
			{
				PlayerStaminaSound.SetActive(true);
			}
			if (staminasound == 0f)
			{
				PlayerStaminaSound.SetActive(false);
			}
			if (Sprint && m_CharacterController.velocity.magnitude > 0f && m_CharacterController.isGrounded)
			{
				Gen -= 180f * Time.deltaTime;
			}
			if (staminaregen == 1f)
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					Sprint = true;
				}
				else
				{
					Sprint = false;
				}
			}
			if (staminaregen == 0f)
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					Sprint = false;
				}
				else
				{
					Sprint = false;
				}
			}
			if (staminaregen == 1f)
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					FonarikWalkRun.SetBool("run", true);
				}
				else
				{
					FonarikWalkRun.SetBool("run", false);
				}
			}
			if (staminaregen == 0f)
			{
				FonarikWalkRun.SetBool("run", false);
				if (Input.GetKey(KeyCode.LeftShift))
				{
					FonarikWalkRun.SetBool("run", false);
				}
				else
				{
					FonarikWalkRun.SetBool("run", false);
				}
			}
			RotateView();
			if (!m_Jump)
			{
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
			{
				StartCoroutine(m_JumpBob.DoBobCycle());
				PlayLandingSound();
				m_MoveDir.y = 0f;
				m_Jumping = false;
			}
			if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
			{
				m_MoveDir.y = 0f;
			}
			m_PreviouslyGrounded = m_CharacterController.isGrounded;
		}

		private void PlayLandingSound()
		{
			m_AudioSource.clip = m_LandSound;
			m_AudioSource.Play();
			m_NextStep = m_StepCycle + 0.5f;
		}

		private void FixedUpdate()
		{
			float speed;
			GetInput(out speed);
			Vector3 vector = base.transform.forward * m_Input.y + base.transform.right * m_Input.x;
			RaycastHit hitInfo;
			Physics.SphereCast(base.transform.position, m_CharacterController.radius, Vector3.down, out hitInfo, m_CharacterController.height / 2f, -1, QueryTriggerInteraction.Ignore);
			vector = Vector3.ProjectOnPlane(vector, hitInfo.normal).normalized;
			m_MoveDir.x = vector.x * speed;
			m_MoveDir.z = vector.z * speed;
			if (m_CharacterController.isGrounded)
			{
				m_MoveDir.y = 0f - m_StickToGroundForce;
				if (m_Jump)
				{
					m_MoveDir.y = m_JumpSpeed;
					PlayJumpSound();
					m_Jump = false;
					m_Jumping = true;
				}
			}
			else
			{
				m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
			}
			m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
			ProgressStepCycle(speed);
			UpdateCameraPosition(speed);
			m_MouseLook.UpdateCursorLock();
		}

		private void PlayJumpSound()
		{
			m_AudioSource.clip = m_JumpSound;
			m_AudioSource.Play();
		}

		private void ProgressStepCycle(float speed)
		{
			if (m_CharacterController.velocity.sqrMagnitude > 0f && (m_Input.x != 0f || m_Input.y != 0f))
			{
				m_StepCycle += (m_CharacterController.velocity.magnitude + speed * (m_IsWalking ? 1f : m_RunstepLenghten)) * Time.fixedDeltaTime;
			}
			if (m_StepCycle > m_NextStep)
			{
				m_NextStep = m_StepCycle + m_StepInterval;
				PlayFootStepAudio();
			}
		}

		private void PlayFootStepAudio()
		{
			if (m_CharacterController.isGrounded)
			{
				int num = Random.Range(1, m_FootstepSounds.Length);
				m_AudioSource.clip = m_FootstepSounds[num];
				m_AudioSource.PlayOneShot(m_AudioSource.clip);
				m_FootstepSounds[num] = m_FootstepSounds[0];
				m_FootstepSounds[0] = m_AudioSource.clip;
			}
		}

		private void UpdateCameraPosition(float speed)
		{
			if (m_UseHeadBob)
			{
				Vector3 localPosition;
				if (m_CharacterController.velocity.magnitude > 0f && m_CharacterController.isGrounded)
				{
					m_Camera.transform.localPosition = m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude + speed * (m_IsWalking ? 1f : m_RunstepLenghten));
					localPosition = m_Camera.transform.localPosition;
					localPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
					CamController.enabled = true;
					FonarikWalkRun.SetBool("walk", true);
					TargetBlue.SetBool("On", true);
				}
				else
				{
					CamController.enabled = false;
					FonarikWalkRun.SetBool("walk", false);
					TargetBlue.SetBool("On", false);
					localPosition = m_Camera.transform.localPosition;
					localPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
				}
				m_Camera.transform.localPosition = localPosition;
			}
		}

		private void GetInput(out float speed)
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool isWalking = m_IsWalking;
			m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
			speed = (m_IsWalking ? m_WalkSpeed : m_RunSpeed);
			m_Input = new Vector2(axis, axis2);
			if (m_Input.sqrMagnitude > 1f)
			{
				m_Input.Normalize();
			}
			if (m_IsWalking != isWalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0f)
			{
				StopAllCoroutines();
				StartCoroutine((!m_IsWalking) ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
			}
		}

		private void RotateView()
		{
			m_MouseLook.LookRotation(base.transform, m_Camera.transform);
		}

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
			if (m_CollisionFlags != CollisionFlags.Below && !(attachedRigidbody == null) && !attachedRigidbody.isKinematic)
			{
				attachedRigidbody.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
			}
		}
	}
}
