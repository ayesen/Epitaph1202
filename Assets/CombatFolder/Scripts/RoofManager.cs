using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofManager : MonoBehaviour
{
    public List<GameObject> roofsToShow;
    public List<GameObject> roofsToHide;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			foreach (var roof in roofsToShow)
			{
				roof.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}
			foreach (var roof in roofsToHide)
			{
				roof.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
		}
	}
}
