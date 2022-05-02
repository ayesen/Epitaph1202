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
	public int CD_max;
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
			for (int i = 0; i <= 2; i++)
			{
				if (PlayerScriptNew.me.matSlots[i].gameObject.name == gameObject.name)
				{
					if (i == 0 && UIManager.Me.left_C == null)
					{
						UIManager.Me.left_C = UIManager.Me.StartCoroutine(UIManager.Me.MakePulse(UIManager.Me.leftIcon, 1f));
						
					}
					else if (i == 1 && UIManager.Me.up_C == null)
					{
						UIManager.Me.up_C = UIManager.Me.StartCoroutine(UIManager.Me.MakePulse(UIManager.Me.upIcon, 1f));
						
					}
					else if (i == 2 && UIManager.Me.right_C == null)
					{
						UIManager.Me.right_C = UIManager.Me.StartCoroutine(UIManager.Me.MakePulse(UIManager.Me.rightIcon, 1f));
						
					}
				}
			}
			Debug.Log("MatScriptNew: " + gameObject.ToString() + CD);
        }
    }
}
