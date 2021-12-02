using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatScriptNew : MonoBehaviour
{
	public int amount;
	[HideInInspector]
	public int amount_max;
	public List<EffectStructNew> myEffects;
	public float lifespan;

	private void Start()
	{
		amount_max = amount;
	}
}
