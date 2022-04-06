using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatScriptNew : MonoBehaviour
{
	public enum MatType
	{
		amp,
		atk,
		functional,
		boss
	};
	public int amount;
	public int CD;
	private int CD_max;
	[HideInInspector]
	public int amount_max;
	public List<EffectStructNew> myEffects;
	public float lifespan;
	public Sprite matIcon;
	public string Description;
	[Header("Hit VFX")]
	public GameObject myVFX;

	// type
	public MatType myType;

	private void Start()
	{
		amount_max = amount;
		CD_max = CD;
	}

    private void Update()
    {
        if(CD <= 0 && amount < amount_max)
        {
			amount++;
			int overdose = CD;
			if (amount == amount_max)
				CD = CD_max;
			else
				CD = CD_max + overdose;
			//Debug.Log("MatScriptNew: " + gameObject.ToString() + CD);
        }
    }
}
