using UnityEngine;

public class ButtonDestroy : MonoBehaviour
{
	public GameObject Glaza1;

	public GameObject ObjectSetOn2;

	public GameObject ObjectSetOn;

	public GameObject ObjectDestroy;

	private void OnMouseDown()
	{
		Glaza1.SetActive(true);
		ObjectDestroy.SetActive(false);
		ObjectSetOn.SetActive(true);
		ObjectSetOn2.SetActive(true);
	}
}
