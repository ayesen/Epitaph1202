using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatScriptNew : MonoBehaviour
{
	public int amount;
	public int CD;
	[HideInInspector]
	public int amount_max;
	public List<EffectStructNew> myEffects;
	public float lifespan;
	public Sprite matIcon;
	public string Description;

	private void Start()
	{
		amount_max = amount;
	}
}
