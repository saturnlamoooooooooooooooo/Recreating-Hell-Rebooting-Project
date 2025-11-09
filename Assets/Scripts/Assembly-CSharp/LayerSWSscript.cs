using UnityEngine;

public class LayerSWSscript : MonoBehaviour
{
	public int newLayer;

	public string newTag;

	public GameObject playertagchange;

	public GameObject fpscontroller;

	public GameObject Pl1;

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "LayerTrigger")
		{
			fpscontroller.layer = newLayer;
			playertagchange.tag = newTag;
			Debug.Log("layer and tag!");
			Pl1.SetActive(false);
		}
	}
}
