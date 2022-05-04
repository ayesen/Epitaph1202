using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class EffectManagerScriptHolder : MonoBehaviour
{
	private List<GameObject> enemyInfoUI;
	private Animator anim;
	// this script is for calling function from effect manager new in animation

	private void Start()
	{
		enemyInfoUI = GameObject.FindGameObjectsWithTag("EnemyInfoUI").ToList();
		anim = GetComponent<Animator>();
	}

    private void Update()
    {
		transform.localRotation = Quaternion.Euler(0, 0, 0);
		transform.localPosition = Vector3.zero;
    }

    public void Casting()
	{
		PlayerScriptNew.me.atkButtonPressed = false;
		ConditionStruct cs = new ConditionStruct
		{
			condition = EffectStructNew.Condition.casting,
			conditionTrigger = PlayerScriptNew.me.gameObject
		};
		EffectManagerNew.me.conditionProcessList.Add(cs);
		// clear all combat info
		foreach (var ui in enemyInfoUI)
		{
			ui.GetComponent<TextMeshProUGUI>().text = "";
		}
	}

	public void CastShoutSound()
    {
		SoundMan.SoundManager.PlayerCast();
    }

	public void PlayerWalkingSound()
    {
		SoundMan.SoundManager.ManWalk();
    }

	public void ResetSelected()
	{
		anim.ResetTrigger("selected");
	}
}
