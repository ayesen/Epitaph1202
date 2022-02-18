using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatScriptNew : MonoBehaviour
{
	public int amount;
	public int CD;
	private int CD_max;
	[HideInInspector]
	public int amount_max;
	public List<EffectStructNew> myEffects;
	public float lifespan;
	public Sprite matIcon;
	public string Description;

	private void Start()
	{
		amount_max = amount;
		CD_max = CD;
	}

    private void Update()
    {
        if(amount < amount_max)
        {
			
        }
    }
}
