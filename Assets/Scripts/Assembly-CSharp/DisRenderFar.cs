using UnityEngine;

public class DisRenderFar : MonoBehaviour
{
	public float disableDistance;

	private Renderer _renderer;

	private Camera _camera;

	private void Awake()
	{
		disableDistance = 18f;
	}

	private void Start()
	{
		_renderer = GetComponent<Renderer>();
		_camera = Camera.main;
	}

	private void OnBecameInvisible()
	{
		_renderer.enabled = false;
	}

	private void OnBecameVisible()
	{
		_renderer.enabled = true;
	}

	private void Checks()
	{
		if (Vector3.Distance(base.transform.position, _camera.transform.position) > disableDistance)
		{
			_renderer.enabled = false;
			return;
		}
		_renderer.enabled = true;
		if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_camera), GetComponent<Renderer>().bounds))
		{
			OnBecameInvisible();
		}
		else
		{
			OnBecameVisible();
		}
	}

	private void Update()
	{
		Checks();
	}
}
