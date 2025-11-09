using UnityEngine;

public class CoinBuilder : MonoBehaviour
{
	public int number;

	public float maxPosZ;

	public GameObject[] Variants;

	private string statusActiveVariant = "";

	private string statusTurnOFFVariant = "";

	private int currentVariant;

	private void FixedUpdate()
	{
		if (!(base.transform.localPosition != new Vector3(0f, 0f, 0f)))
		{
			return;
		}
		if (base.transform.localPosition.z < maxPosZ)
		{
			if (statusActiveVariant == "")
			{
				currentVariant = Random.Range(0, Variants.Length);
				Variants[currentVariant].SetActive(true);
				statusActiveVariant = "Active";
				statusTurnOFFVariant = "Check";
			}
		}
		else if (base.transform.localPosition == new Vector3(0f, 0f, 0f) && statusTurnOFFVariant == "Check")
		{
			Variants[currentVariant].SetActive(false);
			statusTurnOFFVariant = "";
			statusActiveVariant = "";
		}
	}
}
